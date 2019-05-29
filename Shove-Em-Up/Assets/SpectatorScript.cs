using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorScript : MonoBehaviour
{
    public float type = 0;
    public Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim.SetFloat("Type", type);
    }

}
