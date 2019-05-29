using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public enum SoundEvent {
        SOUND_1= 0,
        CHANGECHARACTER_MENUSELECTION= 1,
        PRESSREADY_MENUSELECTION = 2,
    }

    #region Singleton Pattern
    private static SoundManager instance;
    private SoundManager() {
        Init();
    }
    public static SoundManager GetInstance() {
        if (instance == null) instance = new SoundManager();
        return instance;
    }
    #endregion

    #region Variables
    private Hashtable tableOfSoundEvents = new Hashtable();
    
    #endregion

    #region Methods

    public void Init() {
        tableOfSoundEvents.Add(SoundEvent.SOUND_1, "event:/Character/GainPoint2");
        tableOfSoundEvents.Add(SoundEvent.CHANGECHARACTER_MENUSELECTION, "event:/Menu/ChangeCharacter");
        tableOfSoundEvents.Add(SoundEvent.PRESSREADY_MENUSELECTION, "event:/Menu/pressButton");

    }

    public void PlaySound(SoundEvent _event) {
        FMODUnity.RuntimeManager.PlayOneShot(tableOfSoundEvents[_event].ToString());
    }

    public void StopSound(SoundEvent _event)
    {
        //llamar a evento del fmod
    }

    public bool IsPlaySound(SoundEvent _event)
    {
        //llamar a evento del fmod
        return true;
    }

    #endregion

}
