using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPush : MonoBehaviour
{

    private Vector3 positionRelativePJ;
    public GameObject parent;
    public Image coolDownImage;
    public Image forceCharge;
    private PushScript pushScript;
    // Start is called before the first frame update
    void Start()
    {
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

            if (forceCharge.fillAmount == 1 || pushScript.GetCurrentForce() == 0)
            {
                forceCharge.enabled = false;

            }
        }

    }

    public void StartBarCoolDown(PushScript _pushScript)
    {
        pushScript = _pushScript;
        coolDownImage.enabled = true;
    }

    public void StartBarForceCharge(PushScript _pushScript)
    {
        pushScript = _pushScript;
        forceCharge.enabled = true;
    }

}
