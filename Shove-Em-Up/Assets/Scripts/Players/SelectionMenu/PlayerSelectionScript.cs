using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSelectionScript : MonoBehaviour
{
    #region Attributes
    [Header("Player Configuration")]
    [SerializeField] private int player;
    [SerializeField] private int defaultData;
    [SerializeField] private List<PlayerSelectData> listData = new List<PlayerSelectData>();
    private bool activePlayer = false;
    private bool readyPlayer = false;
    [SerializeField] private List<Material> listMaterial = new List<Material>();

    [Header("Panel Configuration")]
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject disabledPanel;
    [SerializeField] private TextMeshProUGUI nameSelect;
    [SerializeField] private TextMeshProUGUI descriptionSelect;
    [SerializeField] private GameObject mesh;
    private GameObject obj;
    private Vector3 scaleMesh = new Vector3(150, 150, 150);
    private Vector3 rotationMesh = new Vector3(-90, 180, 0);

    private float currentTime = 0;
    private float waitTime = 0.5f;
    #endregion

    #region MonoBehaviour Methods
    private void Start() {
        Show();
        InputManager.GetInstance().AddPlayer(player);
    }

    private void Update() {
        CheckButtons();
        currentTime += Time.deltaTime;
        if (activePlayer && !readyPlayer) {
            if (currentTime >= waitTime) {
                CheckAxis();
                currentTime = 0;
            }
        }
    }
    #endregion

    #region Selection Methods
    public void Show() {
        nameSelect.text = listData[defaultData].name;
        descriptionSelect.text = listData[defaultData].description;
        mesh = listData[defaultData].geometry;
        if (obj != null) Destroy(obj);
        obj = Instantiate(mesh, transform, false);
        obj.transform.localScale = scaleMesh;
        obj.transform.localRotation = Quaternion.Euler(rotationMesh);
        obj.GetComponentInChildren<Renderer>().material = listMaterial[player - 1];
    }

    private void LeftSelection() {
        defaultData--;
        if (defaultData < 0) defaultData = listData.Count - 1;
        Show();
    }

    private void RightSelection() {
        defaultData++;
        if (defaultData >= listData.Count) defaultData = 0;
        Show();
    }

    private void Back() {
        if (readyPlayer) readyPlayer = false;
        else if (activePlayer) activePlayer = false;
        else ScenesManager.ChangeScene(ScenesManager.SceneCode.MENU);
        UpdateStates();
    }

    private void Confirm() {
        if (activePlayer) Ready();
        else ActivePlayer();
        UpdateStates();
    }

    private void Ready() {
        readyPlayer = true;
        listData[defaultData].material = listMaterial[player - 1];
        PlayersManager.GetInstance().AddPlayerSelect(player, PlayerSelectData.CreateInstance(listData[defaultData]));
    }

    private void ActivePlayer() {
        activePlayer = true;
    }

    private void UpdateStates() {
        if (readyPlayer) readyPanel.SetActive(true);
        else if (activePlayer) {
            readyPanel.SetActive(false);
            disabledPanel.SetActive(false);
        } else {
            readyPanel.SetActive(false);
            disabledPanel.SetActive(true);
        }
    }

    public bool GetReady() {
        return readyPlayer;
    }
    #endregion

    #region Input Methods
    private void CheckButtons() {
        if (InputManager.GetInstance().CanCheckInputs(player)) {
            if (InputManager.GetInstance().GetController(player).GetButtonDown(InputManager.GetInstance().GetController(player).config.button_A)) Confirm();
            if (InputManager.GetInstance().GetController(player).GetButtonDown(InputManager.GetInstance().GetController(player).config.button_B)) Back();
        }
    }

    private void CheckAxis() {
        if (InputManager.GetInstance().CanCheckInputs(player)) {
            float scroll = InputManager.GetInstance().GetController(player).GetAxis(InputManager.GetInstance().GetController(player).config.horizontalLeftAxis);
            if (scroll != 0) {
                if (scroll > 0) RightSelection();
                else LeftSelection();
            }
        }
    }
    #endregion
}
