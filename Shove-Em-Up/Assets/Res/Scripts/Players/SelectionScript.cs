using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedAnimation(float _speed)
    {
        anim.speed = _speed;

    }

    public void SetReady()
    {
        anim.SetTrigger("Ready");
    }

    public void SetPlayerPodium(int _player) {
        anim.SetInteger("Position", _player);
        anim.SetFloat("Blend", 0.2f);
    }
}
