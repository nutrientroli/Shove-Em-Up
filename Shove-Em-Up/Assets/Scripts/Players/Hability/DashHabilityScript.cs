﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHabilityScript : HabilityScript
{
    private Vector3 forward;
    private float speed = 1.5f;
    private float dashTime = 0;
    private bool usada = false;
    private CharacterController characterController;
    private PlayerScript player;
    private PushScript pushScript;
    private CapsuleCollider capsuleCollider;

    protected override void Start()
    {
        base.Start();
        capsuleCollider = GetComponent<CapsuleCollider>();
        characterController = GetComponent<CharacterController>();
        player = GetComponent<PlayerScript>();
        pushScript = GetComponent<PushScript>();
        canvasPush.SetDashHability();
    }

    public override void UseHability()
    {
        base.UseHability();
        capsuleCollider.radius = player.GetRealCapsuleRadius() * 2f;
        player.particlesDash.Play();
        forward = gameObject.transform.forward;
        modToMe = gameObject.AddComponent<DashModifierScript>();
        usada = true;
    }

    public override void DesactiveHability()
    {
        base.DesactiveHability();
        player.particlesDash.Stop();
        capsuleCollider.radius = player.GetRealCapsuleRadius();
    }

    protected override void Update()
    {
        base.Update();
        if (usada)
        {
            dashTime += Time.deltaTime;
            if (dashTime <= 0.2f)
            {
                CollisionFlags collisionFlags = characterController.Move(gameObject.transform.forward.normalized * speed);
            }
            else
            {
                dashTime = 0;
                DesactiveHability();
                usada = false;
            }
        }
    }

    /*void OnControllerColliderHit(ControllerColliderHit hit)
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
                dashTime = 0;
                DesactiveHability();
                usada = false;
            }
            
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject != gameObject)
        {
            if (usada)
            {
                //calcular el angulo con el que toca el player en un futuro
                Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
                pushScript.PushSomeone(other.gameObject, direction * 10f);
                player.PushSomeoneOther();
                player.ChangeState(PlayerScript.State.MOVING);
                dashTime = 0;
                DesactiveHability();
                usada = false;
            }

        }
    }


}
