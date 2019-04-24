using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierScript:MonoBehaviour
{
    protected float currentTime = 0;
    protected float maxTime;
    public bool isMovible;
    public bool inverted;

    protected virtual void Init()
    {
        maxTime = 3;
        isMovible = true;
        inverted = false;
    }


    public bool CheckModifier(float _deltaTime)
    {
        currentTime += _deltaTime;
        return currentTime >= maxTime;
    }
}
