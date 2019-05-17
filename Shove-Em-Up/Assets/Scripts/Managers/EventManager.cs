
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    [SerializeField] private List<Renderer> listPieces = new List<Renderer>();
    [SerializeField] private Material selectedMaterial;
    private Material unSelectedMaterial;
    [SerializeField] private List<EventPlatformScript> listEvents = new List<EventPlatformScript>();
    [SerializeField] private int indexEvent = 0;
    EventPlatformScript eventPlatform;
    private bool inSelection = false;
    [SerializeField] private int indexIncrement = 0;
    private float timeToChangeRulette = 0.3f;
    private float timeWait = 1.5f;
    private float currentTime = 0;
    private bool wait = false;

    private void Start() {
        unSelectedMaterial = ((Renderer)listPieces[indexEvent]).material;
    }

    private void Update() {
        if (inSelection) {
            if (!wait) {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToChangeRulette) {
                    DuringSelectEvent();
                    currentTime = 0;
                }
            } else {
                currentTime += Time.deltaTime;
                if (currentTime >= timeWait) {
                    EndSelectEvent();
                    currentTime = 0;
                }
            }
        } else {
            if (eventPlatform != null)
            {
                if (CheckEndEvent()) FinnishEvent();
                else if (CheckForceEndEvent()) ForceFinnishEvent();
                else if (eventPlatform.execute)
                {
                    switch (eventPlatform.type)
                    {
                        case EventPlatformScript.TypeEvent.TIME:
                            if (eventPlatform.execute) eventPlatform.Run(Time.deltaTime);
                            break;
                        case EventPlatformScript.TypeEvent.STEP: break;
                        default: break;
                    }
                }
                else if (eventPlatform == null)
                {
                    StartSelectEvent();
                }
            } else {
                StartSelectEvent();
            }
        }
    }

    private void FinnishEvent() {
        eventPlatform.active = false;
        eventPlatform = null;
        LevelManager.GetInstance().SetEventState(false);
        PlayersManager.GetInstance().RespawnFinnishEvent();
    }

    private void ForceFinnishEvent() {
        eventPlatform.ForceFinnish();
    }

    private void StartSelectEvent() {
        inSelection = true;
        indexIncrement = Random.Range(10, 40);
    }

    private void DuringSelectEvent() {
        indexIncrement--;
        ((Renderer)listPieces[indexEvent]).material = unSelectedMaterial;
        indexEvent++;
        if (indexEvent >= listEvents.Count) indexEvent = 0;
        unSelectedMaterial = ((Renderer)listPieces[indexEvent]).material;
        ((Renderer)listPieces[indexEvent]).material = selectedMaterial;
        if (indexIncrement <= 0) wait = true;
    }

    private void EndSelectEvent() {
        inSelection = false;
        wait = false; 
        ((Renderer)listPieces[indexEvent]).material = unSelectedMaterial;
        eventPlatform = listEvents[indexEvent];
        eventPlatform.Init();
        LevelManager.GetInstance().SetEventState(true);
    }

    public bool CheckEndEvent() {
        return (eventPlatform != null && eventPlatform.active && !eventPlatform.execute);
    }

    public bool CheckForceEndEvent() {
        return (PlayersManager.GetInstance().CheckLimitPlayersDeathInEvent() && eventPlatform != null && !eventPlatform.forceFinnish);
    }
    
}
