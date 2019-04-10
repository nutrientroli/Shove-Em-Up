using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    private PlayerScript player;
    private float timeStopKnockback = 0;
    private float timeRelativeWithForce = 1;

    private bool canStop = true;

    private void Start()
    {
        player = GetComponent<PlayerScript>();
        if (player == null)
            Debug.Log("NO TIENE EL SCRIPT PLAYER EL PLAYER");
    }

    private void Update()
    {
        UpdateTimeKnockback(Time.deltaTime);
    }

    public void StartKnockback(float _currentForce, float _forceBase)
    {
        timeStopKnockback = (_currentForce / _forceBase) * timeRelativeWithForce;
        canStop = false;
    }

    private void UpdateTimeKnockback(float _time)
    {
        if(!canStop)
        {
            timeStopKnockback -= _time;
            if(timeStopKnockback <= 0)
            {
                timeStopKnockback = 0;
                canStop = true;
            }
        }
    }

}
