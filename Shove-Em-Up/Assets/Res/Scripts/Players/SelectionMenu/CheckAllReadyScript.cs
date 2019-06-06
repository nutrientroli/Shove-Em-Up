using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckAllReadyScript : MonoBehaviour
{
    #region Attributes
    [Header("Test Value")] [SerializeField] private int playerToReady = 1;

    [Header("Configuration")]
    [SerializeField] private List<PlayerSelectionScript> listOfPlayers;
    private bool allready = false;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private string textCounter;
    [SerializeField] private float timeToStart = 3;
    private float currentTime;
    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        if(Input.GetJoystickNames().Length > 0) {
            playerToReady = Input.GetJoystickNames().Length;
            if (playerToReady > 4) playerToReady = 4;
        } else {
            //No hay mandos conectados.
        }
    }

    private void Update() {
        if (!allready) {
            if (/*CheckReady() ||*/ CheckReadyTest()) AllReady();
        } else {
            if (/*!CheckReady() || */!CheckReadyTest()) StopReady();
            UpdateCountDown(Time.deltaTime);
        }
    }
    #endregion

    #region CheckAllReady Methods
    private void StopReady() {
        allready = false;
        counter.gameObject.SetActive(false);
    }

    private void AllReady() {
        allready = true;
        currentTime = timeToStart;
        counter.gameObject.SetActive(true);
    }

    private bool CheckReady() {
        bool report = true;
        for (int i = 0; i < listOfPlayers.Count; i++) {
            if (report && !listOfPlayers[i].GetReady()) report = false;
        }
        return report;
    }

    private void UpdateCountDown(float _time) {
        currentTime -= _time;
        counter.text = textCounter + currentTime.ToString("0");
        if (currentTime <= 0) ScenesManager.ChangeScene(ScenesManager.SceneCode.GAME);
    }

    private bool CheckReadyTest() {
        int check = 0;
        for (int i = 0; i < listOfPlayers.Count; i++) {
            if (listOfPlayers[i].GetReady()) check++;
        }
        return check >= playerToReady;
    }
    #endregion
}
