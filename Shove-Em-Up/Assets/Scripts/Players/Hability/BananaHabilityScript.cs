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
        for (int i = 0; i < 3; i++)
        {
            bananas.Add(Instantiate(bananaPrefab, transform.position, bananaPrefab.transform.rotation));
            bananas[bananas.Count - 1].GetComponent<BananaScript>().SetMyPlayer(gameObject);
            Vector3 forward;
            float angle = 45;
            float myAngle = Quaternion.ToEulerAngles(gameObject.transform.rotation).y * 57.15f;
            forward = new Vector3(Mathf.Cos(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI/2),0, Mathf.Sin(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2));
            forward = Quaternion.AngleAxis(myAngle, Vector3.up) * forward;
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
