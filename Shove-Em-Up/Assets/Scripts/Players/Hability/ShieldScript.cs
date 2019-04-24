using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public PlayerScript me;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null && player != me) {
            //Invertir Movimiento
            //Debug.Log("Hola  :: " + direction);
            Vector3 direction = (player.gameObject.transform.position - me.gameObject.transform.position).normalized;
            player.ChangeState(PlayerScript.State.MOVING);
            player.GetComponent<KnockbackScript>().StartKnockback(20.0f, 20.0f, 20.0f, direction);
        }
    }
}
