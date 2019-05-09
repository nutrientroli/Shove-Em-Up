using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasHudScript : MonoBehaviour
{
    public List<Image> playersHud = new List<Image>();
    private List<PlayerData> playersData = new List<PlayerData>();
    public List<Image> player1Live = new List<Image>();
    public List<Image> player2Live = new List<Image>();
    public List<Image> player3Live = new List<Image>();
    public List<Image> player4Live = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playersGo = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playersGo.Length; i++)
        {
            playersData.Add(playersGo[i].GetComponent<PlayerData>());
            playersData[i].canvas = this;
            switch(playersData[i].GetPlayer())
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeHud(PlayerData _pd)
    {

        switch (_pd.GetPlayer())
        {
            case 1:
        
                for (int i = 0; i < player1Live.Count; i++)
                {
                    player1Live[i].gameObject.SetActive(false);
                }
        
                for (int i = 0; i < _pd.GetLives(); i++)
                {
                    player1Live[i].gameObject.SetActive(true);
                }
                break;
            case 2:
                for (int i = 0; i < player2Live.Count; i++)
                {
                    player2Live[i].gameObject.SetActive(false);
                }
        
                for (int i = 0; i < _pd.GetLives(); i++)
                {
                    player2Live[i].gameObject.SetActive(true);
                }
                break;
            case 3:
                for (int i = 0; i < player3Live.Count; i++)
                {
                    player3Live[i].gameObject.SetActive(false);
                }
        
                for (int i = 0; i < _pd.GetLives(); i++)
                {
                    player3Live[i].gameObject.SetActive(true);
                }
                break;
            case 4:
                for (int i = 0; i < player4Live.Count; i++)
                {
                    player4Live[i].gameObject.SetActive(false);
                }
        
                for (int i = 0; i < _pd.GetLives(); i++)
                {
                    player4Live[i].gameObject.SetActive(true);
                }
                break;
                
        }
    }
}
