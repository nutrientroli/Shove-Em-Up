using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager {

    #region Singleton Pattern
    private static PlayersManager instance;
    private PlayersManager() { }
    public static PlayersManager GetInstance() {
        if (instance == null) instance = new PlayersManager();
        return instance;
    }
    #endregion

    private List<PlayerData> listPlayers = new List<PlayerData>();
    private Hashtable tableSelectPlayers = new Hashtable();

    public void Dead(int _player) {
        //eliminar vida jugador
        //si tiene 0 o menos que no respawnee y eliminamos un player del LevelManager.
    }

    public void Respawn(int _player) {
        //Teleport a posicion.
    }

    public void Init() {
        //recorrer lista de players asignando vidas, energy a 0, ...
        //LevelManager -> Init
    }

    public void AddPlayer(PlayerData _player) {
        if(!listPlayers.Contains(_player)) listPlayers.Add(_player);
    }

    public void AddPlayerSelect(int _player, PlayerSelectData _selection) {
        if (!tableSelectPlayers.ContainsKey(_player)) tableSelectPlayers.Add(_player, _selection);
        else tableSelectPlayers[_player] = _selection;
    }
}
