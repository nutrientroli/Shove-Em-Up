using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    private List<PlayerScript> players = new List<PlayerScript>();
    private List<bool> enters = new List<bool>();
    private float rotationSpeed = 20;
    private float currentTime = 2.5f;
    private float totalTime = 0;
    private float maxTimeToOtherSide = 5;
    private float side = 1;
    public float timeToDisapear = 12.5f;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        totalTime += Time.deltaTime;
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * side);
        if (currentTime >= maxTimeToOtherSide)
        {
            side *= -1;
            currentTime = 0;
        }

        for(int i = 0; i < players.Count; i++)
        {
            if(enters[i])
            {
                players[i].Movement(gameObject.transform.forward, true);
            }
        }
        if(totalTime >= timeToDisapear -1.5f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            particles.Stop();
        }

        if (totalTime >= timeToDisapear)
            Destroy(gameObject);

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
