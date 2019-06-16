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

    public Transform transPlayer1;
    public Transform transPlayer2;
    public Transform transPlayer3;
    public Transform transPlayer4;

    public List<GameObject> prefabs = new List<GameObject>();
    public List<Color> colors = new List<Color>();

    // Start is called before the first frame update
    void Start() {
        ScoreManager.GetInstance().SetCanvasScore(this);
        UpdateScores();
        UpdatePlayers();
    }

    public void UpdateScores() {
        int players = PlayersManager.GetInstance().GetNumberOfPlayers();
        //Debug.Log("SCORE :: PLAYERS - " + players);
        //ScoreManager.GetInstance().GetPoints(1);
        if (players >= 1) txtScore1.text = "" + ScoreManager.GetInstance().GetPoints(1);
        else txtScore1.transform.parent.gameObject.SetActive(false);
        if (players >= 2) txtScore2.text = "" + ScoreManager.GetInstance().GetPoints(2);
        else txtScore2.transform.parent.gameObject.SetActive(false);
        if (players >= 3) txtScore3.text = "" + ScoreManager.GetInstance().GetPoints(3);
        else txtScore3.transform.parent.gameObject.SetActive(false);
        if (players >= 4) txtScore4.text = "" + ScoreManager.GetInstance().GetPoints(4);
        else txtScore4.transform.parent.gameObject.SetActive(false);
    }

    public void UpdatePlayers()
    {
        int players = PlayersManager.GetInstance().GetNumberOfPlayers();
        for (int i = 1; i < (players + 1); i++) {
            int character = 0;
            switch (PlayersManager.GetInstance().GetTableOfSelectPlayers(i).name) {
                case "Gallina": character = 2; break;
                case "Abeja": character = 0; break;
                case "Toro": character = 1; break;
                case "Mono": character = 3; break;
                default: character = 0; break;
            }
            GameObject obj = new GameObject();
            switch (i) {
                case 1:
                    obj = GameObject.Instantiate(prefabs[character], transPlayer1.parent);
                    obj.transform.SetPositionAndRotation(transPlayer1.position, transPlayer1.rotation);
                    obj.transform.localScale = transPlayer1.localScale;
                    break;
                case 2:
                    obj = GameObject.Instantiate(prefabs[character], transPlayer2.parent);
                    obj.transform.SetPositionAndRotation(transPlayer2.position, transPlayer2.rotation);
                    obj.transform.localScale = transPlayer2.localScale;
                    break;
                case 3:
                    obj = GameObject.Instantiate(prefabs[character], transPlayer3.parent);
                    obj.transform.SetPositionAndRotation(transPlayer3.position, transPlayer3.rotation);
                    obj.transform.localScale = transPlayer3.localScale;
                    break;
                case 4:
                    obj = GameObject.Instantiate(prefabs[character], transPlayer4.parent);
                    obj.transform.SetPositionAndRotation(transPlayer4.position, transPlayer4.rotation);
                    obj.transform.localScale = transPlayer4.localScale;
                    break;
                default: break;
            }
            obj.GetComponentInChildren<Renderer>().material.color = colors[i - 1];

        }
    }
}
