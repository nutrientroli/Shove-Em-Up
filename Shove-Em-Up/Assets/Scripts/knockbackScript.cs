using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    private float timeStopKnockback = 0;
    private float timeRelativeWithForce = 1;

    private bool cantStop = true;

    private void Update()
    {
        UpdateTimeKnockback(Time.deltaTime);
    }

    public void StartKnockback(float _currentForce, float _forceBase)
    {
        timeStopKnockback = (_currentForce / _forceBase) * timeRelativeWithForce;
        cantStop = false;
    }

    private void UpdateTimeKnockback(float _time)
    {
        if(!cantStop)
        {
            timeStopKnockback -= _time;
            if(timeStopKnockback <= 0)
            {
                timeStopKnockback = 0;
                cantStop = true;
            }
        }
    }

}
