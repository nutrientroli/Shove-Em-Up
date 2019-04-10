using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private PlayerScript player;
    private CharacterController characterController;
    private Vector3 toMove = Vector3.zero;
    private Vector3 rotation = Vector3.forward;
    private bool onGround = true;
    private float gravity = 2;
    private float speed = 10;
    private float verticalSpeed = 0;
    private bool isMovible = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            characterController = gameObject.AddComponent<CharacterController>();
        toMove = Vector3.zero;

        player = GetComponent<PlayerScript>();
        if (player == null)
            Debug.Log("NO TIENE EL SCRIPT PLAYER EL PLAYER");
        
    }

    private void Update()
    {
        ResetVectorToMove();
        CheckGravity();
        //Para testear, quitar en un futuro
        ///*
        if (Input.GetKey(KeyCode.W))
            AddVectorToMove(Vector3.forward);
        if (Input.GetKey(KeyCode.S))
            AddVectorToMove(Vector3.back);
        if (Input.GetKey(KeyCode.D))
            AddVectorToMove(Vector3.right);
        if (Input.GetKey(KeyCode.A))
            AddVectorToMove(Vector3.left);

        //*/
    }

    private void LateUpdate()
    {
        MoveCharacter(Time.deltaTime);
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetGravity(float _gravity)
    {
        gravity = _gravity;
    }

    public void CanMove(bool _canMove)
    {
        isMovible = _canMove;   
    }

    private void ResetVectorToMove()
    {
        toMove = Vector3.zero;
    }

    private void CheckGravity()
    {
        if (!onGround)
            verticalSpeed -= gravity;
        else
            verticalSpeed = 0;
    }

    public void AddVectorToMove(Vector3 _toMove)
    {
        toMove += _toMove * speed;
        if(new Vector3( toMove.x, 0, toMove.z) != Vector3.zero)
            rotation = new Vector3(toMove.x, 0, toMove.z).normalized;

    }

    private void MoveCharacter(float _time)
    {
        if (isMovible)
        {
            toMove.y += verticalSpeed;
            toMove *= _time;
            gameObject.transform.forward = rotation;
            CollisionFlags collisionFlags = characterController.Move(toMove);

            if ((collisionFlags & CollisionFlags.Below) != 0)
            {
                onGround = true;
            }
            else
                onGround = false;
        }
    }
}
