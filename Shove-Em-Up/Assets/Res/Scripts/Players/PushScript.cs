using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    private PlayerScript player;
    private float timeChargePush = 0;
    private float maxTimeChargePush = 0.75f;
    private float timeCurrentCoolDownPush = 0;
    private float maxCoolDownPush = 0.5f;

    private float forceBase = 1.1f;
    private float exponentBase = 4f;
    private float dividentBase = 5f;
    private float currentForce = 0f;
    private float speedPush = 17;

    private CharacterController characterController;
    public CanvasPush canvasPush;
    public GameObject particlesChoque;


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

        canvasPush.StartBarForceCharge(this);
        if (timeChargePush < maxTimeChargePush)
        {
            timeChargePush += _time;
            if (timeChargePush > maxTimeChargePush)
                timeChargePush = maxTimeChargePush;
        }

    }

    public void Push()
    {
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.DASH);
        currentForce = (Mathf.Pow(timeChargePush / (maxTimeChargePush - maxTimeChargePush / dividentBase), exponentBase));
        if (currentForce < forceBase)
            currentForce = forceBase;
        if (player.GetRalenticed())
            currentForce *= 0.5f;
        canPush = false;
        RestartCharge();
        maxCoolDownPush = currentForce * 0.5f;
        if (maxCoolDownPush < 0.7f)
            maxCoolDownPush = 0.7f;
        else if (maxCoolDownPush > 1.75f)
            maxCoolDownPush = 1.75f;
        canvasPush.StartBarCoolDown(this);
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


    public void PushSomeone(GameObject _player, Vector3 _direction, float _force = 0)
    {
        if (_force == 0)
            _force = forceBase * 3.5f;
        float totalSpeedPush = speedPush * _force;
        Instantiate(particlesChoque, gameObject.transform.position + (_direction * (gameObject.transform.position - _player.transform.position).magnitude), particlesChoque.transform.rotation);
        _player.GetComponent<KnockbackScript>().StartKnockback(_force, forceBase, totalSpeedPush, _direction);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.KNOCKBACK);
        if (Random.Range(0, 10) > 8) SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_9);
        else if (Random.Range(0, 10) < 2) SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_8);
    }

    
    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if (player.currentState == PlayerScript.State.PUSHING)
            {

                //calcular el angulo con el que toca el player en un futuro
                Vector3 direction = (hit.gameObject.transform.position - gameObject.transform.position).normalized;
                PushSomeone(hit.gameObject, direction);
                player.PushSomeoneOther();
                player.ChangeState(PlayerScript.State.MOVING);
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject != gameObject)
        {
            if (player != null && player.currentState == PlayerScript.State.PUSHING)
            {
                Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
                float rotation = Quaternion.Angle(Quaternion.Euler(gameObject.transform.forward), Quaternion.Euler(direction));
                if (rotation < 1.3f)
                {
                    if (currentForce >= 2)
                        player.AddScore(1);
                    //calcular el angulo con el que toca el player en un futuro
                    player.SetPlayerPushed(other.gameObject);
                    PushSomeone(other.gameObject, direction, currentForce);
                    player.PushSomeoneOther();
                    player.ChangeState(PlayerScript.State.MOVING);
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player" && hit.gameObject != gameObject)
        {
            if (player != null && player.currentState == PlayerScript.State.PUSHING)
            {
                Vector3 direction = (hit.gameObject.transform.position - gameObject.transform.position).normalized;
                float rotation = Quaternion.Angle(Quaternion.Euler(gameObject.transform.forward), Quaternion.Euler(direction));
                if (rotation < 1.3f)
                {
                    if (currentForce >= 2)
                        player.AddScore(1);
                    //calcular el angulo con el que toca el player en un futuro
                    player.SetPlayerPushed(hit.gameObject);
                    PushSomeone(hit.gameObject, direction, currentForce);
                    player.PushSomeoneOther();
                    player.ChangeState(PlayerScript.State.MOVING);
                }
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

    public float GetMaxForce()
    {
        return maxTimeChargePush;
    }

    public float GetCurrentForce()
    {
        return timeChargePush;
    }


}
