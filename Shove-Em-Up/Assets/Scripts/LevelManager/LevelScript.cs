using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] List<GameObject> Characters = new List<GameObject>();
    [SerializeField] int playersVictory;
    [SerializeField] int players;

    // Start is called before the first frame update
    private void Start() {
        PlayersManager.GetInstance().SetListOfCharacters(Characters);
        PlayersManager.GetInstance().Init();
        LevelManager.GetInstance().playersToWin = playersVictory;
        LevelManager.GetInstance().players = players;
    }
}
