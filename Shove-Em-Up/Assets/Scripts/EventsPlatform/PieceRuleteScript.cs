using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRuleteScript : MonoBehaviour
{
    private bool active = false;
    private bool revert = false;
    private float speed = 70;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (gameObject.transform.position.y >= -100)
                gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if(revert)
        {
            if (gameObject.transform.position.y == startPos.y)
                revert = false;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPos, speed * Time.deltaTime);

        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Reverted()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 50, gameObject.transform.position.z);
        active = false;
        revert = true;
    }
}
