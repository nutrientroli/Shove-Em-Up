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
    public Image TimeChargeHability;
    public Image hability;
    public Text confused;
    private PushScript pushScript;
    private HabilityScript habilityScript;
    private Color firstColor;
    public List<Sprite> sprites;
    public PlayerData playerData;
    public ParticleSystem particleHability;
    // Start is called before the first frame update
    void Start()
    {
        switch(playerData.GetPlayer())
        {
            case 1:
                TimeChargeHability.color = Color.red;
                particleHability.startColor = Color.red;
                break;
            case 2:
                TimeChargeHability.color = Color.blue;
                particleHability.startColor = Color.blue;
                break;
            case 3:
                TimeChargeHability.color = Color.green;
                particleHability.startColor = Color.green;
                break;
            case 4:
                TimeChargeHability.color = Color.yellow;
                particleHability.startColor = Color.yellow;
                break;
        }
        firstColor = TimeChargeHability.color;
        positionRelativePJ = gameObject.transform.position - parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = parent.transform.position + positionRelativePJ;
        if (pushScript != null)
        {
            coolDownImage.fillAmount =  1 - pushScript.GetCurrentCoolDownPush() / pushScript.GetMaxCoolDownPush();
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
            {
                if (particleHability.isPlaying)
                {
                    TimeChargeHability.color = firstColor;
                    particleHability.Stop();
                }
            }
            else if (!particleHability.isPlaying)
            {
                particleHability.Play();
            }
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

    public void StatusConfused(bool _confused)
    {
        confused.enabled = _confused;
    }

    public void SetDashHability()
    {
        if (sprites.Count >= 1)
            if(hability != null) hability.sprite = sprites[0];
    }

    public void SetToxicityHability()
    {
        if (sprites.Count >= 2)
            if (hability != null) hability.sprite = sprites[1];
    }

    public void SetShieldHability()
    {
        if (sprites.Count >= 3)
            if (hability != null) hability.sprite = sprites[2];
    }

}
