using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuercoSpinHabilityScript : HabilityScript
{
    private float habilityTime = 0;
    public bool usada = false;
    private bool firstTime = true;
    private CharacterController characterController;
    private PushScript pushScript;
    private SphereCollider sphereCollider;
    public ParticleSystem particlesSpins;
    private ParticleSystem ourParticles;

    protected override void Start()
    {
        base.Start();
        duration = 0.8f;
        characterController = GetComponent<CharacterController>();
        pushScript = GetComponent<PushScript>();
        canvasPush.SetDashHability();
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.transform.position = gameObject.transform.position;
        sphereCollider.radius = 4;
        sphereCollider.isTrigger = true;
        sphereCollider.enabled = false;
        if (particlesSpins != null)
        {
            ourParticles = Instantiate(particlesSpins, gameObject.transform.position, particlesSpins.transform.rotation);
            ourParticles.transform.parent = gameObject.transform;
        }
    }

    public override void UseHability()
    {
        base.UseHability();
        modToMe = gameObject.AddComponent<PuercoSpinModifierScript>();
        usada = true;
        firstTime = true;
        habilityTime = 0;
    }

    public override void DesactiveHability()
    {
        base.DesactiveHability();
        sphereCollider.enabled = false;
        usada = false;
        firstTime = true;
        habilityTime = 0;
        if (ourParticles != null)
            ourParticles.Stop();
        


    }

    protected override void Update()
    {
        base.Update();
        if (usada)
        {
            if (player.currentState == PlayerScript.State.KNOCKBACK)
            {
                DesactiveHability();
            }

            habilityTime += Time.deltaTime;
            if(habilityTime <= 0.2f)
            {
                CollisionFlags collisionFlags = characterController.Move(Vector3.up * 20 * Time.deltaTime);
            }
            if (habilityTime >= 0.3f && firstTime)
            {
                firstTime = false;
                if (ourParticles != null)
                    ourParticles.Play();
                sphereCollider.enabled = true;
            }
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
            if (usada && !firstTime)
            {
                float distance = (other.gameObject.transform.position - gameObject.transform.position).magnitude;
                Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
                float rotation = Quaternion.Angle(Quaternion.Euler(gameObject.transform.forward), Quaternion.Euler(direction));

                //calcular el angulo con el que toca el player en un futuro
                pushScript.PushSomeone(other.gameObject, direction, sphereCollider.radius / distance);
            }

        }
    }
}
