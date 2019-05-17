using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SoundManager
 * La idea es tener una hastable con los eventos del sonido.
 * Con un enum, se marca la realación, ya que son las keys.
 * Faltara mirar de integrar fmod
 * 
 * 
 */


public class SoundManager
{
    public enum SoundEvent {
        SOUND_1= 0,
        SOUND_2= 1
    }

    #region Singleton Pattern
    private static SoundManager instance;
    private SoundManager() { }
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
        //Setear tabla.
    }

    public void PlaySound(SoundEvent _event) {
        //llamar a evento del fmod
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
