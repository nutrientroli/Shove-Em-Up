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
            otherPush.PushSomeone(other.gameObject, direction * 1.2f);
        }
    }
}
