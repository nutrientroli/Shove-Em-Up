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
    public List<int> listOfPlayersToRespawnFinnishEvent = new List<int>();
    private int limitPlayerDeathInEvent = 1;
    private Transform respawnPoint;

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
            GameObject prefab = ((GameObject)tableOfCharacters[selectData.nameObject]);
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
            PlayerScript playerSript = null;
            if (data != null) {
                moveScript = data.gameObject.GetComponent<MoveScript>();
                playerSript = data.gameObject.GetComponent<PlayerScript>();
                data.gameObject.GetComponent<HabilityScript>().RestartHability();
                data.light.DefaultLight();
            }
            if (moveScript != null) {
                Vector3 vec = respawnPoint.position;
                switch (_player) {
                    case 1: vec = new Vector3(vec.x + 5, vec.y + 50, vec.z + 5); break;
                    case 2: vec = new Vector3(vec.x - 5, vec.y + 50, vec.z + 5); break;
                    case 3: vec = new Vector3(vec.x + 5, vec.y + 50, vec.z - 5); break;
                    case 4: vec = new Vector3(vec.x - 5, vec.y + 50, vec.z - 5); break;
                    default: vec = new Vector3(vec.x + 0, vec.y + 50, vec.z + 0); break;
                }
                moveScript.RestartPosition(vec);
            }
            if (playerSript != null) playerSript.Fall();
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
            switch(j + 4 - listOfPlayersToRespawnFinnishEvent.Count)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    data = ((PlayerData)tableOfPlayerData[listOfPlayersToRespawnFinnishEvent[j]]);
                    data.gameObject.GetComponent<PlayerScript>().AddScore(1);
                    break;
                case 3:
                    data = ((PlayerData)tableOfPlayerData[listOfPlayersToRespawnFinnishEvent[j]]);
                    data.gameObject.GetComponent<PlayerScript>().AddScore(3);
                    break;
                case 4:
                    data = ((PlayerData)tableOfPlayerData[listOfPlayersToRespawnFinnishEvent[j]]);
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

        switch(_player) {
            case 1:
                _data.GetComponentInChildren<Renderer>().materials[0].color = Color.red;
                break;
            case 2:
                _data.GetComponentInChildren<Renderer>().materials[0].color = Color.blue;
                break;
            case 3:
                _data.GetComponentInChildren<Renderer>().materials[0].color = Color.green;
                break;
            case 4:
                _data.GetComponentInChildren<Renderer>().materials[0].color = Color.yellow;
                break;
        }
        return _data;
    }
    public int GetNumberOfPlayers()
    {
        return tableOfPlayerData.Count;
    }

    public void SetRespawnPoint(Transform initPosToRespawn)
    {
        respawnPoint = initPosToRespawn;
    }
    #endregion
}
