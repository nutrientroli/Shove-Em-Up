using UnityEngine;

public class MenuScript : MonoBehaviour {

    [SerializeField] private Animation anim;

    public void OnButtonPlayPress() {
        //Vamos al Selector de personajes
        anim.Play();
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
