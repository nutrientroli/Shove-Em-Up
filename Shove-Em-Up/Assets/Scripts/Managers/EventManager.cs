
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    [SerializeField] private List<EventPlatformScript> listEvents = new List<EventPlatformScript>();
    [SerializeField] private int indexEvent = 0;
    EventPlatformScript eventPlatform;

    private void Awake() {

    }

    private void Start() {
        eventPlatform = listEvents[indexEvent];
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
