using UnityEngine;


public static class PresenterSound {
    private static SoundManager.SoundEvent soundActive = SoundManager.SoundEvent.NONE;
    private static FMOD.Studio.EventInstance audio;
    private static float time = 1;

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
        time = 0;
        audio.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundActive = SoundManager.SoundEvent.NONE;
    }

    public static float CheckTime(float _time)
    {
        if (time != 0)
            return _time;
        else
        {
            time = 1; 
            return time;
        }
    }
}



public class PresenterScript : MonoBehaviour {
    private float currentTime = 0;
    private float time = 5f;

    void Update()
    {
        currentTime = PresenterSound.CheckTime(currentTime);
        if (PresenterSound.IsPresenterTalks())
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                currentTime = 0;
                PresenterSound.PresenterMute();
            }
        }
        else
        {
            currentTime = 0;
        }
    }
} 

