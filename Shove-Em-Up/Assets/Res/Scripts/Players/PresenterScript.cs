using UnityEngine;


public static class PresenterSound {
    private static SoundManager.SoundEvent soundActive = SoundManager.SoundEvent.NONE;
    private static FMOD.Studio.EventInstance audio;

    public static bool IsPresenterTalks() {
        return soundActive != SoundManager.SoundEvent.NONE;
    }

    public static void PresenterTalks(SoundManager.SoundEvent _event, bool _force = false) {
        if (_force) PresenterMute();
        if (!IsPresenterTalks()) {
            soundActive = _event;
            audio = SoundManager.GetInstance().PlaySoundAndGetSound(soundActive);
        }
    }

    public static void PresenterMute() {
        audio.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundActive = SoundManager.SoundEvent.NONE;
    }
}



public class PresenterScript : MonoBehaviour {
    private float currentTime = 0;
    [SerializeField] private float time = 5;

    void Update() {
        if (PresenterSound.IsPresenterTalks()) {
            currentTime += Time.deltaTime;
            if (currentTime >= time) {
                currentTime = 0;
                PresenterSound.PresenterMute();
            }
        } else {
            currentTime = 0;
        }
    } 
}
