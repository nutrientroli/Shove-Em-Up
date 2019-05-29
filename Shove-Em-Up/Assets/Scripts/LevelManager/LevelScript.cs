using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> Characters = new List<GameObject>();
    [SerializeField] private int eventsToFinnish;
    [SerializeField] private Transform initPosToRespawn;
    // Start is called before the first frame update
    private void Start() {
        PlayersManager.GetInstance().SetRespawnPoint(initPosToRespawn);
        PlayersManager.GetInstance().SetListOfCharacters(Characters);
        PlayersManager.GetInstance().Init();
        LevelManager.GetInstance().ResetGame(eventsToFinnish);
    }
}
