using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PodiumManager : MonoBehaviour
{
    public List<GameObject> playersPref;
    private List<GameObject> ourPlayers = new List<GameObject>();
    public List<Transform> positions;
    private List<float> scores = new List<float>();
    private List<int> players = new List<int>();
    public List<Color> colors = new List<Color>();

    public void OrderPlayers() {
        int maxPLayers = PlayersManager.GetInstance().GetNumberOfPlayers();
        List<float> playersScores = new List<float>();
        for (int i=0; i< maxPLayers; i++) playersScores.Add(ScoreManager.GetInstance().GetPoints(i + 1) + (0.01f * i));
        scores = playersScores.ToList<float>();
        scores.Sort();
        scores.Reverse();
        for(int i = 0; i < scores.Count; i++) {
            for (int j = 0; j < playersScores.Count; j++) {
                if (scores[i] == playersScores[j]) {
                    players.Add(j + 1);

                }
            }
        }
        CreatePlayers();
    }

    private void CreatePlayers() {
        for(int i = 0; i < players.Count; i++) {
            int character = 0;
            switch(PlayersManager.GetInstance().GetTableOfSelectPlayers((players[i])).name) {
                case "Gallina": character = 0; break;
                case "Abeja": character = 3; break;
                case "Toro": character = 1; break;
                case "Mono": character = 2; break;
                default: character = 0; break;
            }

            if(i == 0)
            {
                switch(players[i])
                {
                    case 1:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_26_3);
                        break;
                    case 2:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_26_2);
                        break;
                    case 3:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_26_4);
                        break;
                    case 4:
                        PresenterSound.PresenterTalks(SoundManager.SoundEvent.PRESENTADOR_26_1);
                        break;
                }
            }
             
            ourPlayers.Add(Instantiate(playersPref[character], positions[i].position, playersPref[character].transform.rotation));
            ourPlayers[ourPlayers.Count - 1].GetComponent<SelectionScript>().SetPlayerPodium(ourPlayers.Count - 1);
            ColorPlayer(players[i], ourPlayers[ourPlayers.Count - 1]);
        }
    }

    private void ColorPlayer(int _player, GameObject obj) {
        obj.GetComponentInChildren<Renderer>().material.color = colors[_player-1];
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) OrderPlayers();
    }
}
