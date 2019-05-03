using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyHabilityScript : HabilityScript
{
    public GameObject honeyPrefab;
    private GameObject honey;

    protected override void Start()
    {
        base.Start();
        duration = 0.5f;
    }

    public override void UseHability()
    {
        base.UseHability();

        honey = Instantiate(honeyPrefab, transform.position + transform.forward, honeyPrefab.transform.rotation);
        honey.GetComponent<HoneyScript>().SetMyPlayer(gameObject);
        honey.GetComponent<HoneyScript>().SetForward(gameObject.transform.forward);
        honey.GetComponent<HoneyScript>().SetSpeed(5);
        
    }

    public override void DesactiveHability()
    {
        base.DesactiveHability();
    }

    protected override void Update()
    {
        base.Update();
    }
}
