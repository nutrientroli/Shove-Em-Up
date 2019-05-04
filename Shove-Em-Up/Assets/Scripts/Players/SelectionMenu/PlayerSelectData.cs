using UnityEngine;

[CreateAssetMenu(fileName = "SelectablePlayer", menuName = "Player")]
public class PlayerSelectData : ScriptableObject {
    public GameObject geometry;
    public new string name;
    public string description;
    public PlayerType type;
    public Material material;
}
