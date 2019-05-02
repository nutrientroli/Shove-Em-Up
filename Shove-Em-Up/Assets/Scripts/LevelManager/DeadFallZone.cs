using UnityEngine;

public class DeadFallZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) LevelManager.GetInstance().players--;
        //Obtener Vidas del player
    }
}
