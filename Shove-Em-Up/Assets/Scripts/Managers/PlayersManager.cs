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
    private List<int> listOfPlayersToRespawnFinnishEvent = new List<int>();
    private int limitPlayerDeathInEvent = 1;

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
        limitPlayerDeathInEvent = 0;
        tableOfPlayerData.Clear();
        for (int i=1; i<=tableOfSelectPlayers.Count; i++) {
            PlayerSelectData selectData = (PlayerSelectData)tableOfSelectPlayers[i];
            GameObject prefab = ((GameObject)tableOfCharacters[selectData.name]);
            GameObject obj = GameObject.Instantiate(prefab);
            PlayerData objData = obj.GetComponentInChildren<PlayerData>();
            objData = ResetValuesPlayerData(i, objData, selectData);
            tableOfPlayerData.Add(i, objData);
            Respawn(i, false);
            limitPlayerDeathInEvent++;
        }
        limitPlayerDeathInEvent--;
    }

    public void Dead(int _player) {
        Respawn(_player, LevelManager.GetInstance().GetEventState());
    }

    public void Respawn(int _player, bool _eventActive) {
        if (_eventActive) {
            listOfPlayersToRespawnFinnishEvent.Add(_player);
        } else {
            PlayerData data = ((PlayerData)tableOfPlayerData[_player]);
            MoveScript moveScript = null;
            if (data != null) moveScript = data.gameObject.GetComponent<MoveScript>();
            if (moveScript != null) moveScript.RestartPosition();
        }
    }

    public void RespawnFinnishEvent() {
        bool die = false;
        PlayerData data;
        for (int i = 0; i < 4; i++)
        {
            die = false;
            for (int j = 0; j < listOfPlayersToRespawnFinnishEvent.Count; j++)
            {
                if (i+1 == listOfPlayersToRespawnFinnishEvent[j])
                {
                    die = true;
                    break;
                }
            }
            if(((PlayerData)tableOfPlayerData[i+1]) != null && !die)
            {
                data = ((PlayerData)tableOfPlayerData[i + 1]);
                data.gameObject.GetComponent<PlayerScript>().AddScore(5);
            }
        }
        for (int j = 0; j < listOfPlayersToRespawnFinnishEvent.Count; j++)
        {
            switch(j)
            {
                case 0:
                    break;
                case 1:
                    data = ((PlayerData)tableOfPlayerData[j + 1]);
                    data.gameObject.GetComponent<PlayerScript>().AddScore(1);
                    break;
                case 3:
                    data = ((PlayerData)tableOfPlayerData[j + 1]);
                    data.gameObject.GetComponent<PlayerScript>().AddScore(3);
                    break;
                case 4:
                    data = ((PlayerData)tableOfPlayerData[j + 1]);
                    data.gameObject.GetComponent<PlayerScript>().AddScore(5);
                    break;
            }
        }

        foreach (int player in listOfPlayersToRespawnFinnishEvent) {
            Respawn(player, false);
        }
        listOfPlayersToRespawnFinnishEvent.Clear();
    }

    public bool CheckLimitPlayersDeathInEvent() {
        return (listOfPlayersToRespawnFinnishEvent.Count >= limitPlayerDeathInEvent);
    }

    private PlayerData ResetValuesPlayerData(int _player, PlayerData _data, PlayerSelectData _select) {
        _data.SetPlayer(_player);
        _data.SetTypePlayer(_select.type);
        _data.GetComponentInChildren<Renderer>().material = _select.material;
        return _data;
    }
    #endregion
}
