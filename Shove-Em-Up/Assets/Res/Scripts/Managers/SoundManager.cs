using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundEvent {
        DISQUALIFIED_ALARM = 0,
        FSS_SOUND = 1,
        GAIN_POINT = 2,
        KNOCKBACK_SOUND = 3,
        LOSE_POINT = 4,
        CROWD_CLAP = 5,
        CROWD_BOOING = 6,
        CROWD_FLIPANDING = 7,
        CROWD_LAUGH = 8,
        DISQUALIFIED_ALARM_2 = 9,
        RULETA_1 = 10,
        RULETA_2 = 11,
        RULETA_3 = 12,
        RULETA_4 = 13,
        EVENT_FAN = 14,
        EVENT_FALLBALLS = 15,
        PAUSE_GAME = 16,
        EVENT_SMOKE = 17,
        EVENT_FALLFLOOR_1 = 18,
        EVENT_FALLFLOOR_2 = 19,
        UNPAUSE_GAME = 20,
        EVENT_WIND_1 = 21,
        EVENT_WIND_2 = 22,
        EVENT_WIND_3_SCATT = 23,
        EVENT_COUNTDOWN = 24,
        STARTS = 25,
        PRESENTADOR_1 = 26,
        PRESENTADOR_2 = 27,
        PRESENTADOR_3 = 28,
        PRESENTADOR_4 = 29,
        PRESENTADOR_5 = 30,
        PRESENTADOR_6 = 31,
        PRESENTADOR_7 = 32,
        PRESENTADOR_8 = 33,
        PRESENTADOR_9 = 34,
        PRESENTADOR_10 = 35,
        PRESENTADOR_11 = 36,
        PRESENTADOR_12 = 37,
        PRESENTADOR_13 = 38,
        PRESENTADOR_13_2 = 39,
        PRESENTADOR_14 = 40,
        PRESENTADOR_15 = 41,
        PRESENTADOR_16 = 42,
        PRESENTADOR_17 = 43,
        PRESENTADOR_18 = 44,
        PRESENTADOR_19 = 45,
        PRESENTADOR_20 = 46,
        PRESENTADOR_21 = 47,
        PRESENTADOR_22 = 48,
        PRESENTADOR_23 = 49,
        PRESENTADOR_24_1 = 50,
        PRESENTADOR_24_2 = 51,
        PRESENTADOR_24_3 = 52,
        PRESENTADOR_24_4 = 53,
        PRESENTADOR_25 = 54,
        PRESENTADOR_26_1 = 55,
        PRESENTADOR_26_2 = 56,
        PRESENTADOR_26_3 = 57,
        PRESENTADOR_26_4 = 58,
        PRESENTADOR_27 = 59,
        PRESENTADOR_28 = 60,
        PRESENTADOR_29 = 61,
        PRESENTADOR_30 = 62,
        PRESENTADOR_31 = 63,
        PRESENTADOR_32 = 64,
        PRESENTADOR_33 = 65,
        MENU_BEE_PICK = 66,
        MENU_BULL_PICK = 67,
        MENU_CHICKEN_PICK = 68,
        MENU_MONKEY_PICK = 69,
        MENU_CHANGE_CHARACTER = 70,
        MENU_PRESS_BUTTON = 71,
        MUSIC_INGAME = 72,
        MUSIC_INIT = 73,
        NONE = 74
    }
    public enum SoundEventType
    {
        ONE = 0,
        MULTI = 1,
        SCRATT = 2
    }
    #region Singleton Pattern
    private static SoundManager instance;
    private SoundManager() { Init(); }
    public static SoundManager GetInstance() {
        if (instance == null)
        {
            instance = new SoundManager();
        }
        return instance;
    }
    #endregion

    #region Variables
    private Hashtable tableOfSoundEvents = new Hashtable();
    private Hashtable tableOfSoundEventsPlaying = new Hashtable();
    private Hashtable tableOfSoundEventsPlayingToDestroyOnTime = new Hashtable();
    #endregion

    #region Methods

    public void Init() {
        tableOfSoundEvents.Add(SoundEvent.DISQUALIFIED_ALARM, "event:/Character/DisqualifiedAlarm");
        tableOfSoundEvents.Add(SoundEvent.FSS_SOUND, "event:/Character/FssSound");
        tableOfSoundEvents.Add(SoundEvent.GAIN_POINT, "event:/Character/GainPoint");
        tableOfSoundEvents.Add(SoundEvent.KNOCKBACK_SOUND, "event:/Character/KnockBackSound");
        tableOfSoundEvents.Add(SoundEvent.LOSE_POINT, "event:/Character/LosePoint");
        tableOfSoundEvents.Add(SoundEvent.CROWD_CLAP, "event:/Entorno/CroudClap");
        tableOfSoundEvents.Add(SoundEvent.CROWD_BOOING, "event:/Entorno/CrowdBooing");
        tableOfSoundEvents.Add(SoundEvent.CROWD_FLIPANDING, "event:/Entorno/CrowdFlipanding");
        tableOfSoundEvents.Add(SoundEvent.CROWD_LAUGH, "event:/Entorno/CrowdLaugh");
        tableOfSoundEvents.Add(SoundEvent.DISQUALIFIED_ALARM_2, "event:/Entorno/DisqualifiedAlarm");
        tableOfSoundEvents.Add(SoundEvent.RULETA_1, "event:/Entorno/Ruleta/Ruleta1mate");
        tableOfSoundEvents.Add(SoundEvent.RULETA_2, "event:/Entorno/Ruleta/Ruleta2metalico");
        tableOfSoundEvents.Add(SoundEvent.RULETA_3, "event:/Entorno/Ruleta/Tin-tin-tiin-tiiiiiiiinSound");
        tableOfSoundEvents.Add(SoundEvent.RULETA_4, "event:/Entorno/Ruleta/TURUTURUTURUUUUSound");
        tableOfSoundEvents.Add(SoundEvent.EVENT_FAN, "event:/Evento/Fan");
        tableOfSoundEvents.Add(SoundEvent.EVENT_FALLBALLS, "event:/Evento/FloorImpact");
        tableOfSoundEvents.Add(SoundEvent.PAUSE_GAME, "event:/Evento/PauseGame");
        tableOfSoundEvents.Add(SoundEvent.EVENT_SMOKE, "event:/Evento/SmokeStartSound");
        tableOfSoundEvents.Add(SoundEvent.EVENT_FALLFLOOR_1, "event:/Evento/TrapFalling");
        tableOfSoundEvents.Add(SoundEvent.EVENT_FALLFLOOR_2, "event:/Evento/TrapParriba");
        tableOfSoundEvents.Add(SoundEvent.UNPAUSE_GAME, "event:/Evento/UnPauseGame");
        tableOfSoundEvents.Add(SoundEvent.EVENT_WIND_1, "event:/Evento/WindFinaldelLoop");
        tableOfSoundEvents.Add(SoundEvent.EVENT_WIND_2, "event:/Evento/WindLoop");
        tableOfSoundEvents.Add(SoundEvent.EVENT_WIND_3_SCATT, "event:/Evento/WindScattered");
        tableOfSoundEvents.Add(SoundEvent.EVENT_COUNTDOWN, "event:/Extra/Countdown");
        tableOfSoundEvents.Add(SoundEvent.STARTS, "event:/Extra/Estrellitas");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_1, "event:/Extra/Presentador/Presentador001");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_2, "event:/Extra/Presentador/Presentador002");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_3, "event:/Extra/Presentador/Presentador003");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_4, "event:/Extra/Presentador/Presentador004");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_5, "event:/Extra/Presentador/Presentador005");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_6, "event:/Extra/Presentador/Presentador006");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_7, "event:/Extra/Presentador/Presentador007");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_8, "event:/Extra/Presentador/Presentador008");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_9, "event:/Extra/Presentador/Presentador009");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_10, "event:/Extra/Presentador/Presentador010");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_11, "event:/Extra/Presentador/Presentador011");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_12, "event:/Extra/Presentador/Presentador012");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_13, "event:/Extra/Presentador/Presentador013");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_13_2, "event:/Extra/Presentador/Presentador013.2");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_14, "event:/Extra/Presentador/Presentador014");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_15, "event:/Extra/Presentador/Presentador015");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_16, "event:/Extra/Presentador/Presentador016");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_17, "event:/Extra/Presentador/Presentador017");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_18, "event:/Extra/Presentador/Presentador018");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_19, "event:/Extra/Presentador/Presentador019");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_20, "event:/Extra/Presentador/Presentador020");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_21, "event:/Extra/Presentador/Presentador021");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_22, "event:/Extra/Presentador/Presentador022");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_23, "event:/Extra/Presentador/Presentador023");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_24_1, "event:/Extra/Presentador/Presentador024amarillo");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_24_2, "event:/Extra/Presentador/Presentador024azul");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_24_3, "event:/Extra/Presentador/Presentador024rojo");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_24_4, "event:/Extra/Presentador/Presentador024verde");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_25, "event:/Extra/Presentador/Presentador025");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_26_1, "event:/Extra/Presentador/Presentador026amarillo");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_26_2, "event:/Extra/Presentador/Presentador026azul");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_26_3, "event:/Extra/Presentador/Presentador026rojo");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_26_4, "event:/Extra/Presentador/Presentador026verde");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_27, "event:/Extra/Presentador/Presentador027");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_28, "event:/Extra/Presentador/Presentador028");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_29, "event:/Extra/Presentador/Presentador029");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_30, "event:/Extra/Presentador/Presentador030");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_31, "event:/Extra/Presentador/Presentador031");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_32, "event:/Extra/Presentador/Presentador032");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_33, "event:/Extra/Presentador/Presentador033");
        tableOfSoundEvents.Add(SoundEvent.MENU_BEE_PICK, "event:/Menu/BeePick");
        tableOfSoundEvents.Add(SoundEvent.MENU_BULL_PICK, "event:/Menu/BullPick");
        tableOfSoundEvents.Add(SoundEvent.MENU_CHANGE_CHARACTER, "event:/Menu/ChangeCharacter");
        tableOfSoundEvents.Add(SoundEvent.MENU_CHICKEN_PICK, "event:/Menu/ChickenPeak");
        tableOfSoundEvents.Add(SoundEvent.MENU_MONKEY_PICK, "event:/Menu/MonkeyPick");
        tableOfSoundEvents.Add(SoundEvent.MENU_PRESS_BUTTON, "event:/Menu/PressButton");
        tableOfSoundEvents.Add(SoundEvent.MUSIC_INGAME, "event:/Music/Ingame");
        tableOfSoundEvents.Add(SoundEvent.MUSIC_INIT, "event:/Music/Inicio");
    }

    public void PlaySound(SoundEvent _event, Transform _transform = null, bool _destroyTime = false, string _tag = "", SoundEventType _type = SoundEventType.ONE, float _variationVolume = 1) {
        if (!IsPlaySound(_event, _tag)) {
            FMOD.Studio.EventInstance _audio = FMODUnity.RuntimeManager.CreateInstance(tableOfSoundEvents[_event].ToString());
            if(_transform != null) _audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_transform));
            else _audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            switch (_type) {
                case SoundEventType.ONE:
                case SoundEventType.MULTI:
                    if (_destroyTime) tableOfSoundEventsPlayingToDestroyOnTime.Add(CreateTagSound(_event, _tag), _audio);
                    break;
                case SoundEventType.SCRATT:
                    tableOfSoundEventsPlaying.Add(_event, _audio);
                    break;
            }
            _audio.start();
        }
    }

    //Force method
    public FMOD.Studio.EventInstance PlaySoundAndGetSound(SoundEvent _event, Transform _transform = null, bool _destroyTime = false, string _tag = "")
    {
        FMOD.Studio.EventInstance _audio = FMODUnity.RuntimeManager.CreateInstance(tableOfSoundEvents[_event].ToString());
        if (_transform != null) _audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_transform));
        else _audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
        _audio.start();
        return _audio;
    }

    public void StopSound(SoundEvent _event) {
        if (IsPlaySound(_event)) {
            FMOD.Studio.EventInstance _audio = (FMOD.Studio.EventInstance)tableOfSoundEventsPlaying[_event];
            _audio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public bool IsPlaySound(SoundEvent _event, string _tag = "") {
        string _key = CreateTagSound(_event, _tag);
        return tableOfSoundEventsPlaying.ContainsKey(_key) || tableOfSoundEventsPlayingToDestroyOnTime.ContainsKey(_key);
    }

    public void AdjustVolume(SoundEvent _event, FMOD.Studio.EventInstance _audio, float _variation) {
        switch (_event) {
            case SoundEvent.MUSIC_INIT: _audio.setVolume(0.5f * _variation); break;
            default: break;
        }
    }

    public void StopSoundOnTime(SoundEvent _event, string _tag="") {
        string _key = CreateTagSound(_event, _tag);
        if (tableOfSoundEventsPlayingToDestroyOnTime.Count > 0 && tableOfSoundEventsPlayingToDestroyOnTime.Contains(_key)) {
           ((FMOD.Studio.EventInstance)tableOfSoundEventsPlayingToDestroyOnTime[_key]).stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            tableOfSoundEventsPlayingToDestroyOnTime.Remove(_key);
        }
    }

    public void ForceStopSound(FMOD.Studio.EventInstance _event)
    {
        _event.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private string CreateTagSound(SoundEvent _event, string _tag) {
        return _tag + _event;
    }
    #endregion

}
