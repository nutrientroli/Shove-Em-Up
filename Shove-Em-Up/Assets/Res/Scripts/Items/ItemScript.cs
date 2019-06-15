using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private bool active = true;
    private float currentTime = 0;
    [SerializeField] private float maxTimeSpawn = 20;
    public GameObject item;
    public float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {
            active = false;
            SetActive();
            currentTime = 0;
            PlayerScript player = other.GetComponent<PlayerScript>();
            player.habilityScript.IncrementPerItem();
            player.StartItemParticles();
        }

    }

    // Start is called before the first frame update
    void Start() {
        SetActive();
        rotationSpeed = Random.Range(250, 600);
    }

    private void SetActive() {
        item.SetActive(active);
    }



    // Update is called once per frame
    void Update() {
        if (active) {
            //item.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
