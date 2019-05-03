using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHabilityScript : HabilityScript
{

    public GameObject prefabShield;
    private GameObject shield;
    private int life = 3;
    private bool usada = false;
    public List<Material> materials;

    private void Awake()
    {
        shield = Instantiate(prefabShield);
        shield.GetComponent<ShieldScript>().me = gameObject.GetComponent<PlayerScript>();
        shield.transform.position = transform.position;
        shield.SetActive(false);
    }



    protected override void Start()
    {
        base.Start();
        life = 3;
        duration = 5;
        canvasPush.SetShieldHability();
    }

    public override void UseHability()
    {
        base.UseHability();
        usada = true;
        life = 3;
        shield.GetComponent<Renderer>().material = materials[3 - life];
        modToMe = gameObject.AddComponent<ShieldModifierScript>();
        shield.SetActive(true);
        shield.transform.position = transform.position;
    }

    public override void DesactiveHability()
    {
        base.DesactiveHability();
        if (gameObject.GetComponent<ShieldModifierScript>() != null)
            gameObject.GetComponent<PlayerScript>().RemoveMod(gameObject.GetComponent<ShieldModifierScript>());
        usada = false;
        shield.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        shield.transform.position = transform.position;
        if(usada)
        {
            if (life <= 0)
                DesactiveHability();
        }

    }

    public void DecreseLifeShield()
    {
        life--;
        if(life > 0)
            shield.GetComponent<Renderer>().material = materials[3 - life];
    }
}
