using UnityEngine;

public class MenuScript : MonoBehaviour {
    public void OnButtonPlayPress() {
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESSREADY_MENUSELECTION);
        ScenesManager.ChangeScene(ScenesManager.SceneCode.CHARACTER_SELECTOR);
    }

    public void OnButtonQuitPress() {
        Debug.Log("Quit Game");
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESSREADY_MENUSELECTION);
    }

    public void OnButtonReplayPress() {
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESSREADY_MENUSELECTION);
        ScenesManager.ChangeScene(ScenesManager.SceneCode.GAME);
    }

    public void OnButtonReturnToMenuPress() {
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.PRESSREADY_MENUSELECTION);
        ScenesManager.ChangeScene(ScenesManager.SceneCode.MENU);
    }
}
