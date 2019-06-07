using System.Collections.Generic;
using UnityEngine;

public class FallDownEventPlatform : EventPlatformScript 
{
    #region Variables
    [Header("Objects Configuration")] //Crear Tipo de objeto con el GameObject y tier?
    [SerializeField] private List<GameObject> tier1Pieces;
    [SerializeField] private List<GameObject> tier2Pieces;
    [SerializeField] private List<GameObject> tier3Pieces;
    private GameObject randomPiece;

    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float timeToAction = 5.0f;

    [Header("Extra Configuration")]
    public float timeVariaton = 1.0f;
    #endregion

    #region ParentFunctions
    public override void Init() {
        base.Init();
        type = TypeEvent.TIME;
        int len = (tier1Pieces.Count + tier2Pieces.Count + tier3Pieces.Count) * 2;
        listEvent.Add(Wait);
        for (int i = 0; i < len; i++) {
            if(i%2 == 0) listEvent.Add(FeedBack);
            else listEvent.Add(Action);
        }
    }
    #endregion

    #region EventFunctions
    private float FeedBack() {
        GetPieceToDown();
        randomPiece.GetComponent<Renderer>().material.color = Color.red;
        return timeToAction * timeVariaton;
    }

    private float Action() {
        Destroy(randomPiece);
        return timeToAction * timeVariaton;
    }

    private float Wait()
    {
        return waitTime;
    }
    #endregion

    #region CustomFunctions
    private void GetPieceToDown()
    {
        if (tier1Pieces.Count > 0)
        {
            int random = Random.Range(0, tier1Pieces.Count - 1);
            randomPiece = tier1Pieces[random];
            tier1Pieces.RemoveAt(random);
        }
        else if (tier2Pieces.Count > 0)
        {
            int random = Random.Range(0, tier2Pieces.Count - 1);
            randomPiece = tier2Pieces[random];
            tier2Pieces.RemoveAt(random);
        }
        else if (tier3Pieces.Count > 0)
        {
            int random = Random.Range(0, tier3Pieces.Count - 1);
            randomPiece = tier3Pieces[random];
            tier3Pieces.RemoveAt(random);
        }
    }
    #endregion
}
