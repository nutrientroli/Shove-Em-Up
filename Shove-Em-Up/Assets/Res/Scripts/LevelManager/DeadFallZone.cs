using System.Collections.Generic;
using UnityEngine;

public class DeadFallZone : MonoBehaviour {

    [SerializeField] private List<LightCollorChangerController> lights = new List<LightCollorChangerController>();


    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            other.gameObject.GetComponent<PlayerScript>().KillerKillMe();
            PlayerData data = other.GetComponentInChildren<PlayerData>();
            if (data != null) {
                PlayersManager.GetInstance().Dead(data.GetPlayer());
                foreach (LightCollorChangerController _light in lights) _light.DeadPlayer();
            }
        }
    }
}
