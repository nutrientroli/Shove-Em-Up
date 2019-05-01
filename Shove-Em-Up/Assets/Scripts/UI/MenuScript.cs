using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void OnButtonPlayPress() {
        SceneManager.LoadScene(1);
    }

    public void OnButtonQuitPress()
    {
        Debug.Log("Quit Game");
    }
}
