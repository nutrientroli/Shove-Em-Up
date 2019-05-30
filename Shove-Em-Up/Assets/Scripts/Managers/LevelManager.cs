using UnityEngine;

public class LevelManager{

    #region Singleton Pattern
    private static LevelManager instance;
    private LevelManager() { }
    public static LevelManager GetInstance() {
        if (instance == null) instance = new LevelManager();
        return instance;
    }
    #endregion

    public int currentEvent;
    public int events;
    public bool eventActive;

    public void FinnishGame() {
        //Cambiar a mostrar puntuaciones
        ScenesManager.ChangeScene(ScenesManager.SceneCode.ENDGAME);
        currentEvent = 0;
    }

    public void PassEvent() {
        currentEvent++;
        if (events <= currentEvent) FinnishGame();
    }

    public void SetEventState(bool _active) {
        eventActive = _active;
    }

    public bool GetEventState() {
        return eventActive;
    }

    public void ResetGame(int _events) {
        events = _events;
    }

}
