using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHabilityScript : HabilityScript
{

    public override void UseHability()
    {
        base.UseHability();
        Debug.Log("Use Hability");
    }

    public override void DeactiveHability()
    {
        base.DeactiveHability();
        Debug.Log("Deactive Hability");
    }

    private void OnDrawGizmos()
    {
        if(active)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 2);
        }
    }
}
