using System.Collections.Generic;
using UnityEngine;

public class CombinateEventPlatform : EventPlatformScript
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private List<MeteorScript> meteors = new List<MeteorScript>();
    [SerializeField] private GameObject feedback;
    public Transform posToInstiantiate;
    public GameObject ventiladorPrefab;
    private GameObject fan;
    private int state = 0; //Estado 0: Ventilador con meteoritos
    private int maxState = 3;
    private List<bool> poolMeteors = new List<bool>();
    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 0.25f;
    [SerializeField] private float timeToAction = 0.2f;
    [Header("Extra Configuration")]
    public float timeVariaton = 1f;
    [SerializeField] private int pool = 5;
    #endregion

    #region ParentFunctions
    public override void Init()
    {
        base.Init();
        state = Random.Range(0, maxState);
        state = 0; //actualmente solo hay 1 combinación
        if (state == 0)
        {

            GameObject[] meteorGO;
            meteorGO = GameObject.FindGameObjectsWithTag("Meteor");

            if (meteors.Count == 0)
            {
                for (int i = 0; i < meteorGO.Length; i++)
                {
                    if (meteorGO[i].GetComponent<MeteorScript>() != null)
                    {
                        meteors.Add(meteorGO[i].GetComponent<MeteorScript>());
                        poolMeteors.Add(false);
                    }
                }
            }

            pool = meteors.Count;
            type = TypeEvent.TIME;

            listEvent.Add(FeedBack);
            listEvent.Add(Action2);
            for (int i = 0; i < pool; i++)
            {
                listEvent.Add(Wait);
                listEvent.Add(Action);
                listEvent.Add(Wait);
            }
            //listEvent.Add(Restart);
            listEvent.Add(End);
        }
    }
    public override void ForceFinnish()
    {
        base.ForceFinnish();
        listEvent.Add(DestroyFan);
        for (int i = 0; i < 7; i++) listEvent.Add(Wait);
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions

    private float FeedBack() {

        for (int i = 0; i < meteors.Count; i++)
        {
            MeteorScript suplent;
            int randomNum = Random.Range(0, meteors.Count - 1);
            suplent = meteors[i];
            meteors[i] = meteors[randomNum];
            meteors[randomNum] = suplent;

        }

        for (int i = 0; i < poolMeteors.Count; i++)
        {
            poolMeteors[i] = false;
        }

        return timeToAction * timeVariaton;
    }


    private float Action()
    {
        if (state == 0)
        {
            for (int i = 0; i < meteors.Count; i++)
            {
                if (!poolMeteors[i])
                {
                    meteors[i].Active(1f);
                    poolMeteors[i] = true;
                    break;
                }
            }
        }

        return timeToAction * timeVariaton;
    }

    private float Action2()
    {
        if (state == 0)
        {
            fan = Instantiate(ventiladorPrefab, posToInstiantiate);
        }
        return 0;
    }

    private float DestroyFan()
    {
        if (fan != null) Destroy(fan);
        return 0;
    }

    private float Wait()
    {
        return waitTime;
    }

    private float Restart()
    {
        return -2;
    }
    private float End()
    {
        return -1;
    }
    #endregion
}
