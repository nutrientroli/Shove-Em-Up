using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum State {MOVING, CHARGING, PUSHING, KNOCKBACK, HABILITY };
    public State currentState;
    private float forcePush = 0;
    private bool pushing = false;
    private MoveScript moveScript;

    private void Start()
    {
        moveScript = GetComponent<MoveScript>();
        if (moveScript == null)
            moveScript = gameObject.AddComponent<MoveScript>();
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
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
        }
    }

    public void ChangeState(State _newState)
    {
        switch(currentState)
        {
            case State.MOVING:
                break;
            case State.CHARGING:
                break;
            case State.PUSHING:
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
                break;
            case State.CHARGING:
                break;
            case State.PUSHING:
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
        }

        currentState = _newState;
    }

    public void Push(float _force)
    {
        forcePush = _force;
        pushing = true;
        ChangeState(State.PUSHING); //provisional
    }

    public void Charge()
    {
        ChangeState(State.CHARGING);
    }



}
