using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityScript : MonoBehaviour
{
    private float currentTime = 0;
    private float maxTime = 10;
    private float timeToStart = 1;
    private bool exit = false;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > timeToStart && !exit)
        {
            gameObject.transform.localScale += new Vector3(0.2f,0.1f,0.2f);
            if (gameObject.transform.localScale.x > 7)
                gameObject.transform.localScale = new Vector3(7, gameObject.transform.localScale.y, 7);
            if (gameObject.transform.localScale.y > 3)
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 3, gameObject.transform.localScale.z);

            if (gameObject.transform.localScale == new Vector3(7, 3, 7))
            {
                exit = true;
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }

        if(currentTime >= maxTime)
        {
            gameObject.transform.localScale -= new Vector3(0.2f, 0.1f, 0.2f);
            if (gameObject.transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if(player != null && other.gameObject.GetComponent<ToxicityModifierScript>() == null)
                player.AddOtherMod(player.gameObject.AddComponent<ToxicityModifierScript>());
        }
    }
}
