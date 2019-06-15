using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorScript : MonoBehaviour
{
    private float type = 0;
    private float otherType = 0;
    
    public Animator anim;

    public List<Color> colors = new List<Color>();
    private int index = 0;
    private bool ir = true;

    // Start is called before the first frame update
    void Start() {
        PublicoManager.GetInstance().AddToList(this);
        type = Random.Range(0.0f, 1.0f);
        anim.SetFloat("Type", type);
        anim.speed = Random.Range(0.7f, 1.0f);
        index = Random.Range(0, 5);
        GetComponentInChildren<Renderer>().material.color = colors[index];
    }

    public void ChangeAnimation()
    {
        anim.SetTrigger("Go");
        if (ir)
        {
            otherType = Random.Range(0.0f, 1.0f);
            anim.SetFloat("OtherType", otherType);
            ir = false;
        }
        else
        {
            type = Random.Range(0.0f, 1.0f);
            anim.SetFloat("Type", type);
            ir = true;
        }
        anim.speed = Random.Range(0.7f, 1.0f);
    }

}
