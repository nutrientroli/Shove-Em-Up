using System.Collections;

public static class SelectionSoundScript
{
    private static Hashtable soundPlayerTable = new Hashtable();

    public static void PlaySoundPlayerSelection(int _player, SoundManager.SoundEvent _event) {
        StopSoundPlayerSelection(_player);
        soundPlayerTable.Add(_player, SoundManager.GetInstance().PlaySoundAndGetSound(_event, null, true, _player.ToString()));
    } 

    public static void StopSoundPlayerSelection(int _player) {
        if (soundPlayerTable.ContainsKey(_player)) {
            SoundManager.GetInstance().ForceStopSound(((FMOD.Studio.EventInstance)soundPlayerTable[_player]));
            soundPlayerTable.Remove(_player);
        }
    }
}
