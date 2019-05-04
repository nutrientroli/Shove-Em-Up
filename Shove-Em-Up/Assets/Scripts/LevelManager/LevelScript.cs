using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] List<GameObject> Characters = new List<GameObject>();
    // Start is called before the first frame update
    void Start() {
        PlayersManager.GetInstance().SetListOfCharacters(Characters);
        PlayersManager.GetInstance().Init();
    }
}
