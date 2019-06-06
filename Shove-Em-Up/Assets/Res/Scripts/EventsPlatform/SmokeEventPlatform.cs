using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEventPlatform : EventPlatformScript
{

    #region Variables
    [Header("Objects Configuration")] //Crear Tipo de objeto con el GameObject y tier?
    [SerializeField] public ToxicityScript smoke;
    [SerializeField] private Transform transfInitial;


    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float timeToAction = 1.0f;

    [Header("Extra Configuration")]
    public float timeVariaton = 1.0f;
    #endregion

    #region ParentFunctions
    public override void Init()
    {
        base.Init();
        type = TypeEvent.TIME;
        listEvent.Add(StartSmoke);
        listEvent.Add(EndSmoke);
        listEvent.Add(StartSmoke);
        listEvent.Add(EndSmoke);
        listEvent.Add(StartSmoke);
        listEvent.Add(EndSmoke);
        listEvent.Add(End);
    }

    public override void ForceFinnish()
    {
        base.ForceFinnish();
        listEvent.Add(EndSmoke);
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions
    private float StartSmoke()
    {
        smoke.transform.position = transfInitial.position;
        smoke.gameObject.SetActive(true);
        smoke.Active();
        
        return timeToAction * timeVariaton;
    }

    private float EndSmoke()
    {
        if (smoke != null)
        {
            ToxicityScript script = smoke.GetComponent<ToxicityScript>();
            if (script != null) script.exit = true;
        }
        return timeToAction * timeVariaton;
    }

    private float Wait()
    {
        return waitTime;
    }
    private float End()
    {
        return -1;
    }
    #endregion

    #region CustomFunctions

    #endregion
}
