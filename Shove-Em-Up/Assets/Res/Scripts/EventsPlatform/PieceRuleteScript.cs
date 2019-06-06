using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRuleteScript : MonoBehaviour
{
    private bool active = false;
    private bool revert = false;
    private float speed = 60;
    private Vector3 startPos;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (gameObject.transform.position.y >= -75)
                gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if(revert)
        {
            if (startPos.y - gameObject.transform.position.y <= 0.005f && startPos.y - gameObject.transform.position.y >= -0.005f)
            {
                revert = false;
            }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPos, speed * Time.deltaTime);

        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Reverted()
    {
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, 50, gameObject.transform.position.z);
        active = false;
        revert = true;
    }
}
