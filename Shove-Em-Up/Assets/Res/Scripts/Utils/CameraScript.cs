using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 initPosition;
    [SerializeField] private Vector3 finalPosition;
    private float lerpValue;
    [SerializeField] private float offset;

    // Start is called before the first frame update
    void Start() {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.Lerp(initPosition, finalPosition, lerpValue);
    }

    private void GetFinalPosition()
    {

    }
}
