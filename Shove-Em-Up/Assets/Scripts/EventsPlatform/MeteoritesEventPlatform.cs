using System.Collections.Generic;
using UnityEngine;

public class MeteoritesEventPlatform : EventPlatformScript 
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private List<MeteorScript> meteors = new List<MeteorScript>();
    [SerializeField] private GameObject feedback;
    private List<bool> poolMeteors = new List<bool>();
    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private float timeToAction = 0.1f;
    [Header("Extra Configuration")]
    public float timeVariaton = 0.2f;
    [SerializeField] private int pool = 5;
    #endregion

    #region ParentFunctions
    public override void Init() {
        base.Init();
        GameObject [] meteorGO;
        meteorGO =GameObject.FindGameObjectsWithTag("Meteor");

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
        for (int i = 0; i < pool; i++) {
            listEvent.Add(Wait);
            listEvent.Add(Action);
            listEvent.Add(Wait);

        }
        //listEvent.Add(Restart);
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions

    private float FeedBack()
    {
        
        for(int i = 0; i < meteors.Count; i++)
        {
            MeteorScript suplent;
            int randomNum = Random.Range(0, meteors.Count - 1);
            suplent = meteors[i];
            meteors[i] = meteors[randomNum];
            meteors[randomNum] = suplent;
            
        }

        for(int i = 0; i < poolMeteors.Count; i++)
        {
            poolMeteors[i] = false;
        }

        return timeToAction * timeVariaton;
    }

    private float Action() {
        for(int i = 0; i < meteors.Count; i++)
        {
            if(!poolMeteors[i])
            {
                meteors[i].Active(2f);
                poolMeteors[i] = true;
                break;
            }
        }

        return timeToAction * timeVariaton;
    }

    private float Wait() {
        return waitTime;
    }

    private float Restart() {
        return -2;
    }

    private float End()
    {
        return -1;
    }
    #endregion
}
