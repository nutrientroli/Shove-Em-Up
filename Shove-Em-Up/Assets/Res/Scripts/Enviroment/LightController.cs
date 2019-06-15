using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // references
    [SerializeField] private Light lightToManage;

    private float currentBrightness;
    [SerializeField] private float minLightIntensity, maxLightIntensity;

    [SerializeField] private bool useCurrentAsMaxIntensity = true;



    public void EnableLight(float _time)
    {
        StartCoroutine(FadeIn(_time));
    }
    public void DisableLight(float _time)
    {
        StartCoroutine(FadeOut(_time));
    }


    private void Awake()
    {

        lightToManage = GetComponentInChildren<Light>();

        currentBrightness = lightToManage.intensity;

        if (useCurrentAsMaxIntensity)
            maxLightIntensity = currentBrightness;

        if (minLightIntensity == maxLightIntensity)
            Debug.LogError("LIGHTS_: El valor de emisividad minimo y maximo no pueden ser el mismo");

    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        if (currentBrightness == minLightIntensity)
    //            EnableLight(5f);
    //        else if (currentBrightness == maxLightIntensity)
    //            DisableLight(5f);
    //    }
    //}

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

    private IEnumerator FadeOut (float _time)
    {
        float decreasePerSecond = (currentBrightness - minLightIntensity) / _time;
        while (currentBrightness > minLightIntensity)
        {
            Debug.Log(decreasePerSecond);

            lightToManage.intensity -= decreasePerSecond;
            currentBrightness = lightToManage.intensity;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }


}
