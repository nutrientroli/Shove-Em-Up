using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHabilityScript : HabilityScript
{

    public GameObject prefabShield;
    private GameObject shield;

    private void Awake()
    {
        shield = Instantiate(prefabShield);
        shield.GetComponent<ShieldScript>().me = gameObject.GetComponent<PlayerScript>();
        shield.transform.position = transform.position;
        shield.SetActive(false);
    }
    public override void UseHability()
    {
        base.UseHability();
        shield.SetActive(true);
        shield.transform.position = transform.position;
    }

    public override void DeactiveHability()
    {
        base.DeactiveHability();
        shield.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        shield.transform.position = transform.position;
    }
}
