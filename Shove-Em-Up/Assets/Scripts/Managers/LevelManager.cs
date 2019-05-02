public class LevelManager{

    #region Singleton Pattern
    private static LevelManager instance;
    private LevelManager() { }
    public static LevelManager GetInstance() {
        if (instance == null) instance = new LevelManager();
        return instance;
    }
    #endregion

    public int players;

    public void Start() {
        players = 4;
    }

    public void FinnishGame() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.ENDGAME);
    }
}
