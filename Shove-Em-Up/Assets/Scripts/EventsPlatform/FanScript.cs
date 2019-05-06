using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    private List<PlayerScript> players = new List<PlayerScript>();
    private List<bool> enters = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < players.Count; i++)
        {
            if(enters[i])
            {
                players[i].Movement(gameObject.transform.forward, true);
            }
        }
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
