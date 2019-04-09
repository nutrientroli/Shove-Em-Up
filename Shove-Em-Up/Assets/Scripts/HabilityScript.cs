using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityScript : MonoBehaviour
{
    private float maxEnergy = 100;
    private float currentEnergy = 0;
    private float incrementEnergyPerSecond = 1;
    private float incrementEnergyPerPush = 20;
    private float incrementEnergyPerItem = 50;
    private float currentTime = 0;


    // Update is called once per frame
    void Update()
    {
        if(currentEnergy < maxEnergy)
            currentTime += Time.deltaTime;
        while(currentTime >= 1)
        {
            currentTime--;
            IncrementEnergy(incrementEnergyPerSecond);
        }
    }

    public void SetMaxEnergy(float _energy)
    {
        maxEnergy = _energy;
    }

    private void IncrementEnergy(float _energy)
    {
        currentEnergy += _energy;
        print(currentEnergy);
    }

    public void UseHability()
    {
        currentEnergy = 0;
        currentTime = 0;
    }

}
