using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    private PlayerScript player;
    private float timeChargePush = 0;
    private float maxTimeChargePush = 1f;
    private float timeCurrentCoolDownPush = 0;
    private float maxCoolDownPush = 0.75f;

    private float forceBase = 2f;
    private float exponentBase = 4f;
    private float dividentBase = 7f;
    private float currentForce = 1.4f;
    private float speedPush = 17;

    private CharacterController characterController;
    public CanvasPush canvasPush;


    private bool canPush = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            characterController = gameObject.AddComponent<CharacterController>();
        player = GetComponent<PlayerScript>();
        if (player == null)
            Debug.Log("NO TIENE EL SCRIPT PLAYER EL PLAYER");
    }

    private void Update()
    {
        UpdateCoolDownPush(Time.deltaTime);
        if (timeChargePush > 0)
            ChargePush(Time.deltaTime);
    }

    public void ChargePush(float _time)
    {
        if (timeChargePush < maxTimeChargePush)
        {
            timeChargePush += _time;
            if (timeChargePush > maxTimeChargePush)
                timeChargePush = maxTimeChargePush;
        }

    }

    public void Push()
    {
        currentForce = (Mathf.Pow(timeChargePush / (maxTimeChargePush - maxTimeChargePush / dividentBase), exponentBase));
        if (currentForce < forceBase)
            currentForce = forceBase;
        canPush = false;
        RestartCharge();
        canvasPush.StartBar(this);
    }

    public void RestartCharge()
    {
        timeChargePush = 0;
    }

    private void UpdateCoolDownPush(float _time)
    {
        if (!canPush)
        {
            timeCurrentCoolDownPush += _time;
            if (timeCurrentCoolDownPush >= maxCoolDownPush)
            {
                canPush = true;
                timeCurrentCoolDownPush = 0;
                currentForce = 0;
            }
        }
    }

    public bool CanPush()
    {
        return canPush;
    }

    public void PushCharacter(float _time)
    {
        float totalSpeedPush = speedPush * currentForce;
        CollisionFlags collisionFlags = characterController.Move(gameObject.transform.forward * _time * totalSpeedPush);
    }


    public void PushSomeone(GameObject _player, Vector3 _direction)
    {
        float totalSpeedPush = speedPush * currentForce;
        _player.GetComponent<KnockbackScript>().StartKnockback(currentForce, forceBase, totalSpeedPush, _direction);
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if (player.currentState == PlayerScript.State.PUSHING)
            {
                //calcular el angulo con el que toca el player en un futuro
                Debug.Log("Collision");
                Vector3 direction = (hit.gameObject.transform.position - gameObject.transform.position).normalized;
                PushSomeone(hit.gameObject, direction);
            }
        }
    }

    public float GetMaxCoolDownPush()
    {
        return maxCoolDownPush;
    }

    public float GetCurrentCoolDownPush()
    {
        return timeCurrentCoolDownPush;
    }


}
