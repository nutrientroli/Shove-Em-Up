using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityScript : MonoBehaviour
{
    //Energy System
    public  float maxEnergy = 100;
    private float currentEnergy = 0;
    private float incrementEnergyPerPush = 5;
    private float incrementEnergyPerItem = 20;

    //Time System
    private float currentTime = 0;
    private float coolDownIncrementImpact = 0;
    private float durationIncrementImpact = 0.5f;
    protected float duration = 5;

    //Utils
    protected bool active = false;

    //Modifiers
    public ModifierScript modToOthers;
    public ModifierScript modToMe;
    protected PlayerScript player;

    //Script provisional
    public CanvasPush canvasPush;

    virtual protected void Start()
    {
        canvasPush.StartBarHability(this);
        player = gameObject.GetComponent<PlayerScript>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!active) {
            if (currentEnergy < maxEnergy)
            {
                IncrementEnergy(Time.deltaTime);
            }
            else {
                //Debug.Log("Habilidad Posible");
            }
        } else {
            currentTime += Time.deltaTime;
            if (currentTime >= duration) DesactiveHability();
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

    public virtual void DesactiveHability()
    {
        currentTime = 0;
        active = false;
    }
    
    public bool CanUseHability()
    {
        return !active && currentEnergy == maxEnergy && player.GetIsMovible() && player.GetPushable();
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

    public void IncrementPerItem()
    {
        currentEnergy += incrementEnergyPerItem;
        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;
        
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public void RestartHability()
    {
        currentEnergy = 0;
    }

}
