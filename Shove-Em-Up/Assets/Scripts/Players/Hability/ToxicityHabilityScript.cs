using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityHabilityScript : HabilityScript
{
    public GameObject prefabGas;

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
