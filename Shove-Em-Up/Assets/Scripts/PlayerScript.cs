using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Push
    private PushScript pushScript;
    private MoveScript moveScript;

    //Hability
    private float currentEnergy = 0;
    private float energyToHability = 1;

    private void Start()
    {
        pushScript = GetComponent<PushScript>();
        if (pushScript == null)
            pushScript = gameObject.AddComponent<PushScript>();

        moveScript = GetComponent<MoveScript>();
        if (moveScript == null)
            moveScript = gameObject.AddComponent<MoveScript>();
    }


    public void Movement(Vector3 VectorToMove)
    {
        //Debug.Log(VectorToMove);
        //Movimiento en base de los Axis del input.
        moveScript.AddVectorToMove(VectorToMove);
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
