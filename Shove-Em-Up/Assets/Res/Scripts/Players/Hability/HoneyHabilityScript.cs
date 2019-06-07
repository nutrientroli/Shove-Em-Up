using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyHabilityScript : HabilityScript
{
    public GameObject honeyPrefab;
    private List<HoneyScript> honey = new List<HoneyScript>();

    protected override void Start()
    {
        base.Start();
        duration = 0.5f;

        float angle = 45;
        Vector3 forward;
        for (int i = 0; i < 8; i++)
        {
            forward = new Vector3(Mathf.Cos(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2), 0, Mathf.Sin(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2));
            forward = gameObject.transform.rotation * forward;
            honey.Add(Instantiate(honeyPrefab, transform.position + forward, honeyPrefab.transform.rotation).GetComponent<HoneyScript>());
            honey[honey.Count - 1].SetMyPlayer(gameObject);
            honey[honey.Count - 1].SetForward((forward).normalized);
            honey[honey.Count - 1].SetSpeed(5);
            honey[honey.Count - 1].SetScale(honey[honey.Count - 1].gameObject.transform.localScale);
            honey[honey.Count - 1].gameObject.SetActive(false);
        }

    }

    public override void UseHability()
    {
        base.UseHability();

        float angle = 45;
        Vector3 forward;
        for (int i = 0; i < honey.Count; i++)
        {
            forward = new Vector3(Mathf.Cos(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2), 0, Mathf.Sin(Mathf.PI * 2 * (i - 1) / 360 * angle + Mathf.PI / 2));
            forward = gameObject.transform.rotation * forward;
            honey[i].Restart();
            honey[i].gameObject.SetActive(true);
            honey[i].gameObject.transform.position = transform.position + forward;
            honey[i].SetForward((forward).normalized);

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
