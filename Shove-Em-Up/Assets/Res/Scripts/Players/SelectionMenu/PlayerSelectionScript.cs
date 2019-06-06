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
    [SerializeField] private List<Color> listColor = new List<Color>();

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
            CheckAxis(currentTime);
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
        obj.transform.position = obj.transform.position + new Vector3(0, 0, -0.7f);
        obj.GetComponentInChildren<Renderer>().material = listData[defaultData].material;
        obj.GetComponentInChildren<Renderer>().material.color = listColor[player - 1];
    }

    private void LeftSelection() {
        defaultData--;
        if (defaultData < 0) defaultData = listData.Count - 1;
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.CHANGECHARACTER_MENUSELECTION);
        Show();
    }

    private void RightSelection() {
        defaultData++;
        if (defaultData >= listData.Count) defaultData = 0;
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.CHANGECHARACTER_MENUSELECTION);
        Show();
    }

    private void Back() {
        if (readyPlayer) readyPlayer = false;
        else if (activePlayer) activePlayer = false;
        else ScenesManager.ChangeScene(ScenesManager.SceneCode.MENU);
        UpdateStates();
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.CHANGECHARACTER_MENUSELECTION);
    }

    private void Confirm() {
        if (activePlayer) Ready();
        else ActivePlayer();
        UpdateStates();
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.CHANGECHARACTER_MENUSELECTION);
    }

    private void Ready() {
        readyPlayer = true;
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.CHANGECHARACTER_MENUSELECTION);
        Material mat = listData[defaultData].material;
        mat.color = listColor[player - 1];
        listData[defaultData].material = mat;
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

    private void CheckAxis(float _dt) {
        if (InputManager.GetInstance().CanCheckInputs(player)) {
            float scroll = InputManager.GetInstance().GetController(player).GetAxis(InputManager.GetInstance().GetController(player).config.horizontalLeftAxis);
            if (scroll != 0) {
                if (currentTime >= waitTime) {
                    if (scroll > 0) RightSelection();
                    else LeftSelection();
                    currentTime = 0;
                }
            } else {
                currentTime = waitTime;
            }
        }
    }
    #endregion
}
