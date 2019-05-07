using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityScript : MonoBehaviour
{
    public bool exit = false;
    private Vector3 scaleVector = new Vector3(0.2f, 0, 0.2f);
    private float maxY = 3;
    private float maxX = 30;

    void Update() {
        if(!exit) {
            if (gameObject.transform.localScale.x < maxX || gameObject.transform.localScale.y < maxY) gameObject.transform.localScale += scaleVector;
            if (gameObject.transform.localScale.x > maxX) gameObject.transform.localScale = new Vector3(maxX, gameObject.transform.localScale.y, maxX);
            if (gameObject.transform.localScale.y > maxY) gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, maxY, gameObject.transform.localScale.z);
        }

        if(exit) {
            gameObject.transform.localScale -= scaleVector;
            if (gameObject.transform.localScale.x <= 5) Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if(player != null && other.gameObject.GetComponent<ToxicityModifierScript>() == null)
                player.AddOtherMod(player.gameObject.AddComponent<ToxicityModifierScript>());
        }
    }
}
