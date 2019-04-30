using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuercoSpinHabilityScript : HabilityScript
{
    private float habilityTime = 0;
    private bool usada = false;
    private CharacterController characterController;
    private PlayerScript player;
    private PushScript pushScript;
    private SphereCollider sphereCollider;

    protected override void Start()
    {
        base.Start();
        characterController = GetComponent<CharacterController>();
        player = GetComponent<PlayerScript>();
        pushScript = GetComponent<PushScript>();
        canvasPush.SetDashHability();
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.transform.position = gameObject.transform.position;
        sphereCollider.radius = 3;
        sphereCollider.isTrigger = true;
        sphereCollider.enabled = false;
    }

    public override void UseHability()
    {
        base.UseHability();
        modToMe = gameObject.AddComponent<PuercoSpinModifierScript>();
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
            habilityTime += Time.deltaTime;
            if (habilityTime >= 0.5f)
            {
                habilityTime = 0;
                DesactiveHability();
                usada = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject != gameObject)
        {
            if (usada)
            {
                float distance = (other.gameObject.transform.position - gameObject.transform.position).magnitude;
                Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
                float rotation = Quaternion.Angle(Quaternion.Euler(gameObject.transform.forward), Quaternion.Euler(direction));

                //calcular el angulo con el que toca el player en un futuro
                pushScript.PushSomeone(other.gameObject, direction * (distance / sphereCollider.radius));
                player.PushSomeoneOther();
                usada = false;
                
            }

        }
    }
}
