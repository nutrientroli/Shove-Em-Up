using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{

    private PlayerScript player;

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerScript>();
        if (!player) Debug.LogError("Error. CharacterController without Player");
    }
    
    public void ChargePush()
    {
        player.ChargePush(Time.deltaTime);
        Debug.Log("Charge Input");
    }

    public void Push()
    {
        player.Push();
        Debug.Log("Push Input");
    }

    public void Hability()
    {
        //player.
    }

    public void Move(float _h, float _v)
    {
        Debug.Log("Move Input");
        player.Movement(new Vector3(_h, 0, _v));
    }
}
