using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayer : MonoBehaviour
{
    private Color originalColor;
    private float currentTime = 0;
    private Light light;
    // Start is called before the first frame update
    void Awake()
    {
        light = gameObject.GetComponent<Light>();
        originalColor = light.color;
    }

    private void Update()
    {
        if(currentTime > 0)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 1)
                light.enabled = false;
        }
    }



    public void DefaultLight()
    {
        light.color = originalColor;
        light.enabled = true;
        currentTime = 0;
    }

    public void RedLight()
    {
        light.color = Color.red;
        currentTime = Time.deltaTime;
    }
}
