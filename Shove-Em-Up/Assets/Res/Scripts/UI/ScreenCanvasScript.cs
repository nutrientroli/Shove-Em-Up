using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCanvasScript : MonoBehaviour
{
    public GameObject txtMeteorites;
    public GameObject txtFallRoulette;
    public GameObject txtFan;
    public GameObject txtDoubleEvent;
    public GameObject txtItems;
    public GameObject txtSmoke;
    public GameObject txtEnd;

    private CameraScript mcamera;

    private void Awake() {
        mcamera = Camera.main.gameObject.GetComponent<CameraScript>();
    }

    public void SetEvent(int _indexEvent) {
        HideAll();
        switch (_indexEvent) {
            case 0: txtItems.SetActive(true); break;
            case 1: txtDoubleEvent.SetActive(true); break;
            case 2: txtFan.SetActive(true); break;
            case 3: txtMeteorites.SetActive(true); break;
            case 4: txtSmoke.SetActive(true); break;
            case 5: txtFallRoulette.SetActive(true); break;
        }
    }

    public void SetEndGame() {
        HideAll();
        StartCoroutine(EndGame());
    }

    public void HideAll() {
        txtMeteorites.SetActive(false);
        txtFallRoulette.SetActive(false);
        txtFan.SetActive(false);
        txtDoubleEvent.SetActive(false);
        txtItems.SetActive(false);
        txtSmoke.SetActive(false);
        txtEnd.SetActive(false);
    }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(5f);
        txtEnd.SetActive(true);
        mcamera.PlayAnimationEndGame();
    }
}
