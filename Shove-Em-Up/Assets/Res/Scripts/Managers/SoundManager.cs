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
        CHANGECHARACTER_MENUSELECTION= 1,
        PRESSREADY_MENUSELECTION = 2,
        DASH = 3,
        KNOCKBACK = 4,
        PRESENTADOR_1 = 5,
        PRESENTADOR_2 = 6,
        PRESENTADOR_3 = 7,
        PRESENTADOR_4 = 8,
        PRESENTADOR_5 = 9,
        PRESENTADOR_6 = 10,
        PRESENTADOR_7 = 11,
        PRESENTADOR_8 = 12,
        PRESENTADOR_9 = 13,
        PRESENTADOR_10 = 14,
        ESCENA_1 = 15
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
    
    #endregion

    #region Methods

    public void Init() {
        tableOfSoundEvents.Add(SoundEvent.CHANGECHARACTER_MENUSELECTION, "event:/Menu/ChangeCharacter");
        tableOfSoundEvents.Add(SoundEvent.PRESSREADY_MENUSELECTION, "event:/Menu/pressButton");
        tableOfSoundEvents.Add(SoundEvent.DASH, "event:/Character/FssSound1");
        tableOfSoundEvents.Add(SoundEvent.KNOCKBACK, "event:/Character/KnockBackSound2");
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_1, "event:/Extra/frase5"); //otro universo
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_2, "event:/Extra/frase7"); //desalluno
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_3, "event:/Extra/frase8"); //Se palpa tension
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_4, "event:/Extra/frase6"); //Apuestas (Ruleta)
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_5, "event:/Extra/frase9"); //Ruleta
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_6, "event:/Extra/frase4"); //mis ojos!
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_7, "event:/Extra/frase3"); //Claro Ganador
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_8, "event:/Extra/frase2"); //Ataca Matraca
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_9, "event:/Extra/frase1"); //Uy Uy Uy
        tableOfSoundEvents.Add(SoundEvent.PRESENTADOR_10, "event:/Extra/frase10"); //MAreando
        tableOfSoundEvents.Add(SoundEvent.ESCENA_1, "event:/Enviroment/CrowdBooing2");
    }

    public void PlaySound(SoundEvent _event) {
        //llamar a evento del fmod
        FMODUnity.RuntimeManager.PlayOneShot("event:/Character/KnockBackSound");
    }

    public void StopSound(SoundEvent _event)
    {
        //llamar a evento del fmod
    }

    public bool IsPlaySound(SoundEvent _event)
    {
        
        return true;
    }

    #endregion

}
