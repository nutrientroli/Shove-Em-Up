using UnityEngine;

public class MenuScript : MonoBehaviour {
    public void OnButtonPlayPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.CHARACTER_SELECTOR);
    }

    public void OnButtonQuitPress() {
        Debug.Log("Quit Game");
    }

    public void OnButtonReplayPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.GAME);
    }

    public void OnButtonReturnToMenuPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.MENU);
    }
}
