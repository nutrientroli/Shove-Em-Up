using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour
{
    private Vector3 forward = Vector3.forward;
    private float rotationSpeed = 360;
    private float speed = 10;
    private float currentTime = 0;
    public float timeToDisapear = 1.5f;
    private GameObject myPlayer;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToDisapear)
            Destroy(gameObject);
        gameObject.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        gameObject.transform.position += forward * Time.deltaTime * speed;
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
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (player.GetKnockable())
            {
                if (player.currentState != PlayerScript.State.KNOCKBACK)
                    player.ChangeState(PlayerScript.State.MOVING);
                player.AddOtherMod(player.gameObject.AddComponent<StunBananaModifierScript>());
            }
            Destroy(gameObject);
        }
       /* if(other.gameObject.GetComponent<BananaScript>() != null)
        {
            if (other.gameObject.GetComponent<BananaScript>().myPlayer != myPlayer)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }*/
    }
}
