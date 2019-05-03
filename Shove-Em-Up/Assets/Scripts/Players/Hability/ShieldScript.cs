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
            Vector3 direction = (player.gameObject.transform.position - me.gameObject.transform.position).normalized;
            PushScript otherPush = other.gameObject.GetComponent<PushScript>();

            if (player.currentState == PlayerScript.State.PUSHING)
            {
                otherPush.PushSomeone(other.gameObject, direction, 2.5f);

            }
            else if(player.currentState != PlayerScript.State.KNOCKBACK)
            {
                if(other.gameObject.GetComponent<PuercoSpinHabilityScript>() != null)
                {
                    if(!other.gameObject.GetComponent<PuercoSpinHabilityScript>().usada)
                    {
                        otherPush.PushSomeone(other.gameObject, direction, 1.1f);
                    }
                }
                else otherPush.PushSomeone(other.gameObject, direction, 1.1f);

            }
            me.gameObject.GetComponent<ShieldHabilityScript>().DecreseLifeShield();
        }
    }
}
