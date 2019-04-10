using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    private PlayerScript player;
    private float timeChargePush = 0;
    private float maxTimeChargePush = 1;
    private float timeCurrentCoolDownPush = 0;
    private float maxCoolDownPush = 0.5f;

    private float forceBase = 0.15f;
    private float currentForce = 0;

    private bool canPush = true;

    private void Start()
    {
        player = GetComponent<PlayerScript>();
        if (player == null)
            Debug.Log("NO TIENE EL SCRIPT PLAYER EL PLAYER");
    }

    private void Update()
    {
        UpdateCoolDownPush(Time.deltaTime);
        if (timeChargePush > 0)
            ChargePush(Time.deltaTime);
    }

    public void ChargePush(float _time)
    {
        if (timeChargePush < maxTimeChargePush)
        {
            timeChargePush += _time;
            if (timeChargePush > maxTimeChargePush)
                timeChargePush = maxTimeChargePush;
        }

    }

    public float Push()
    {
        currentForce = (1 + timeChargePush / (maxTimeChargePush - maxTimeChargePush/4)) * forceBase;
        canPush = false;
        timeChargePush = 0;
        return currentForce;
    }

    private void UpdateCoolDownPush(float _time)
    {
        if (!canPush)
        {
            timeCurrentCoolDownPush += _time;
            if (timeCurrentCoolDownPush >= maxCoolDownPush)
            {
                canPush = true;
                timeCurrentCoolDownPush = 0;
            }
        }
    }

    public bool CanPush()
    {
        return canPush;
    }
}
