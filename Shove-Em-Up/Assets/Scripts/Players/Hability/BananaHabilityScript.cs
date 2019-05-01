using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaHabilityScript : HabilityScript
{
    public GameObject bananaPrefab;
    public List<GameObject> bananas;

    protected override void Start()
    {
        base.Start();
        duration = 0.5f;
    }

    public override void UseHability()
    {
        base.UseHability();
        for (int i = 0; i < 1; i++)
        {
            bananas.Add(Instantiate(bananaPrefab, transform.position, bananaPrefab.transform.rotation));
            bananas[bananas.Count - 1].GetComponent<BananaScript>().SetMyPlayer(gameObject);
            bananas[bananas.Count - 1].GetComponent<BananaScript>().SetForward(gameObject.transform.forward);
            bananas[bananas.Count - 1].GetComponent<BananaScript>().SetSpeed(20);
        }
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
