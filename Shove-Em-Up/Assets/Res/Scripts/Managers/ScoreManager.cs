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
    private UIInGameScript canvasScore;

    public void Init() {
        GameObject[] _players;
        _players = GameObject.FindGameObjectsWithTag("Player");
        if (_players != null && _players.Length>0) {
            foreach (GameObject p in _players) {
                if (p.GetComponent<PlayerData>() != null) players.Add(0);
                Debug.Log("PLAYER ENCONTRADO");
            }
        } else {
            //for(int i=0; i<4; i++) players.Add(0);
            players.Add(5);
            players.Add(20);
            players.Add(50);
            players.Add(17);
        }
    }

    public void SetPoints(int _player, int _score) {
        if (players != null && players.Count != 0) players[_player - 1] = _score;
        if(canvasScore != null) canvasScore.UpdateScores();
    }

    public int GetPoints(int _player) {
        int _score = 0;
        if (players != null && players.Count != 0) _score = players[_player - 1];
        return _score;
    }

    public int GetMaxPoints() {
        int _score = 0;
        if (players.Count > 0) {
            for (int i = 0; i < players.Count; i++) {
                if (_score < players[i]) _score = players[i];
            }
        }
        return _score;
    }

    public void SetCanvasScore(UIInGameScript _canvasScore) {
        Debug.Log("SETEO CANVAS SCORE");
        canvasScore = _canvasScore;
    }
}
