using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventPlatform : EventPlatformScript
{

    #region Variables
    [Header("Objects Configuration")] //Crear Tipo de objeto con el GameObject y tier?
    [SerializeField] private GameObject prefabItem;
    public GameObject HotPoint;
    private List<Vector3> listRandomPositions = new List<Vector3>();
    private List<bool> listRandomPositionsOcuped = new List<bool>();
    private List<GameObject> listItems = new List<GameObject>();
    [SerializeField] private Transform transInitial;
    [SerializeField] private List<Transform> itemSpawnPositions_Lst;


    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float timeToAction = 2.0f;

    [Header("Extra Configuration")]
    public float timeVariaton = 1.0f;
    #endregion

    #region ParentFunctions
    public override void Init() {
        base.Init();
        ClearData();
        AddPositions();
        type = TypeEvent.TIME;
        listEvent.Add(Action2);
        for (int i=0; i<12; i++) {
            if(i%2==0) listEvent.Add(SpawnItem);
            else listEvent.Add(Wait);
        }
        listEvent.Add(End);

    }

    public override void ForceFinnish() {
        base.ForceFinnish();
        listEvent.Add(Wait);
        listEvent.Add(End);
    }
    #endregion

    #region EventFunctions
    private float SpawnItem() {
        Vector3 vector = GetRandomPosition();
        if(vector != Vector3.zero) {
            GameObject item = Instantiate(prefabItem);
            item.transform.position = vector;
            listItems.Add(item);
        }
        return timeToAction * timeVariaton;
    }

    private float Action2()
    {
        HotPoint.SetActive(true);
        return 0;
    }

    private float Wait() {
        return waitTime;
    }

    private float End() {
        HotPoint.SetActive(false);
        for (int i = 0; i< listItems.Count; i++) {
            if (listItems[i] != null)  Destroy(listItems[i]);
        }
        return -1;
    }
    #endregion

    #region CustomFunctions
    private void ClearData() {
        listRandomPositions.Clear();
        listRandomPositionsOcuped.Clear();
        listItems.Clear();
    }

    private void AddPositions() {

        // Manual
        //listRandomPositions.Add(new Vector3(3.12f, 3.69f, -1.19f));
        //listRandomPositions.Add(new Vector3(-3.61f, 3.69f, -1.19f));
        //listRandomPositions.Add(new Vector3(-3.25f, 3.69f, -13.04f));
        //listRandomPositions.Add(new Vector3(3.19f, 3.69f, -12.97f));
        //listRandomPositions.Add(new Vector3(6.53f, 3.69f, -6.77f));
        //listRandomPositions.Add(new Vector3(-6.72f, 3.69f, -6.77f));

        // auto
        foreach (Transform transform in itemSpawnPositions_Lst)
        {
            listRandomPositions.Add(transform.position);
        }

        for (int i=0; i<6; i++)  listRandomPositionsOcuped.Add(false);
    }

    private Vector3 GetRandomPosition() {
        Vector3 report = Vector3.zero;
        bool check = false;
        int icheck = 0;
        while (!check && icheck<=listRandomPositions.Count) {
            int iCheck = 1;
            foreach (bool pos in listRandomPositionsOcuped) {
                if (pos) iCheck++;
            }
            if (iCheck < (listRandomPositionsOcuped.Count * 3)) {
                int random = UnityEngine.Random.Range(0, 6);
                if (!listRandomPositionsOcuped[random]) {
                    check = true;
                    report = listRandomPositions[random];
                    listRandomPositionsOcuped[random] = true;
                }
            }
            icheck++;
        }
        return report;
    }
    #endregion
}
