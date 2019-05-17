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
        ScenesManager.ChangeScene(ScenesManager.SceneCode.ENDGAME);
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


}
