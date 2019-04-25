using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierScript:MonoBehaviour
{
    protected float currentTime = 0;
    protected float maxTime;
    public bool isMovible;
    public bool inverted;
    public bool isPushable;

    protected virtual void Init()
    {
        maxTime = 3;
        isMovible = true;
        inverted = false;
        isPushable = true;
    }


    public bool CheckModifier(float _deltaTime)
    {
        currentTime += _deltaTime;
        return currentTime >= maxTime;
    }
}
