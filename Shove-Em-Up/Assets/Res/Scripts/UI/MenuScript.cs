using UnityEngine;

public class MenuScript : MonoBehaviour {

    [SerializeField] private Animation anim;
    [SerializeField] private Transform trnsMusicSelection;

    private void Start() {
        SoundManager.GetInstance().StopSound(SoundManager.SoundEvent.MUSIC_INGAME);
        SoundManager.GetInstance().StopSound(SoundManager.SoundEvent.MUSIC_INIT);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MUSIC_INGAME, Camera.main.transform, false, "", SoundManager.SoundEventType.SCRATT);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MUSIC_INIT, trnsMusicSelection, false, "", SoundManager.SoundEventType.SCRATT);
        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_11);
    }



    public void OnButtonPlayPress() {
        //Vamos al Selector de personajes
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
        anim.Play();
    }

    public void OnButtonQuitPress() {
        Debug.Log("Quit Game");
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
    }

    public void OnButtonReplayPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.GAME);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
    }

    public void OnButtonReturnToMenuPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.MENU);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
    }
}
