using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Push
    private PushScript pushScript;

    //Hability
    private float currentEnergy = 0;
    private float energyToHability = 1;

    private void Start()
    {
        pushScript = GetComponent<PushScript>();
        if (pushScript == null)
            pushScript = gameObject.AddComponent<PushScript>();
    }


    public void Movement(Vector3 VectorToMove)
    {
        //Movimiento en base de los Axis del input.
    }

    public void ChargePush(float _time)
    {
        pushScript.ChargePush(_time);
        //irá aumentando el valor del tiempo mientras siga llamando a la función
    }

    public void Push()
    {
        //la fuerza de empuje tendrá de referencia el timepo cargado en la función ChargePush
        pushScript.Push();
    }

    private bool CanPush()
    {
        bool report = pushScript.CanPush();
        //if(report && !)
        //Comprueba que esté en el suelo, que no este siendo empujado ni empujando...
        return report;
    }


        

}
