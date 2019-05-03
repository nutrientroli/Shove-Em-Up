using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private PlayerScript player;
    private CharacterController characterController;
    private Vector3 toMove = Vector3.zero;
    private Vector3 forward = Vector3.forward;
    private bool onGround = true;
    private float gravity = 2;
    private float speed = 7;
    private float verticalSpeed = 0;
    private float multiplyCharge = 1;
    private float maxMultiplayCharge = 0.5f;
    public bool isMovible = true;

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
 
        CheckGravity();
        //Para testear, quitar en un futuro
        ///*
        if (Input.GetKey(GetComponent<PlayerScript>().Up))
            AddVectorToMove(Vector3.forward);
        if (Input.GetKey(GetComponent<PlayerScript>().Down))
            AddVectorToMove(Vector3.back);
        if (Input.GetKey(GetComponent<PlayerScript>().Right))
            AddVectorToMove(Vector3.right);
        if (Input.GetKey(GetComponent<PlayerScript>().Left))
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

    public void Charging(bool _charging)
    {
        if (_charging)
            multiplyCharge = maxMultiplayCharge;
        else
            multiplyCharge = 1;
    }

    private void ResetVectorToMove()
    {
        toMove = new Vector3(0, toMove.y, 0);
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
        if (isMovible)
        {
            toMove += _toMove * speed * multiplyCharge;
          
            if (new Vector3(toMove.x, 0, toMove.z) != Vector3.zero)
            {
               
                forward = new Vector3(toMove.x, 0, toMove.z).normalized;
               
            }
        }

    }

    private void MoveCharacter(float _time)
    {
        toMove.y += verticalSpeed;
        toMove *= _time;
        gameObject.transform.forward = forward;
        if (player.GetRalenticed() && player.GetKnockable() && player.currentState != PlayerScript.State.KNOCKBACK)
            toMove *= 0.2f;
        CollisionFlags collisionFlags = characterController.Move(toMove);
        ResetVectorToMove();
        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            onGround = true;
        }
        else
            onGround = false;
    }

    public Vector3 GetMoveVector()
    {
        return toMove;
    }

    public void InvertMovement(float _time)
    {
        toMove.y += verticalSpeed;
        toMove *= _time;
        gameObject.transform.forward = forward;
        CollisionFlags collisionFlags = characterController.Move(toMove * -1);
        ResetVectorToMove();
        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            onGround = true;
        }
        else
            onGround = false;
    }

}
