using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityHabilityScript : HabilityScript
{
    public GameObject prefabGas;

    private void Awake()
    {
        canvasPush.SetToxicityHability();
    }

    public override void UseHability()
    {
        base.UseHability();
        Instantiate(prefabGas, gameObject.transform.position, prefabGas.transform.rotation);
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
