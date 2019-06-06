using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject posImpact;
    public float speed = 30;
    public LayerMask layerMask;
    public ParticleSystem[] particles;
    private bool activate = false;
    private bool explosion = false;
    private float delay = 0;
    private float currentTime = 0;
    private SphereCollider collider;
    private Vector3 posInitial;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
        collider.radius = 2;
        posInitial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(activate)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= delay && !explosion)
            {
                gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
                if(gameObject.transform.position.y <= posImpact.transform.position.y + 1)
                {
                    explosion = true;
                    collider.enabled = true;
                    currentTime = 0;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    posImpact.GetComponent<MeshRenderer>().enabled = false;
                    foreach (ParticleSystem p in particles)
                        p.Play();
                }

            }
            if(explosion && currentTime >= 0.3f)
            {
                Restart();
            }

        }
    }

    public void Restart()
    {
        explosion = false;
        activate = false;
        collider.enabled = false;
        posImpact.transform.parent = gameObject.transform;
        transform.position = posInitial;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        posImpact.GetComponent<MeshRenderer>().enabled = true;
        currentTime = 0;
    }

    public void Active(float _delay = 2)
    {
        delay = _delay;
        RaycastHit rayHit;
        Ray ray = new Ray();
        ray.direction = Vector3.down;
        ray.origin = transform.position;

        if (Physics.Raycast(ray, out rayHit, 50, layerMask.value))
        {
            posImpact.transform.position = rayHit.point + new Vector3(0, 0.35f, 0);
            posImpact.transform.parent = null;
        }
        activate = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(explosion)
        {
            if(other.gameObject.tag == "Player")
            {
                PushScript pushScript = other.gameObject.GetComponent<PushScript>();
                Vector3 direction = new Vector3(other.gameObject.transform.position.x - gameObject.transform.position.x, 0, other.gameObject.transform.position.z - gameObject.transform.position.z).normalized;
                if (direction.x == 0 && direction.z == 0)
                    direction = other.gameObject.transform.forward;

                pushScript.PushSomeone(other.gameObject, direction, 2.5f);
            }

        }
    }
}
