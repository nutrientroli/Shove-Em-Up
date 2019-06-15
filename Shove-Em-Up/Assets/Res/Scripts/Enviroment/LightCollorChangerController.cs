using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador que cambia el color de la luz emitida ademas del material que este usa
/// </summary>
public class LightCollorChangerController : MonoBehaviour
{

    private Material lightMaterial;
    private Light lightToManage;

    [SerializeField] private Color standardStateColor;
    [SerializeField] private Color triggerStateColor;

    public bool isOnTrigger = false;

    public void SetStandardState ()
    {
        SetLightColor(standardStateColor);
        isOnTrigger = false;
    }
    public void SetTriggerState ()
    {
        SetLightColor(triggerStateColor);
        isOnTrigger = true;
    }

    private void SetLightColor(Color _colorToSet)
    {
        // light
        lightToManage.color = _colorToSet;

        // material
        lightMaterial.SetColor("Emision", _colorToSet);

    }
 


    private void Awake()
    {
        lightToManage = GetComponentInChildren<Light>();
        lightMaterial = GetComponentInChildren<Renderer>().material;
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isOnTrigger)
                SetStandardState();
            else
                SetTriggerState();
        }
    }


    /*
    private IEnumerator FadeIn(float _time)
    {
        float increasePerSecond = (maxLightIntensity - currentBrightness) / _time;
        while (currentBrightness < maxLightIntensity)
        {
            lightToManage.intensity += increasePerSecond;
            currentBrightness = lightToManage.intensity;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator FadeOut(float _time)
    {
        float decreasePerSecond = (currentBrightness - minLightIntensity) / _time;
        while (currentBrightness > minLightIntensity)
        {
            lightToManage.intensity -= decreasePerSecond;
            currentBrightness = lightToManage.intensity;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    */
}
