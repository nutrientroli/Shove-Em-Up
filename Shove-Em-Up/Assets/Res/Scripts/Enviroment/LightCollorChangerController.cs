using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollorChangerController : MonoBehaviour
{

    private Material lightMaterial;
    private Light lightToManage;
    [SerializeField] private Color standardStateColor;
    [SerializeField] private Color triggerStateColor;
    private bool isOn = false;
    private float maxTime = 2.0f;
    private float currentTime = 0;


    private void Awake() {
        lightToManage = GetComponentInChildren<Light>();
        lightMaterial = GetComponentInChildren<Renderer>().material;
    }

    private void Update() {
        if (isOn) {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime) {
                currentTime = 0;
                SetStandardState();
                isOn = false;
            }
        }
    }


    private void SetStandardState() {
        SetLightColor(standardStateColor);
    }

    private void SetTriggerState() {
        SetLightColor(triggerStateColor);
    }

    private void SetLightColor(Color _colorToSet) {
        lightToManage.color = _colorToSet;
        lightMaterial.SetColor("_EmissionColor", _colorToSet);
    }

    public void DeadPlayer() {
        SetTriggerState();
        currentTime = 0;
        isOn = true;
    }
}
