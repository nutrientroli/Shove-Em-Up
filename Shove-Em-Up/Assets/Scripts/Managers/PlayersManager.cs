using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager {

    //TO DO
    //Asignar color en base al número de jugador.
    //Asignar prefab.



    #region Singleton Pattern
    private static PlayersManager instance;
    private PlayersManager() { }
    public static PlayersManager GetInstance() {
        if (instance == null) instance = new PlayersManager();
        return instance;
    }
    #endregion

    private Hashtable tableOfPlayerData = new Hashtable();
    private Hashtable tableOfCharacters = new Hashtable();
    private Hashtable tableOfSelectPlayers = new Hashtable();

    #region Selectable Methods
    public void AddPlayerSelect(int _player, PlayerSelectData _selection) {
        if (!tableOfSelectPlayers.ContainsKey(_player)) tableOfSelectPlayers.Add(_player, _selection);  
        else tableOfSelectPlayers[_player] = _selection;
    }
    #endregion

    #region InGame Methods
    public void SetListOfCharacters(List<GameObject> _listOfCharacters) {
        foreach(GameObject _obj in _listOfCharacters) {
            if (!tableOfCharacters.ContainsKey(_obj.name)) tableOfCharacters.Add(_obj.name, _obj);
        }
    }

    public void Init() {
        for (int i=1; i<=tableOfSelectPlayers.Count; i++) {
            PlayerSelectData selectData = (PlayerSelectData)tableOfSelectPlayers[i];
            GameObject prefab = ((GameObject)tableOfCharacters[selectData.name]);
            GameObject obj = GameObject.Instantiate(prefab);
            PlayerData objData = obj.GetComponentInChildren<PlayerData>();
            objData = ResetValuesPlayerData(i, objData, selectData);
            tableOfPlayerData.Add(i, objData);
            Respawn(i);
        }
    }

    public void Dead(int _player) {
        PlayerData data = (PlayerData)tableOfPlayerData[_player];
        data.SetLives(data.GetLives() - 1);
        if (data.GetLives() > 0) Respawn(_player);
        else LevelManager.GetInstance().players--;
        Debug.Log("Dead "+ _player + data.GetLives());
    }

    public void Respawn(int _player) {
        Debug.Log("Respawn " + _player);
        PlayerData data = ((PlayerData)tableOfPlayerData[_player]);
        data.transform.parent.position = new Vector3(0, 50, 0);
        //data.transform.position = new Vector3(0, 0, 0);
    }

    private PlayerData ResetValuesPlayerData(int _player, PlayerData _data, PlayerSelectData _select) {
        _data.SetPlayer(_player);
        _data.SetLives(3);
        _data.SetTypePlayer(_select.type);
        _data.GetComponent<Renderer>().material = _select.material;
        return _data;
    }
    #endregion
}
