using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyHabilityScript : HabilityScript
{
    public GameObject honeyPrefab;
    private List<GameObject> honey = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        duration = 0.5f;
    }

    public override void UseHability()
    {
        base.UseHability();

        float angle = 45;
        Vector3 forward;
        for (int i = 0; i < 8; i++)
        {
            forward = new Vector3(Mathf.Cos(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2), 0, Mathf.Sin(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2));
            forward = gameObject.transform.rotation * forward;
            GameObject go = Instantiate(honeyPrefab, transform.position + forward, honeyPrefab.transform.rotation);
            honey.Add(go);
            honey[honey.Count - 1].GetComponent<HoneyScript>().SetMyPlayer(gameObject);
            honey[honey.Count - 1].GetComponent<HoneyScript>().SetForward((forward).normalized);
            honey[honey.Count - 1].GetComponent<HoneyScript>().SetSpeed(5);
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
