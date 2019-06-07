using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifScript : MonoBehaviour
{
    public Image gif;
    public List<Sprite> frames = new List<Sprite>();
    private int frame = 0;
    private int framesPerSecond = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Play();
    }

    private void Play() {
        frame  = (int)(Time.time* framesPerSecond) % frames.Count;
        gif.sprite = frames[frame];
    }
}
