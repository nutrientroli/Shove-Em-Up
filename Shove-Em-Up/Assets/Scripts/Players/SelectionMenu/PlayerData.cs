using UnityEngine;

[CreateAssetMenu(fileName = "SelectablePlayer", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    public GameObject geometry;
    public new string name;
    public string description;
}
