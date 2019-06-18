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
    private float otherCurrentTime = 4f;


    void Update() {
        currentTime = PresenterSound.CheckTime(currentTime);
        if (PresenterSound.IsPresenterTalks()) {
            currentTime += Time.deltaTime;
            if (currentTime >= time) {
                currentTime = 0;
                PresenterSound.PresenterMute();
            }
        } else {
            currentTime = 0;
        }

        otherCurrentTime += Time.deltaTime;
        if (otherCurrentTime >= 10)
        {
            otherCurrentTime = otherCurrentTime - 2;
            if (UnityEngine.Random.Range(0, 10) > 7)
            {
                otherCurrentTime = 0;
                int puntuaciónMax = -100;
                int player = 0;
                for (int i = 0; i < PlayersManager.GetInstance().GetNumberOfPlayers(); i++)
                {
                    if (puntuaciónMax < ScoreManager.GetInstance().GetPoints(i + 1))
                    {
                        puntuaciónMax = ScoreManager.GetInstance().GetPoints(i + 1);
                        player = i + 1;
                    }
                }
                switch (player)
                {
                    case 1:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_24_3);
                        break;
                    case 2:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_24_2);
                        break;
                    case 3:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_24_4);
                        break;
                    case 4:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_24_1);
                        break;

                }

            }
        }
    }
} 

