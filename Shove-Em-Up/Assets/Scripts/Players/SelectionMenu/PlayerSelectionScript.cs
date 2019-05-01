using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionScript : MonoBehaviour
{
    public PlayerData data;
    public TextMeshProUGUI nameSelect;
    public TextMeshProUGUI descriptionSelect;
    public GameObject mesh;

    private void Start()
    {
        Show();
    }

    public void Show()
    {
        nameSelect.text = data.name;
        descriptionSelect.text = data.description;
        mesh = Instantiate(data.geometry);
    }
}
