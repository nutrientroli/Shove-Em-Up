using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTwoSec : MonoBehaviour
{
    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 2)
            Destroy(gameObject);
    }
}
