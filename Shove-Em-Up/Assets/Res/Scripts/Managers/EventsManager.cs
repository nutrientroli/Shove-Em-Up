
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsManager : MonoBehaviour {
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
    private float timeCounter = 0;
    public Text counter;

    [Header("Params Testing")]
    [SerializeField] private bool Testing = false;
    [SerializeField] private int indexTesting = 0;

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
                    if (Random.Range(0, 10) > 9) SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_10);

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
                else if (eventPlatform.execute) {
                    if (!eventPlatform.counterIsShow) {
                        switch (eventPlatform.type) {
                            case EventPlatformScript.TypeEvent.TIME:
                                if (eventPlatform.execute) eventPlatform.Run(Time.deltaTime);
                                break;
                            case EventPlatformScript.TypeEvent.STEP: break;
                            default: break;
                        }
                    } else {
                        ShowNumberCounter(Time.deltaTime);
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
        LevelManager.GetInstance().PassEvent();
        PlayersManager.GetInstance().RespawnFinnishEvent();
    }

    private void ForceFinnishEvent() {
        if(!Testing) eventPlatform.ForceFinnish();
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_3);
    }

    private void StartSelectEvent() {
        inSelection = true;
        indexIncrement = Random.Range(10, 40);
        if (Random.Range(0, 10) > 5) SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_5);
        else SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESENTADOR_4);
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
        if(Testing) eventPlatform = listEvents[indexTesting];
        else eventPlatform = listEvents[indexEvent];
        eventPlatform.Init();
        LevelManager.GetInstance().SetEventState(true);
    }

    public bool CheckEndEvent() {
        return (eventPlatform != null && eventPlatform.active && !eventPlatform.execute);
    }

    public bool CheckForceEndEvent() {
        //Comentar para testear.
        if (Testing) return false;
        return (PlayersManager.GetInstance().CheckLimitPlayersDeathInEvent() && eventPlatform != null && !eventPlatform.forceFinnish);
        //return false;
    }

    public void ShowNumberCounter(float _dt) {
        timeCounter += _dt;
        if (counter.IsActive()) {
            if (timeCounter < 1) counter.text = "3";
            else if (timeCounter >= 1 && timeCounter < 2) counter.text = "2";
            else if (timeCounter >= 2 && timeCounter < 3)  counter.text = "1";
            else {
                timeCounter = 0;
                eventPlatform.counterIsShow = false;
                counter.text = "3";
                counter.gameObject.SetActive(false);
            }
        } else {
            counter.gameObject.SetActive(true);
        }

    }
    
}
