using UnityEngine;


public class MenuScript : MonoBehaviour {

    [SerializeField] private Animation anim;
    [SerializeField] private Transform trnsMusicSelection;
    public FMOD.Studio.EventInstance audio1 = new FMOD.Studio.EventInstance();
    public FMOD.Studio.EventInstance audio2 = new FMOD.Studio.EventInstance();

    private void Start() {
        audio1 = SoundManager.GetInstance().PlaySoundAndGetSound(SoundManager.SoundEvent.MUSIC_INGAME, Camera.main.transform);
        audio2 = SoundManager.GetInstance().PlaySoundAndGetSound(SoundManager.SoundEvent.MUSIC_INIT, trnsMusicSelection);
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
        Application.Quit();
    }

    public void OnButtonReplayPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.GAME);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
    }

    public void OnButtonReturnToMenuPress() {
        ScenesManager.ChangeScene(ScenesManager.SceneCode.MENU);
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
    }

    public void Exit()
    {
        audio1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        audio2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
