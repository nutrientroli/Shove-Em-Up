using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    private PlayerScript player;
    private float timeChargePush = 0;
    private float maxTimeChargePush = 1;
    private float timeCurrentCoolDownPush = 0;
    private float maxCoolDownPush = 1;

    private float forceBase = 20;
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
    }

    public void ChargePush(float _time)
    {
        if(timeChargePush < maxTimeChargePush)
        {
            timeChargePush += _time;
            if (timeChargePush > maxTimeChargePush)
                timeChargePush = maxTimeChargePush;
        }
    }

    public void Push()
    {
        currentForce = forceBase * (1 + timeChargePush / maxTimeChargePush);
        player.Push(currentForce);
        canPush = false;
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
