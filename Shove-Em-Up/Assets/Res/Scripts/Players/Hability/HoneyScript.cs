using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyScript : MonoBehaviour
{

    private Vector3 forward = Vector3.forward;
    private float rotationSpeed = 360;
    private float speed = 5;
    private float currentTime = 0;
    private Vector3 firstScale;
    private GameObject myPlayer;
    private bool gravity = true;
    public LayerMask layerMask;
    private SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += forward * Time.deltaTime * speed;
        currentTime += Time.deltaTime;
        gameObject.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        gameObject.transform.localScale -= Time.deltaTime * Vector3.one * 1.2f;

        RaycastHit rayHit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out rayHit, collider.radius * 1.5f, layerMask))
        {
            gravity = false;
        }
        else
        {
            if (Physics.Raycast(gameObject.transform.position + Vector3.right, Vector3.down, out rayHit, collider.radius * 1.5f, layerMask))
            {
                gravity = false;
            }
            else if (Physics.Raycast(gameObject.transform.position + Vector3.left, Vector3.down, out rayHit, collider.radius * 1.5f, layerMask))
            {
                gravity = false;
            }
            else gravity = true;
        }

        if (gravity)
            gameObject.transform.position += forward * Time.deltaTime * speed + Vector3.down * speed * Time.deltaTime * 3;
        else
            gameObject.transform.position += forward * Time.deltaTime * speed;

        if (gameObject.transform.localScale.x <= 0.5f)
            gameObject.SetActive(false);
    }

    public void SetForward(Vector3 _forward)
    {
        forward = _forward;
        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.forward, _forward);
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetRotationSpeed(float _rotatioSpeed)
    {
        rotationSpeed = _rotatioSpeed;
    }

    public void SetMyPlayer(GameObject _player)
    {
        myPlayer = _player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject != myPlayer)
        {

            if (other.gameObject.GetComponent<HoneyModifierScript>() == null)
            {
                PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
                if(player.GetKnockable())
                    player.AddOtherMod(player.gameObject.AddComponent<HoneyModifierScript>());
            }

        }
    }

    public void Restart()
    {
        gameObject.transform.localScale = firstScale;
        currentTime = 0;
    }

    public void SetScale(Vector3 _scale)
    {
        firstScale = _scale;
    }
}
