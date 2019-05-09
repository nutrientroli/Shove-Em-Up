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
    private int lives = 3;
    private bool hudPuesto = false;
    public CanvasHudScript canvas;
    private PlayerType typePlayer;

    public void SetPlayer(int _player) {
        player = _player;
    }

    public int GetPlayer() {
        return player;
    }

    public void SetLives(int _lives) {
        lives = _lives;
        canvas.ChangeHud(this);
    }

    public int GetLives() {
        return lives;
    }

    public void SetTypePlayer(PlayerType _type) {
        typePlayer = _type;
    }

    public PlayerType GetTypePlayer() {
        return typePlayer;
    }
}
