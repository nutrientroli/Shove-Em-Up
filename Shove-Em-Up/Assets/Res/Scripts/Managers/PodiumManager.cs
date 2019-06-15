using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumManager : MonoBehaviour
{
    private List<PlayerData> playersData = new List<PlayerData>();
    public List<GameObject> playersPref;
    private List<GameObject> ourPlayers = new List<GameObject>();
    public List<Transform> positions;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OrderPlayers()
    {
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < _players.Length; i++)
        {
            playersData.Add(_players[i].GetComponent<PlayerData>());
        }

        bool done = false;
        int maxNum = -100;
        List<PlayerData> _playersPD = new List<PlayerData>();
        PlayerData _player = new PlayerData();
        int iterations = 0;
        while(!done && iterations <= 100)
        {
            iterations++;
            for (int i = 0; i < playersData.Count; i++)
            {
                for (int j = 0; j < _playersPD.Count; j++)
                {
                    if (maxNum < playersData[i].GetScore() && playersData[i].GetPlayer() != _playersPD[j].GetPlayer())
                    {
                        _player = playersData[i];
                        maxNum = playersData[i].GetScore();
                    }
                }
            }
            _playersPD.Add(_player);
            maxNum = -100;
            if (_playersPD.Count == playersData.Count)
                done = true;
        }
        for (int i = 0; i < _playersPD.Count; i++)
            playersData[i] = _playersPD[i];

        CreatePlayers();
        ColorPlayer();
    }

    private void CreatePlayers()
    {
        for(int i = 0; i < playersData.Count; i++)
        {
            int character = 0;
            print(playersData[i].GetPlayer());
            /*print(PlayersManager.GetInstance().GetTableOfSelectPlayers(playersData[i].GetPlayer()).name);
            switch(PlayersManager.GetInstance().GetTableOfSelectPlayers(playersData[i].GetPlayer()).name)
            {
                case "Gallina":
                    character = 0;
                    break;
                case "Abeja":
                    character = 3;
                    break;
                case "Toro":
                    character = 1;
                    break;
                case "Mono":
                    character = 2;
                    break;
            }
            ourPlayers.Add(Instantiate(playersPref[character], positions[i].position, playersPref[character].transform.rotation));
        */
    }
    }

    private void ColorPlayer()
    {
        for(int i = 0; i < ourPlayers.Count; i++)
        {
            ourPlayers[i].GetComponentInChildren<Renderer>().material.color = PlayersManager.GetInstance().GetTableOfSelectPlayers(playersData[i].GetPlayer()).material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OrderPlayers();
    }
}
