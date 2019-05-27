using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPoints : MonoBehaviour
{
    public List<Image> imagenes;
    private Image imagen;
    private Vector3 positionRelativePJ;
    private GameObject parent;
    private float currentTime = 0;
    private float alpha = 1;
    private void Start()
    {
        gameObject.transform.forward = Camera.main.transform.position - gameObject.transform.position;
    }

    public void Init(int num, GameObject _parent)
    {
        switch (num)
        {
            case 1:
                imagenes[0].enabled = true;
                imagen = imagenes[0];
                break;
            case 3:
                imagenes[1].enabled = true;
                imagen = imagenes[1];

                break;
            case 5:
                imagenes[2].enabled = true;
                imagen = imagenes[2];

                break;
        }
        parent = _parent;
        positionRelativePJ = gameObject.transform.position - parent.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            positionRelativePJ += new Vector3(0, 2 * Time.deltaTime, 0);
            currentTime += Time.deltaTime;
            if(currentTime >= 0.5f)
            {
                alpha -= 1.5f * Time.deltaTime;
                if (imagen != null)
                {
                    imagen.color = new Color(255, 255, 255, alpha);
                    if (currentTime >= 1f || imagen.color.a <= 0)
                        Destroy(gameObject);
                }
            }
            gameObject.transform.position = parent.transform.position + positionRelativePJ;

        }
    }
}
