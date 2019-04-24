using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierScript:MonoBehaviour
{
    protected float currentTime = 0;
    protected float maxTime = 3;
    public bool isMovible = false;

    protected virtual void Init()
    {

    }


    public bool CheckModifier(float _deltaTime)
    {
        currentTime += _deltaTime;
        return currentTime >= maxTime;
    }
}
