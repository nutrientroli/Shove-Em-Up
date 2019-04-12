using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPush : MonoBehaviour
{

    private Vector3 positionRelativePJ;
    public GameObject parent;
    public Image coolDownImage;
    private PushScript pushScript;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        positionRelativePJ = gameObject.transform.position - parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            gameObject.transform.position = parent.transform.position + positionRelativePJ;
            coolDownImage.fillAmount = pushScript.GetCurrentCoolDownPush() / pushScript.GetMaxCoolDownPush();
            if (coolDownImage.fillAmount == 1)
            {
                start = false;
                coolDownImage.enabled = false;
            }
        }
    }

    public void StartBar(PushScript _pushScript)
    {
        pushScript = _pushScript;
        coolDownImage.enabled = true;
        start = true;
    }
}
