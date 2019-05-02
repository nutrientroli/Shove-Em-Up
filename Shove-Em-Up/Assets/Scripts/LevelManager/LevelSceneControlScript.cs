using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneControlScript : MonoBehaviour
{
    [SerializeField] private int maxPlayersWin = 1;


    //Clase que gestionará las vidas de cada player.

    // Start is called before the first frame update
    void Start() {
        LevelManager.GetInstance().Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.GetInstance().players <= maxPlayersWin) {
            LevelManager.GetInstance().FinnishGame();
        }
    }
}
