using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventPlatform : EventPlatformScript
{

    #region Variables
    [Header("Objects Configuration")] //Crear Tipo de objeto con el GameObject y tier?
    [SerializeField] private GameObject prefabItem;
    private List<Vector3> listRandomPositions = new List<Vector3>();
    private List<bool> listRandomPositionsOcuped = new List<bool>();
    private List<GameObject> listItems = new List<GameObject>();
    [SerializeField] private Transform transInitial;


    [Header("Event Configuration")]
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float timeToAction = 2.0f;

    [Header("Extra Configuration")]
    public float timeVariaton = 1.0f;
    #endregion

    #region ParentFunctions
    public override void Init()
    {
        base.Init();
        ClearData();
        AddPositions();
        type = TypeEvent.TIME;
        for (int i=0; i<16; i++) {
            if(i%2==0) listEvent.Add(SpawnItem);
            else listEvent.Add(Wait);
        }
        listEvent.Add(End);
    }

    private void ClearData()
    {
        listRandomPositions.Clear();
        listRandomPositionsOcuped.Clear();
        listItems.Clear();
    }
    #endregion

    #region EventFunctions
    private float SpawnItem()
    {
        Vector3 vector = GetRandomPosition();
        if(vector != Vector3.zero) {
            GameObject item = Instantiate(prefabItem);
            item.transform.position = vector;
            listItems.Add(item);
        }

        return timeToAction * timeVariaton;
    }

   

    private float Wait()
    {
        return waitTime;
    }
    private float End() {
        for (int i = 0; i< listItems.Count; i++) {
            if (listItems[i] != null) {
                Destroy(listItems[i]);
            }
        }
        return -1;
    }
    #endregion

    #region CustomFunctions
    private void AddPositions()
    {
        listRandomPositions.Add(new Vector3(transInitial.position.x + 10, transInitial.position.y + 0.5f, 0));
        listRandomPositions.Add(new Vector3(transInitial.position.x - 10, transInitial.position.y + 0.5f, 0));
        listRandomPositions.Add(new Vector3(0, transInitial.position.y + 0.5f, transInitial.position.z + 10));
        listRandomPositions.Add(new Vector3(0, transInitial.position.y + 0.5f, transInitial.position.z - 10));
        listRandomPositions.Add(new Vector3(transInitial.position.x + 7.071f, transInitial.position.y + 0.5f, transInitial.position.z + 7.071f));
        listRandomPositions.Add(new Vector3(transInitial.position.x + 7.071f, transInitial.position.y + 0.5f, transInitial.position.z - 7.071f));
        listRandomPositions.Add(new Vector3(transInitial.position.x - 7.071f, transInitial.position.y + 0.5f, transInitial.position.z + 7.071f));
        listRandomPositions.Add(new Vector3(transInitial.position.x - 7.071f, transInitial.position.y + 0.5f, transInitial.position.z - 7.071f));
        for (int i=0; i<8; i++) {
            listRandomPositionsOcuped.Add(false);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 report = Vector3.zero;
        bool check = false;
        int icheck = 0;
        while (!check && icheck<=listRandomPositions.Count) {
            int iCheck = 1;
            foreach (bool pos in listRandomPositionsOcuped) {
                if (pos) iCheck++;
            }
            if (iCheck < (listRandomPositionsOcuped.Count * 3)) {
                int random = UnityEngine.Random.Range(0, 8);
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
