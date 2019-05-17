using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPoints : MonoBehaviour
{
    public List<Image> imagenes;


    public void Init(int num)
    {
        switch (num)
        {
            case 1:
                imagenes[0].enabled = true;
                break;
            case 3:
                imagenes[1].enabled = true;
                break;
            case 5:
                imagenes[2].enabled = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.0001f, gameObject.transform.localScale.y + 0.0001f, gameObject.transform.localScale.z + 0.0001f);
        gameObject.transform.position += Vector3.forward * Time.deltaTime;
        if (gameObject.transform.localScale.x >= -0.002)
            Destroy(gameObject);
    }
}
