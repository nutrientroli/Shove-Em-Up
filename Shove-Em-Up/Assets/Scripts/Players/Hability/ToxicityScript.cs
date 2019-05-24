using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityScript : MonoBehaviour
{
    public bool exit = false;
    private Vector3 scaleVector = new Vector3(0.2f, 0, 0.2f);
    private float maxY = 3;
    private float maxX = 30;
    private List<PlayerScript> players = new List<PlayerScript>();


    void Update() {
        if(!exit) {
            if (gameObject.transform.localScale.x < maxX || gameObject.transform.localScale.y < maxY) gameObject.transform.localScale += scaleVector;
            if (gameObject.transform.localScale.x > maxX) gameObject.transform.localScale = new Vector3(maxX, gameObject.transform.localScale.y, maxX);
            if (gameObject.transform.localScale.y > maxY) gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, maxY, gameObject.transform.localScale.z);
        }

        if(exit) {
            gameObject.transform.localScale -= scaleVector;
            if (gameObject.transform.localScale.x <= 5)
                Desactive();
        }
    }

    public void Desactive()
    {
        foreach(PlayerScript p in players)
        {
            if (p.gameObject.GetComponent<ToxicityModifierScript>() != null)
                p.RemoveMod(p.gameObject.GetComponent<ToxicityModifierScript>());
        }
        players.Clear();
        gameObject.SetActive(false);
    }

    public void Active()
    {
        exit = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            bool inside = false;
            PlayerScript _player =  other.gameObject.GetComponent<PlayerScript>();
            foreach(PlayerScript p in players)
            {
                if (p == _player)
                    inside = true;
            }
            if (!inside)
            {
                players.Add(_player);
                if (other.gameObject.GetComponent<ToxicityModifierScript>() == null)
                    _player.AddOtherMod(_player.gameObject.AddComponent<ToxicityModifierScript>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool inside = false;
            PlayerScript _player = other.gameObject.GetComponent<PlayerScript>();
            foreach (PlayerScript p in players)
            {
                if (p == _player)
                    inside = true;
            }
            if (inside)
            {
                players.Remove(_player);
                if (other.gameObject.GetComponent<ToxicityModifierScript>() != null)
                {
                    _player.RemoveMod(other.gameObject.GetComponent<ToxicityModifierScript>());
                }
            }
        }
    }
}
