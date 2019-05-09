using System.Collections.Generic;
using UnityEngine;

public class FallRuleteEventPlatform : EventPlatformScript
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private List<PieceRuleteScript> pieces = new List<PieceRuleteScript>();
    private int randomNum = 0;
    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private float timeToAction = 2f;
    [Header("Extra Configuration")]
    public float timeVariaton = 1.5f;
    [SerializeField] private int pool = 5;
    #endregion

    #region ParentFunctions
    public override void Init()
    {
        base.Init();
        randomNum = Random.Range(0, pieces.Count - 1);
        pool = pieces.Count;
        type = TypeEvent.TIME;

        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(Restart);
        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(Restart);
        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(Restart);


        listEvent.Add(End);
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
    private float End() {
        return -1;
    }
    #endregion
}
