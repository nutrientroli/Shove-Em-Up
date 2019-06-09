using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesManager {

    public enum SceneCode {
        MENU = 0,
        GAME = 1,
        ENDGAME = 2,
        LOAD_SCENE = 3
    }

    private static SceneCode currentScene;
    private static SceneCode loadScene;

    public static void ChangeScene(SceneCode _Scene) {
        loadScene = _Scene;
        currentScene = SceneCode.LOAD_SCENE;
        SceneManager.LoadScene((int)currentScene);
    }

    public static AsyncOperation ChangeSceneLoading() {
        currentScene = loadScene;
        return SceneManager.LoadSceneAsync((int)currentScene);
    }

    public static SceneCode GetCurrentScene() {
        return currentScene;
    }

    public static SceneCode GetLoadScene() {
        return loadScene;
    }
}
