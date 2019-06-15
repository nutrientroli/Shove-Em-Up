
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsManager : MonoBehaviour {
    [SerializeField] private List<PieceRuleteScript> listPieces = new List<PieceRuleteScript>();
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
    private bool finnishgame = false;
    private float timeCounter = 0;

    public Text counter;
    [SerializeField] private bool test = false;
    [SerializeField] private int indexTest = 0;

    [SerializeField] private ScreenCanvasScript screen;
    private List<EventPlatformScript> listExecutedEvents = new List<EventPlatformScript>();

    private void Start() {
        listPieces[indexEvent].UnSelectPiece();
        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_32);
    }

    private void Update() {
        if (!finnishgame) {
            if (inSelection) {
                if (!wait) {
                    currentTime += Time.deltaTime;
                    if (currentTime >= timeToChangeRulette) {
                        DuringSelectEvent();
                        currentTime = 0;
                        if (Random.Range(0, 10) > 8) PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_10);

                    }
                } else {
                    currentTime += Time.deltaTime;
                    if (currentTime >= timeWait) {
                        EndSelectEvent();
                        currentTime = 0;
                    }
                }
            } else {
                if (eventPlatform != null) {
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
                    } else if (eventPlatform == null) {
                        StartSelectEvent();
                    }
                } else {
                    StartSelectEvent();
                }
            }
        }
    }

    private void FinnishEvent() {
        eventPlatform.active = false;
        eventPlatform = null;
        LevelManager.GetInstance().SetEventState(false);
        finnishgame = LevelManager.GetInstance().PassEvent();
        if(finnishgame) screen.SetEndGame();
        PlayersManager.GetInstance().RespawnFinnishEvent();
    }

    private void ForceFinnishEvent() {
        eventPlatform.ForceFinnish();
        if (Random.Range(0, 10) > 5) PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_3);
        else PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_33);
    }

    private void StartSelectEvent() {
        screen.HideAll();
        foreach (PieceRuleteScript piece in listPieces) piece.UnSelectPiece();
        inSelection = true;
        do {
            indexIncrement = Random.Range(10, 40);
        } while (!CheckEventNextEvent(indexIncrement, indexEvent));
        if (Random.Range(0, 10) > 5) PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_6);
        else PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_9);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.RULETA_4);
    }

    private void DuringSelectEvent() {
        indexIncrement--;
        listPieces[indexEvent].UnSelectPiece();
        indexEvent++;
        if (indexEvent >= listEvents.Count) indexEvent = 0;
        listPieces[indexEvent].SelectPiece();
        if (indexIncrement <= 0) wait = true;
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.RULETA_2);
    }

    private void EndSelectEvent() {
        inSelection = false;
        wait = false; 
        listPieces[indexEvent].UnSelectPiece();
        //foreach (PieceRuleteScript piece in listPieces) piece.SelectPiece();
        if (test) indexEvent = indexTest;
        eventPlatform = listEvents[indexEvent];
        listExecutedEvents.Add(listEvents[indexEvent]);
        eventPlatform.Init();
        screen.SetEvent(indexEvent);
        LevelManager.GetInstance().SetEventState(true);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.RULETA_3);
    }

    public bool CheckEndEvent() {
        return (eventPlatform != null && eventPlatform.active && !eventPlatform.execute);
    }

    public bool CheckForceEndEvent() {
        //Comentar para testear.
        //return (PlayersManager.GetInstance().CheckLimitPlayersDeathInEvent() && eventPlatform != null && !eventPlatform.forceFinnish);
        return false;
    }

    public void ShowNumberCounter(float _dt) {
        /*timeCounter += _dt;
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
        }*/
    }

    private bool CheckEventNextEvent(int _increment, int _index) {
        while (_increment>0) {
            _increment--;
            _index++;
            if (_index >= listEvents.Count) _index = 0;
        }
        if (listExecutedEvents.Count == listEvents.Count) listExecutedEvents.Clear();
        return !(listExecutedEvents.Contains(listEvents[_index]));
    }
    
}
