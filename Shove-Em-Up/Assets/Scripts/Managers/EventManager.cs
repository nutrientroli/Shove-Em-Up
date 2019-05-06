﻿
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private List<EventPlatformScript> listEvents = new List<EventPlatformScript>();
    EventPlatformScript eventPlatform;

    private void Awake() {
        eventPlatform = gameObject.GetComponent<EventPlatformScript>();
        if (eventPlatform == null) Debug.LogError("No hay evento!");
    }

    private void Start() {
        eventPlatform.Init();
    }

    private void Update() {
        switch (eventPlatform.type) {
            case EventPlatformScript.TypeEvent.TIME:
                if(eventPlatform.execute) eventPlatform.Run(Time.deltaTime);
                break;
            case EventPlatformScript.TypeEvent.STEP: break;
            default: break;
        }
    }
    
}
