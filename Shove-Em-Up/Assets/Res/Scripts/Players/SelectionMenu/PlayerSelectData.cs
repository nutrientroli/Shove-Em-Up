using UnityEngine;

[CreateAssetMenu(fileName = "SelectablePlayer", menuName = "Player")]
public class PlayerSelectData : ScriptableObject {
    public GameObject geometry;
    public new string name;
    public string nameObject;
    public string description;
    public PlayerType type;
    public Material material;

    public void Init(GameObject _geometry, string _name, string _description, PlayerType _type, Material _material, string _nameObject)
    {
        this.geometry = _geometry;
        this.name = _name;
        this.description = _description;
        this.type = _type;
        this.material = _material;
        this.nameObject = _nameObject;
    }

    public static PlayerSelectData CreateInstance(GameObject _geometry, string _name, string _description, PlayerType _type, Material _material, string _nameObject)
    {
        var data = ScriptableObject.CreateInstance<PlayerSelectData>();
        data.Init(_geometry, _name, _description, _type, _material, _nameObject);
        return data;
    }
    public static PlayerSelectData CreateInstance(PlayerSelectData _data)
    {
        var data = ScriptableObject.CreateInstance<PlayerSelectData>();
        data.Init(_data.geometry, _data.name, _data.description, _data.type, _data.material, _data.nameObject);
        return data;
    }
}
