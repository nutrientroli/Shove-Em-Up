using System.Collections.Generic;
using UnityEngine;

public class FallDownEventPlatform : EventPlatformScript 
{
    //Temporal
    public List<GameObject> tier1Pieces;
    public List<GameObject> tier2Pieces;
    public List<GameObject> tier3Pieces;
    //

    public float adjustTimer = 1.0f;
    public float timetoFallDown = 5.0f;

    private GameObject randomPiece;

    public override void Init() {
        base.Init();
        type = TypeEvent.TIME;
        int len = (tier1Pieces.Count + tier2Pieces.Count + tier3Pieces.Count) * 2;
        for (int i = 0; i < len; i++) {
            if(i%2 == 0) listEvent.Add(FallDownFeedBack);
            else listEvent.Add(FallDown);
        }
    }

    private void GetPieceToDown() {
        if (tier1Pieces.Count > 0) {
            int random = Random.Range(0, tier1Pieces.Count - 1);
            randomPiece = tier1Pieces[random];
            tier1Pieces.RemoveAt(random);
        } else if (tier2Pieces.Count > 0) {
            int random = Random.Range(0, tier2Pieces.Count - 1);
            randomPiece = tier2Pieces[random];
            tier2Pieces.RemoveAt(random);
        } else if (tier3Pieces.Count > 0) {
            int random = Random.Range(0, tier3Pieces.Count - 1);
            randomPiece = tier3Pieces[random];
            tier3Pieces.RemoveAt(random);
        }
    }


    private float FallDownFeedBack() {
        GetPieceToDown();
        randomPiece.GetComponent<Renderer>().material.color = Color.red;
        return timetoFallDown * adjustTimer;
    }

    private float FallDown() {
        Destroy(randomPiece);
        return timetoFallDown * adjustTimer;
    }
}
