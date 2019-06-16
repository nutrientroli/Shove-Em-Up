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
    private float otherCurrentTime = 0;

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
        otherCurrentTime += Time.deltaTime;
        if (otherCurrentTime >= 10)
        {
            otherCurrentTime = 0;
            if (UnityEngine.Random.Range(0, 10) > 6)
            {
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
