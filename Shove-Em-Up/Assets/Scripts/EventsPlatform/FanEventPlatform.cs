using System.Collections.Generic;
using UnityEngine;

public class FanEventPlatform : EventPlatformScript
{
    #region Variables
    [Header("Objects Configuration")] //Crear Tipo de objeto con el GameObject y tier?

    public Transform posToInstiantiate;
    public GameObject ventiladorPrefab;
    private GameObject fan;
    

    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float timeToAction = 10.0f;

    [Header("Extra Configuration")]
    public float timeVariaton = 1.0f;
    #endregion

    #region ParentFunctions
    public override void Init()
    {
        base.Init();
        type = TypeEvent.TIME;
        listEvent.Add(Wait);
        listEvent.Add(Action);
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions
    private float Wait()
    {
        return waitTime;
    }

    private float Action()
    {
        fan = Instantiate(ventiladorPrefab, posToInstiantiate);
        return timeToAction * timeVariaton;
    }
    private float End()
    {
        return -1;
    }
    #endregion

    #region CustomFunctions

    #endregion
}
