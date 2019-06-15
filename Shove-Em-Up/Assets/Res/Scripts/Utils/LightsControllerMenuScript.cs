using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsControllerMenuScript : MonoBehaviour
{
    public List<LightController> lights = new List<LightController>();
    public bool darkness = false;
    private bool firstCall = true;

    private void Update() {
        if(darkness && firstCall) {
            firstCall = false;
            FadeOut();
        } else if(!darkness && !firstCall) {
            firstCall = true;
            FadeIn();
        }
    }

    private void FadeOut() {
        foreach(LightController _light in lights) _light.DisableLight(15.0f);
    }

    private void FadeIn() {
        foreach (LightController _light in lights) _light.EnableLight(15.0f);
    }
}
