using System.Collections.Generic;
using UnityEngine;

public class CombinateEventPlatform : EventPlatformScript
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private List<MeteorScript> meteors = new List<MeteorScript>();
    public List<FanScript> ventiladores;
    private GameObject[] players;
    public GameObject posicionCentral;
    private int randomNum = 0;

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
        randomNum = Random.Range(0, ventiladores.Count);
        state = 0; //actualmente solo hay 1 combinación
        if (state == 0)
        {

            GameObject[] meteorGO;
            meteorGO = GameObject.FindGameObjectsWithTag("Meteor");
            players = GameObject.FindGameObjectsWithTag("Player");


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

                    int randomNum = 0;
                    bool vivo = true;
                    bool valido = false;
                    int iteraciones = 0;
                    while (!valido)
                    {
                        iteraciones++;
                        vivo = true;
                        randomNum = Random.Range(0, players.Length);
                        print(randomNum);
                        foreach (int j in PlayersManager.GetInstance().listOfPlayersToRespawnFinnishEvent)
                        {
                            if (players[randomNum].GetComponent<PlayerData>().GetPlayer() == j)
                            {
                                vivo = false;
                            }
                            if (players.Length == PlayersManager.GetInstance().listOfPlayersToRespawnFinnishEvent.Count)
                                valido = true;

                            if ((posicionCentral.transform.position - players[randomNum].transform.position).magnitude >= 14)
                                vivo = false;
                        }
                        if (vivo || iteraciones >= 50)
                            valido = true;

                    }
                    if (players.Length == PlayersManager.GetInstance().listOfPlayersToRespawnFinnishEvent.Count || iteraciones >= 50)
                    {
                        meteors[i].gameObject.transform.position = new Vector3(posicionCentral.transform.position.x + Random.Range(-12, 12), meteors[i].transform.position.y, posicionCentral.transform.position.z + Random.Range(-15, 16));
                    }
                    else
                        meteors[i].gameObject.transform.position = new Vector3(players[randomNum].transform.position.x, meteors[i].transform.position.y, players[randomNum].transform.position.z);
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
            ventiladores[randomNum].Active();
        }
        return 0;
    }

    private float DestroyFan()
    {
        ventiladores[randomNum].Hide();
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
        ventiladores[randomNum].Hide();
        return -1;
    }
    #endregion
}
