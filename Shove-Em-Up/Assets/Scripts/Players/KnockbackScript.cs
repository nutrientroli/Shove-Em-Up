using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{ 
    private PlayerScript player;
    private CharacterController characterController;

    private float timeStopKnockback = 0;
    private float timeRelativeWithForce = 0.3f;
    private float force = 0;
    private float maxForce = 12f;
    private float hight = 4f;
    private Vector3 direction = Vector3.zero;
    private MoveScript moveScript;

    private bool canStop = true;

    private void Awake()
    {
        moveScript = GetComponent<MoveScript>();
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            characterController = gameObject.AddComponent<CharacterController>();

        player = GetComponent<PlayerScript>();
        if (player == null)
            Debug.Log("NO TIENE EL SCRIPT PLAYER EL PLAYER");
    }

    private void Update()
    {
        UpdateTimeKnockback(Time.deltaTime);
    }

    public void StartKnockback(float _currentForce, float _forceBase, float _speed, Vector3 _direction)
    {
        if (player.Knockback())
        {
            timeStopKnockback = (_currentForce / _forceBase) * timeRelativeWithForce;
            force = (_currentForce / _forceBase) * _speed / 2;

            if (force > maxForce)
                force = maxForce;

            direction = _direction.normalized;
            direction.y += timeStopKnockback * hight / 2;
            gameObject.transform.forward = new Vector3(-direction.x, 0, -direction.z);
            moveScript.SetForward(gameObject.transform.forward);
            canStop = false;
        }
    }

    private void UpdateTimeKnockback(float _time)
    {
        if (!canStop)
        {
            CollisionFlags collisionFlags = characterController.Move(direction * _time * force);
            if (direction.y <= 0)
                direction.y = 0;
            else
                direction.y -= hight * _time / 2;


            timeStopKnockback -= _time; 
            if (timeStopKnockback <= 0)
            {
                timeStopKnockback = 0;
                canStop = true;
                player.StopKnockback();
            }
        }
    }
}
