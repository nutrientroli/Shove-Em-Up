using UnityEngine;

public class MenuScript : MonoBehaviour {
    public void OnButtonPlayPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.CHARACTER_SELECTOR);
    }

    public void OnButtonQuitPress() {
        Debug.Log("Quit Game");
    }
}
