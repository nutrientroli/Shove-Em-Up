using UnityEngine;

public class DeadFallZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            other.gameObject.GetComponent<PlayerScript>().KillerKillMe();
            PlayerData data = other.GetComponentInChildren<PlayerData>();
            if (data != null)
            {
                data.light.RedLight();
                PlayersManager.GetInstance().Dead(data.GetPlayer());
            }
        }
    }
}
