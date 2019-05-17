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
        
        foreach(GameObject p in _players)
        {
            if(p.GetComponent<PlayerData>() != null)
                players.Add(0);
        }
    }

    public void SetPoints(int _player, int score)
    {
        players[_player] = score;
    }
}
