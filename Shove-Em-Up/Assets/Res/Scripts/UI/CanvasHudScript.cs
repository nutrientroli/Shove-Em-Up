using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasHudScript : MonoBehaviour
{
    public List<Image> playersHud = new List<Image>();
    private List<PlayerData> playersData = new List<PlayerData>();
    public List<Text> playerScore = new List<Text>();

    private bool used = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!used)
        {
            GameObject[] playersGo = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < playersGo.Length; i++)
            {
                playersData.Add(playersGo[i].GetComponent<PlayerData>());
                playersData[i].canvas = this;
                switch (playersData[i].GetPlayer())
                {
                    case 1:
                        playersHud[0].gameObject.SetActive(true);
                        break;
                    case 2:
                        playersHud[1].gameObject.SetActive(true);
                        break;
                    case 3:
                        playersHud[2].gameObject.SetActive(true);
                        break;
                    case 4:
                        playersHud[3].gameObject.SetActive(true);
                        break;
                }
            }
            used = true;
        }
    }

    public void AddScore(int _player, int _score)
    {

        if (playersHud[_player - 1].gameObject.activeSelf)
        {
            playerScore[_player - 1].text = _score.ToString();
        }
        
    }
}
