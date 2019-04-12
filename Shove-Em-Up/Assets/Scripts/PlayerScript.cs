using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum State {MOVING, CHARGING, PUSHING, KNOCKBACK, HABILITY };
    public State currentState;
    private float currentTime = 0;
    private float timeToPush = 0.25f;
    private MoveScript moveScript;
    private PushScript pushScript;
    private KnockbackScript knockbackScript;

    private void Start()
    {
        moveScript = GetComponent<MoveScript>();
        if (moveScript == null)
            moveScript = gameObject.AddComponent<MoveScript>();
        pushScript = GetComponent<PushScript>();
        if (pushScript == null)
            pushScript = gameObject.AddComponent<PushScript>();
        knockbackScript = GetComponent<KnockbackScript>();
        if (knockbackScript == null)
            knockbackScript = gameObject.AddComponent<KnockbackScript>();
        currentState = State.MOVING;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.MOVING:
                break;
            case State.CHARGING:
                break;
            case State.PUSHING:
                pushScript.PushCharacter(Time.deltaTime);
                currentTime += Time.deltaTime;
                if (currentTime >= timeToPush)
                    ChangeState(State.MOVING);
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
        }

        ///*
        if (Input.GetKeyDown(KeyCode.P))
            Charge(Time.deltaTime);                 //PARA PROBAR, QUITAR EN UN FUTURO
        if (Input.GetKeyUp(KeyCode.P))
            Push();
        if (Input.GetKeyDown(KeyCode.K))
            Knockback();
        //*/
    }

    public void ChangeState(State _newState)
    {
        switch(currentState)
        {
            case State.MOVING:
                moveScript.CanMove(false);
                break;
            case State.CHARGING:
                moveScript.CanMove(false);
                moveScript.Charging(false);
                break;
            case State.PUSHING:
                currentTime = 0;
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
        }

        switch(_newState)
        {
            case State.MOVING:
                moveScript.CanMove(true);
                moveScript.Charging(false);
                break;
            case State.CHARGING:
                moveScript.CanMove(true);
                moveScript.Charging(true);
                break;
            case State.PUSHING:
                pushScript.Push();
                break;
            case State.KNOCKBACK:
                pushScript.PushSomeone(gameObject, -gameObject.transform.forward); //cambiar en un futuro
                break;
            case State.HABILITY:
                break;
        }
        currentState = _newState;
    }

    public void Push()
    {
        if (pushScript.CanPush() && currentState == State.CHARGING)
            ChangeState(State.PUSHING);
    }

    public void Charge(float _time)
    {
        if (pushScript.CanPush() && currentState != State.CHARGING)
        {
            pushScript.ChargePush(_time);
            ChangeState(State.CHARGING);
        }
        
    }

    public void Movement(Vector3 _vector)
    {
        moveScript.AddVectorToMove(_vector);
        if (currentState != State.MOVING)
            ChangeState(State.MOVING);
    }

    public void Knockback()
    {
        ChangeState(State.KNOCKBACK);
    }

    public void StopKnockback()
    {
        ChangeState(State.MOVING);
    }


}
