using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPointEvent : MonoBehaviour
{
    private float currentTime = 0;
    private float timeToPoint = 1;
    private List<PlayerData> playersData = new List<PlayerData>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playersData.Count == 1)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeToPoint)
            {
                currentTime -= timeToPoint;
                playersData[0].GetComponent<PlayerScript>().AddScore(1);
            }
        }
        else
            currentTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bool existe = false;
            for(int i = 0; i < playersData.Count; i++)
            {
                if (playersData[i] == other.GetComponent<PlayerData>())
                    existe = true;
            }
            if(!existe)
                playersData.Add(other.GetComponent<PlayerData>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool existe = false;
            for (int i = 0; i < playersData.Count; i++)
            {
                if (playersData[i] == other.GetComponent<PlayerData>())
                    existe = true;
            }

            if (existe)
                playersData.Remove(other.GetComponent<PlayerData>());
        }
    }


}
