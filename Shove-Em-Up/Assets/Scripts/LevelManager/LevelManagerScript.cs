using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManagerScript
{
    public static int players;

    public static void Init()
    {
        players = 4;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
