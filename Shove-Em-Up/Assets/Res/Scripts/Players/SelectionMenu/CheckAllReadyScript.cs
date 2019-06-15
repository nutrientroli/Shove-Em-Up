using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckAllReadyScript : MonoBehaviour
{
    #region Attributes
    [Header("Configuration")]
    [SerializeField] private int playerToReady = 1;
    [SerializeField] private List<PlayerSelectionIntegratedScript> listOfPlayers;
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
            if (CheckReady()) AllReady();
        } else {
            if (!CheckReady()) StopReady();
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
        PublicoManager.GetInstance().Reset();
        allready = true;
        currentTime = timeToStart;
        counter.gameObject.SetActive(true);
        if (Random.Range(0, 10) > 5) PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_33); //SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_33);
        else PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_31); //SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_31);
    }

    private void UpdateCountDown(float _time) {
        currentTime -= _time;
        counter.text = textCounter + currentTime.ToString("0");
        if (currentTime <= 0) ScenesManager.ChangeScene(ScenesManager.SceneCode.GAME);
    }

    private bool CheckReady() {
        int check = 0;
        for (int i = 0; i < listOfPlayers.Count; i++) {
            if (listOfPlayers[i].GetReady()) check++;
        }
        return check >= playerToReady;
    }
    #endregion
}
