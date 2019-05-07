using System.Collections.Generic;
using UnityEngine;

public class FallRuleteEventPlatform : EventPlatformScript
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private List<PieceRuleteScript> pieces = new List<PieceRuleteScript>();
    private int randomNum = 0;
    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 0.3f;
    [SerializeField] private float timeToAction = 2f;
    [Header("Extra Configuration")]
    public float timeVariaton = 1.5f;
    [SerializeField] private int pool = 5;
    #endregion

    #region ParentFunctions
    public override void Init()
    {
        base.Init();
        GameObject[] meteorGO;
        meteorGO = GameObject.FindGameObjectsWithTag("Meteor");
        randomNum = Random.Range(0, pieces.Count - 1);
        pool = pieces.Count;
        type = TypeEvent.TIME;

        listEvent.Add(Action);
        listEvent.Add(Restart);

    }
    #endregion

    #region EventFunctions


    private float Action()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if(i != randomNum)
                pieces[i].Activate();
        }

        return timeToAction * timeVariaton;
    }

    private float Wait()
    {
        return waitTime;
    }

    private float Restart()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (i != randomNum)
                pieces[i].Reverted();
        }

        return -2;
    }
    #endregion
}
