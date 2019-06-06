using System;
using System.Collections.Generic;
using UnityEngine;

public class FallRuleteEventPlatform : EventPlatformScript
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private List<PieceRuleteScript> pieces = new List<PieceRuleteScript>();
    private List<Renderer> listRenderPieces = new List<Renderer>();
    public Material selectMaterialFeedback;
    private int randomNum1 = 0;
    private int randomNum2 = 0;
    private int randomNum3 = 0;
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

        for (int i=0; i<pieces.Count; i++) {
            Renderer ren = pieces[i].GetComponent<Renderer>();
            if(ren != null) listRenderPieces.Add(ren);
        }
       
        pool = pieces.Count;
        type = TypeEvent.TIME;

        listEvent.Add(GetNumberRandom);
        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(Restart);
        listEvent.Add(Restore);
        listEvent.Add(GetNumberRandom);
        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(Restart);
        listEvent.Add(Restore);
        listEvent.Add(GetNumberRandom);
        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(Restart);
        listEvent.Add(Restore);
        listEvent.Add(Wait);
        listEvent.Add(End);
    }
    public override void ForceFinnish()
    {
        base.ForceFinnish();
        listEvent.Add(Restart);
        listEvent.Add(Restore);
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions
    private float Restore()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].GetComponent<Renderer>().material = ((PieceRuleteScript)pieces[i]).mat;
        }
        return waitTime;
    }

    private float Action()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if(i != randomNum1 && i != randomNum2 && i != randomNum3)
                pieces[i].Activate();
        }

        return timeToAction * timeVariaton;
    }

    private float Wait()
    {
        return waitTime;
    }
    private float GetNumberRandom()
    {
        randomNum1 = UnityEngine.Random.Range(0, pieces.Count - 1);
        randomNum2 = UnityEngine.Random.Range(0, pieces.Count - 1);
        randomNum3 = UnityEngine.Random.Range(0, pieces.Count - 1);

        while (randomNum1 == randomNum2) randomNum2 = UnityEngine.Random.Range(0, pieces.Count - 1);
        while (randomNum1 == randomNum3 || randomNum2 == randomNum3) randomNum3 = UnityEngine.Random.Range(0, pieces.Count - 1);

        for (int i=0; i<pieces.Count; i++) {
            if(i != randomNum1 && i != randomNum2 && i != randomNum3) pieces[i].GetComponent<Renderer>().material = selectMaterialFeedback;
        }

        return waitTime;
    }
    private float Restart()
    {

        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].GetComponent<Renderer>().material = ((PieceRuleteScript)pieces[i]).mat;
            if (i != randomNum1 && i != randomNum2 && i != randomNum3)
                pieces[i].Reverted();
        }

        return -2;
    }
    private float End() {
        
        return -1;
    }

    #endregion
}
