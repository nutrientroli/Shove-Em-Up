using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPoints : MonoBehaviour
{
    public List<Image> imagenes;
    private Vector3 positionRelativePJ;
    private GameObject parent;

    private void Start()
    {

    }

    public void Init(int num, GameObject _parent)
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
        parent = _parent;
        positionRelativePJ = gameObject.transform.position - parent.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            gameObject.transform.position = parent.transform.position + positionRelativePJ;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.0001f, gameObject.transform.localScale.y + 0.0001f, gameObject.transform.localScale.z + 0.0001f);
            //positionRelativePJ += Vector3.forward * Time.deltaTime;
            if (gameObject.transform.localScale.x >= -0.002)
                Destroy(gameObject);
        }
    }
}
