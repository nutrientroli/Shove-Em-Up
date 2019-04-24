﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPush : MonoBehaviour
{

    private Vector3 positionRelativePJ;
    public GameObject parent;
    public Image coolDownImage;
    public Image forceCharge;
    public Image TimeChargeHability;
    private PushScript pushScript;
    private HabilityScript habilityScript;
    private Color firstColor;
    // Start is called before the first frame update
    void Start()
    {
        firstColor = TimeChargeHability.color;
        positionRelativePJ = gameObject.transform.position - parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = parent.transform.position + positionRelativePJ;
        if (pushScript != null)
        {
            coolDownImage.fillAmount = pushScript.GetCurrentCoolDownPush() / pushScript.GetMaxCoolDownPush();
            forceCharge.fillAmount = pushScript.GetCurrentForce() / pushScript.GetMaxForce();

            if (coolDownImage.fillAmount == 1)
            {
                coolDownImage.enabled = false;
            }

            if (pushScript.GetCurrentForce() == 0)
            {
                forceCharge.enabled = false;

            }
        }
        if(habilityScript != null)
        {
            TimeChargeHability.fillAmount = habilityScript.GetCurrentEnergy() / habilityScript.GetMaxEnergy();
            if (TimeChargeHability.fillAmount < 1)
                TimeChargeHability.color = firstColor;
            else
                TimeChargeHability.color = Color.yellow;
        }

    }

    public void StartBarCoolDown(PushScript _pushScript)
    {
        if(pushScript == null)
            pushScript = _pushScript;
        coolDownImage.enabled = true;
    }

    public void StartBarForceCharge(PushScript _pushScript)
    {
        if(pushScript == null)
            pushScript = _pushScript;
        forceCharge.enabled = true;
    }

    public void StartBarHability(HabilityScript _habilityScript)
    {
        if (habilityScript == null)
        {
            habilityScript = _habilityScript;
            TimeChargeHability.enabled = true;
        }
    }

}
