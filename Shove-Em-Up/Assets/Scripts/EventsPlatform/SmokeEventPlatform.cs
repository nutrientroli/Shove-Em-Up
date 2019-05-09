using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEventPlatform : EventPlatformScript
{

    #region Variables
    [Header("Objects Configuration")] //Crear Tipo de objeto con el GameObject y tier?
    [SerializeField] private GameObject prefabSmoke;
    private GameObject smoke;


    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float timeToAction = 5.0f;

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
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions
    private float StartSmoke()
    {
        smoke = Instantiate(prefabSmoke);
        smoke.transform.position = Vector3.zero;
        
        return timeToAction * timeVariaton;
    }

    private float EndSmoke()
    {
        Debug.Log("End");
        ToxicityScript script = smoke.GetComponent<ToxicityScript>();
        if(script != null) script.exit = true;
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
