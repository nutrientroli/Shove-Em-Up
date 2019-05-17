using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    SHIELD = 0,
    DASH = 1,
    MONKEY = 2,
    OTHER = 3
}


public class PlayerData : MonoBehaviour {
    //Clase que se rellena con el Scriptable Object.

    private int player;
    private bool hudPuesto = false;
    private int score = 0;
    public GameObject canvasPoints;
    public CanvasHudScript canvas;
    private PlayerType typePlayer;

    public void SetPlayer(int _player) {
        player = _player;
    }

    public int GetPlayer() {
        return player;
    }

    public int GetScore()
    {
        return score;
    }
    
    public void AddScore(int _score = 1)
    {
        score += _score;
        GameObject go;
        go = Instantiate(canvasPoints, gameObject.transform.position + new Vector3(0, 4f, 0), canvasPoints.transform.rotation);
        go.GetComponent<CanvasPoints>().Init(_score,gameObject);
        print("player: " + player + "  Score:  " + score);
        ScoreManager.GetInstance().SetPoints(player, score);
    }


    public void SetTypePlayer(PlayerType _type) {
        typePlayer = _type;
    }

    public PlayerType GetTypePlayer() {
        return typePlayer;
    }

}
