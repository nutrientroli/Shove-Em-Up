using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityScript : MonoBehaviour
{
    //Energy System
    [SerializeField] private float maxEnergy = 100;
    private float currentEnergy = 0;
    private float incrementEnergyPerPush = 5;
    private float incrementEnergyPerItem = 20;

    //Time System
    private float currentTime = 0;
    private float coolDownIncrementImpact = 0;
    private float durationIncrementImpact = 0.5f;
    private float duration = 5;

    //Utils
    protected bool active = false;

    //Modifiers
    public ModifierScript modToOthers;
    public ModifierScript modToMe;

    //Script provisional
    public CanvasPush canvasPush;

    private void Start()
    {
        canvasPush.StartBarHability(this);
    }
    // Update is called once per frame
    protected void Update()
    {
        if (!active) {
            if (currentEnergy < maxEnergy)
            {
                IncrementEnergy(Time.deltaTime);
            }
            else {
                Debug.Log("Habilidad Posible");
            }
        } else {
            currentTime += Time.deltaTime;
            if (currentTime >= duration) DeactiveHability();
        }

        if (coolDownIncrementImpact > 0)
            coolDownIncrementImpact -= Time.deltaTime;
    }

    protected void SetMaxEnergy(float _energy)
    {
        maxEnergy = _energy;
    }

    private void IncrementEnergy(float _energy)
    {
        if(currentEnergy < maxEnergy)
            currentEnergy += _energy;

        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;
    }

    public virtual void UseHability()
    {
        currentEnergy = 0;
        currentTime = 0;
        active = true;
    }

    public virtual void DeactiveHability()
    {
        currentTime = 0;
        active = false;
    }

    public bool CanUseHability()
    {
        return !active && currentEnergy == maxEnergy;
    }

    public void IncrementPerImpact()
    {
        if (coolDownIncrementImpact <= 0)
        {
            coolDownIncrementImpact = durationIncrementImpact;
            currentEnergy += incrementEnergyPerPush;
            if (currentEnergy > maxEnergy)
                currentEnergy = maxEnergy;
        }
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

}
