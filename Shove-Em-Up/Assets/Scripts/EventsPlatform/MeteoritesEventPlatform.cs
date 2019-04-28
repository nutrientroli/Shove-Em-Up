using System.Collections.Generic;
using UnityEngine;

public class MeteoritesEventPlatform : EventPlatformScript 
{
    #region Variables
    [Header("Objects Configuration")]
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject feedback;
    private List<GameObject> poolMeteors;
    private List<GameObject> poolFeedback;
    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float timeToAction = 5.0f;
    [Header("Extra Configuration")]
    public float timeVariaton = 1.0f;
    [SerializeField] private int pool = 5;
    #endregion

    #region ParentFunctions
    public override void Init() {
        base.Init();
        type = TypeEvent.TIME;
        InitPools();
        
        listEvent.Add(Wait);
        for (int i = 0; i < pool; i++) {
            listEvent.Add(FeedBack);
            listEvent.Add(Action);
        }
        listEvent.Add(Restart);
    }
    #endregion

    #region EventFunctions
    private float FeedBack() {
        //Seleccionar punto de caida en punto del mapa donde puede caer.
        return timeToAction * timeVariaton;
    }

    private float Action() {
        //Crear meteoro y que se autogestione.
        return timeToAction * timeVariaton;
    }

    private float Wait() {
        return waitTime;
    }

    private float Restart() {
        return -2;
    }
    #endregion

    #region CustomFunctions
    private void InitPools()
    {
        GameObject _obj;
        for (int i = 0; i < pool; i++) {
            _obj = Instantiate(meteor);
            _obj.SetActive(false);
            poolMeteors.Add(_obj);

            _obj = Instantiate(feedback);
            _obj.SetActive(false);
            poolFeedback.Add(_obj);
        }
    }
    #endregion
}
