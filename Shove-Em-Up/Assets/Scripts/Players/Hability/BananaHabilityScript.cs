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
        float angle = 15;
        Vector3 forward;
        for (int i = 0; i < 3; i++)
        {
            bananas.Add(Instantiate(bananaPrefab, transform.position, bananaPrefab.transform.rotation));
            bananas[bananas.Count - 1].GetComponent<BananaScript>().SetMyPlayer(gameObject);
            forward = new Vector3(Mathf.Cos(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI/2),0, Mathf.Sin(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2));
            forward = gameObject.transform.rotation* forward;
            bananas[bananas.Count - 1].GetComponent<BananaScript>().SetForward((forward).normalized);
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
