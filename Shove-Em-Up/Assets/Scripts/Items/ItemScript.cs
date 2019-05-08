using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private bool active = true;
    private float currentTime = 0;
    [SerializeField] private float maxTimeSpawn = 20;
    public GameObject item;
    public float rotationSpeed = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {
            active = false;
            SetActive();
            currentTime = 0;
            other.GetComponent<PlayerScript>().habilityScript.IncrementPerItem();
        }

    }

    // Start is called before the first frame update
    void Start() {
        SetActive();
    }

    private void SetActive() {
        item.SetActive(active);
    }



    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            item.transform.Rotate(Vector3.up, rotationSpeed);
        }
        //else
        //{
        //    currentTime += Time.deltaTime;
        //    if (currentTime >= maxTimeSpawn)
        //    {
        //        currentTime = 0;
        //        active = true;
        //        SetActive();
        //    }
        //}

        item.transform.Rotate(Vector3.up, rotationSpeed);
    }
}
