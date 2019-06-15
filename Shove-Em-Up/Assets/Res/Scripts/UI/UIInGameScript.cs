using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameScript : MonoBehaviour
{
    public TextMeshProUGUI txtScore1;
    public TextMeshProUGUI txtScore2;
    public TextMeshProUGUI txtScore3;
    public TextMeshProUGUI txtScore4;

    // Start is called before the first frame update
    void Start() {
        ScoreManager.GetInstance().SetCanvasScore(this);
    }

    public void UpdateScores() {
        int players = PlayersManager.GetInstance().GetNumberOfPlayers();
        //Debug.Log("SCORE :: PLAYERS - " + players);
        //ScoreManager.GetInstance().GetPoints(1);
        if (players >= 1) txtScore1.text = "" + ScoreManager.GetInstance().GetPoints(1);
        if (players >= 2) txtScore2.text = "" + ScoreManager.GetInstance().GetPoints(2);
        if (players >= 3) txtScore3.text = "" + ScoreManager.GetInstance().GetPoints(3);
        if (players >= 4) txtScore4.text = "" + ScoreManager.GetInstance().GetPoints(4);
    }
}
