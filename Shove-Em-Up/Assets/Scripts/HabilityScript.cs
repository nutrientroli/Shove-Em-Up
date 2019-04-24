using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityScript : MonoBehaviour
{
    //Energy System
    [SerializeField] private float maxEnergy = 100;
    private float currentEnergy = 0;
    private float incrementEnergyPerSecond = 1;
    private float incrementEnergyPerPush = 20;
    private float incrementEnergyPerItem = 50;

    //Time System
    private float currentTime = 0;
    private float duration = 5;

    //Utils
    protected bool active = false;

    //Modifiers
    public ModifierScript modToOthers;
    public ModifierScript modToMe;

    // Update is called once per frame
    protected void Update()
    {
        if (!active) {
            if (currentEnergy < maxEnergy)
            {
                currentTime += Time.deltaTime;
            }else {
                Debug.Log("Habilidad Posible");
            }
            while (currentTime >= 1) {
                //No es lo mismo que multiplicar por deltaTime el incremento? Asi no hacemos el while.
                currentTime--;
                IncrementEnergy(incrementEnergyPerSecond);
            }
        } else {
            currentTime += Time.deltaTime;
            if (currentTime >= duration) DeactiveHability();
        }
    }

    protected void SetMaxEnergy(float _energy)
    {
        maxEnergy = _energy;
    }

    private void IncrementEnergy(float _energy)
    {
        currentEnergy += _energy;
       // print(currentEnergy);
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

}
