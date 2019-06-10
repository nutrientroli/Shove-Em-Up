using UnityEngine;


public static class PresenterSound {
    private static SoundManager.SoundEvent soundActive = SoundManager.SoundEvent.NONE;

    public static bool IsPresenterTalks() {
        return soundActive != SoundManager.SoundEvent.NONE;
    }

    public static void PresenterTalks(SoundManager.SoundEvent _event, bool _force = false) {
        if (_force) PresenterMute();
        if (!IsPresenterTalks()) {
            soundActive = _event;
            SoundManager.GetInstance().PlaySound(soundActive, null, true);
        }
    }

    public static void PresenterMute() {
        SoundManager.GetInstance().StopSoundOnTime(soundActive);
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
