using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        LevelManagerScript.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManagerScript.players <= 1) {
            LevelManagerScript.Restart();
        }
    }
}
