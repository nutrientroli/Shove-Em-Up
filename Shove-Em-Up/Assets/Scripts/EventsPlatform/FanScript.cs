using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    private List<PlayerScript> players = new List<PlayerScript>();
    private List<bool> enters = new List<bool>();
    private bool inAction = false;
    private bool air = false;
    private BoxCollider collider;
    private float rotationSpeed = 20;
    private float currentTime = 2.5f;
    private float totalTime = 0;
    private float maxTimeToOtherSide = 5;
    private float side = 1;
    private Quaternion startRotation;
    public float timeToDisapear = 12.5f;
    public ParticleSystem particles;
    public Transform toGo;
    public Transform goBack;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = gameObject.transform.rotation;
        collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = false;
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (inAction)
        {
            
            if (air)
            {
                currentTime += Time.deltaTime;
                totalTime += Time.deltaTime;
                gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * side);
                if (currentTime >= maxTimeToOtherSide)
                {
                    side *= -1;
                    currentTime = 0;
                }

                for (int i = 0; i < players.Count; i++)
                {
                    if (enters[i])
                    {
                        players[i].Movement(gameObject.transform.forward, true);
                    }
                }
                if (totalTime >= timeToDisapear - 1.5f)
                {
                    collider.enabled = false;
                }

                if (totalTime >= timeToDisapear)
                    Hide();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, toGo.position, Time.deltaTime * 2.5f);
                if ((transform.position - toGo.position).magnitude <= 0.01f)
                {
                    particles.Play();
                    air = true;
                }
            }
        }else if(air)
        {
            transform.position = Vector3.MoveTowards(transform.position, goBack.position, Time.deltaTime * 2);
            if ((transform.position - goBack.position).magnitude <= 0.01f)
            {
                air = false;
                gameObject.transform.rotation = startRotation;
            }
        }

    }

    public void Hide()
    {
        collider.enabled = false;
        inAction = false;
        particles.Stop();
        currentTime = 0;
        side = 1;
        totalTime = 0;
    }

    public void Active()
    {
        inAction = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerScript newPlayer = other.gameObject.GetComponent<PlayerScript>();
            bool find = false;
            int num = 0;
            int finalNum = 0;
            foreach(PlayerScript _player in players)
            {
                if (newPlayer == _player)
                {
                    find = true;
                    finalNum = num;
                }
                num++;
            }
            if(!find)
            {
                players.Add(newPlayer);
                enters.Add(true);
            }
            else
            {
                enters[finalNum] = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerScript newPlayer = other.gameObject.GetComponent<PlayerScript>();
            int num = 0;
            foreach (PlayerScript _player in players)
            {
                if (newPlayer == _player)
                {
                    enters[num] = false;
                }
                num++;
            }
        }
    }
}
