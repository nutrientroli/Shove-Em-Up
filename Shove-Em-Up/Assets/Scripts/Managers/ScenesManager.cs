using UnityEngine.SceneManagement;

public static class ScenesManager {

    public enum SceneCode {
        MENU = 0,
        CHARACTER_SELECTOR = 1,
        GAME = 4,
        ENDGAME = 3
    }

    private static SceneCode currentScene;

    public static void ChangeScene(SceneCode _Scene) {
        currentScene = _Scene;
        SceneManager.LoadScene((int)_Scene);
    }

    public static SceneCode GetCurrentScene() {
        return currentScene;
    }
}
