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
    private float maxTime = 1.0f;
    private float currentTime = 0;
    private float intens = 0;


    private void Awake() {
        lightToManage = GetComponentInChildren<Light>();
        lightMaterial = GetComponentInChildren<Renderer>().material;
        intens = lightToManage.intensity;
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
        SetLightColor(triggerStateColor, true);
    }

    private void SetLightColor(Color _colorToSet, bool intensified = false) {
        lightToManage.color = _colorToSet;
        lightMaterial.SetColor("_EmissionColor", _colorToSet);
        if (intensified) lightToManage.intensity = intens * 6;
        else lightToManage.intensity = intens;
    }

    public void DeadPlayer() {
        SetTriggerState();
        currentTime = 0;
        isOn = true;
    }
}
