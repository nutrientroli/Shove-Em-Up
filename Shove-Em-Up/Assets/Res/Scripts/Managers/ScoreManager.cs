using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{

    #region Singleton Pattern
    private static ScoreManager instance;
    private ScoreManager() { }
    public static ScoreManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ScoreManager();
            instance.Init();
        }
        return instance;
    }
    #endregion
    private List<int> players = new List<int>(); 

    public void Init()
    {
        GameObject[] _players;
        _players = GameObject.FindGameObjectsWithTag("Player");

        //Mejorar rendimiento. Hastable en PlayersManagers


        if (_players != null && _players.Length>0) {
            foreach (GameObject p in _players) {
                if (p.GetComponent<PlayerData>() != null) players.Add(0);
            }
        } else {
            //for(int i=0; i<4; i++) players.Add(0);
            players.Add(5);
            players.Add(20);
            players.Add(50);
            players.Add(17);
        }
    }

    public void SetPoints(int _player, int score)
    {
        players[_player - 1] = score;
    }

    public int GetPoints(int _player)
    {
        int score = 0;
        if (players != null) score = players[_player - 1];

        /*Test*/
        /*switch (_player) {
            case 1: score = 50; break;
            case 2: score = 100; break;
            case 3: score = 30; break;
            case 4: score = 50; break;
        }*/
        return score;
    }

    public int GetMaxPoints() {
        int score = 0;
        if (players.Count > 0) {
            for (int i = 0; i < players.Count; i++) {
                if (score < players[i]) score = players[i];
            }
        }
        //test
        //return 100;
        return score;
    }
}
