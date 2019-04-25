using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHabilityScript : HabilityScript
{
    private Vector3 forward;
    private float speed = 1.2f;
    private float currentTime = 0;
    private bool usada = false;
    private CharacterController characterController;
    private PlayerScript player;
    private PushScript pushScript;

    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        player = GetComponent<PlayerScript>();
        pushScript = GetComponent<PushScript>();
    }

    public override void UseHability()
    {
        base.UseHability();
        forward = gameObject.transform.forward;
        modToMe = gameObject.AddComponent<DashModifierScript>();
        usada = true;
    }

    public override void DesactiveHability()
    {
        base.DesactiveHability();
    }

    protected override void Update()
    {
        base.Update();
        if (usada)
        {
            currentTime += Time.deltaTime;
            if (currentTime <= 0.2f)
            {
                CollisionFlags collisionFlags = characterController.Move(gameObject.transform.forward.normalized * speed);
            }
            else
            {
                currentTime = 0;
                DesactiveHability();
                usada = false;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if (usada)
            {
                //calcular el angulo con el que toca el player en un futuro
                Vector3 direction = (hit.gameObject.transform.position - gameObject.transform.position).normalized;
                pushScript.PushSomeone(hit.gameObject, direction);
                player.PushSomeoneOther();
                player.ChangeState(PlayerScript.State.MOVING);
                currentTime = 0;
                DesactiveHability();
                usada = false;
            }
            
        }
    }


}
