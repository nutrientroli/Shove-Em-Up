using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorScript : MonoBehaviour
{
    private float type = 0;
    
    public Animator anim;

    public List<Color> colors = new List<Color>();
    private int index = 0;

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
        type = Random.Range(0.0f, 1.0f);
        anim.SetFloat("Type", type);
        anim.speed = Random.Range(0.7f, 1.0f);
    }

}
