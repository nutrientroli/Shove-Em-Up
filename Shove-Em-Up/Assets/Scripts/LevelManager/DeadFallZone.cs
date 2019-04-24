using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFallZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) LevelManagerScript.players--;
        //Debug.Log(LevelManagerScript.players);
    }
}
