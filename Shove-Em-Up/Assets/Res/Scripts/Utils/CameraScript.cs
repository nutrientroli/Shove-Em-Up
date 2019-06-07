using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 finalPosition;
    [SerializeField] private float offset;
    private List<Vector3> finalsPositions;
    private float lerpValue = 0;
    private bool increaseValue = true;
    public float speed = 0.5f;
    public float time = 1.0f;
    private float currentTime = 0;
    private float variationTime;
    private bool wait = true;

    // Start is called before the first frame update
    void Start() {
        initPosition = transform.position;
        AddFinalsPositions();
        GetFinalPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!wait) {
            if (increaseValue) {
                lerpValue += (Time.deltaTime * speed);
                if (lerpValue > 1) {
                    increaseValue = false;
                    wait = true;
                    variationTime = Random.Range(time * 0.5f, time);
                }
            } else {
                lerpValue -= (Time.deltaTime * speed);
                if (lerpValue < 0) {
                    increaseValue = true;
                    GetFinalPosition();
                    wait = true;
                    variationTime = Random.Range(time * 0.5f, time);
                }
            }
            transform.position = Vector3.Lerp(initPosition, finalPosition, lerpValue);
        } else {
            currentTime += Time.deltaTime;
            if (currentTime >= variationTime) {
                currentTime = 0;
                wait = false;
            }
        }
    }

    private void AddFinalsPositions()
    {
        finalsPositions = new List<Vector3>();
        for (int i=0; i<8; i++) finalsPositions.Add(new Vector3(transform.position.x + Random.Range(-offset, offset), transform.position.y + Random.Range(-offset, offset), transform.position.z));
    }

    private void GetFinalPosition()
    {
        finalPosition = finalsPositions[Random.Range(0, finalsPositions.Count - 1)];
        Debug.Log(finalPosition);
    }
}
