using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorScript : MonoBehaviour
{
    private float type = 0;
    
    public Animator anim;

    // Start is called before the first frame update
    void Start() {
        type = Random.Range(0, 1);
        anim.SetFloat("Type", type);
    }

}
