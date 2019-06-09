using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionIntegratedScript : MonoBehaviour
{
    #region Attributes
    [Header("Player Configuration")]
    [SerializeField] private int player;
    [SerializeField] private int defaultData;
    [SerializeField] private Transform defaultTransform;
    [SerializeField] private List<PlayerSelectData> listData = new List<PlayerSelectData>();
    [SerializeField] private List<Color> listColor = new List<Color>();

    [Header("VISUALIZATION - TEST")]
    [SerializeField] private bool activeMenu = true;
    [SerializeField] private bool activePlayer = false;
    [SerializeField] private bool readyPlayer = false;
    private GameObject mesh;
    private GameObject obj;
    [SerializeField] private Light focus;
    private float currentTime = 0;
    private float waitTime = 0.5f;
    #endregion

    #region MonoBehaviour Methods
    private void Start() {
        Show();
        InputManager.GetInstance().AddPlayer(player);
    }

    private void Update() {
        if (!activeMenu) {
            CheckButtons();
            currentTime += Time.deltaTime;
            if (activePlayer && !readyPlayer) CheckAxis(currentTime);
        } else {
            activePlayer = false;
            readyPlayer = false;
        }
    }
    #endregion

    #region Input Methods
    private void CheckButtons()
    {
        if (InputManager.GetInstance().CanCheckInputs(player))
        {
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

    #region Selection Methods
    public void Show() {
        mesh = listData[defaultData].geometry;
        if (obj != null) Destroy(obj);
        obj = Instantiate(mesh, transform, false);
        obj.transform.SetPositionAndRotation(defaultTransform.position, defaultTransform.rotation);
        obj.GetComponentInChildren<Renderer>().material = listData[defaultData].material;
        obj.GetComponentInChildren<Renderer>().material.color = listColor[player - 1];
        focus = obj.GetComponent<PlayerHelperSelectionScript>().focus;
        SetLight();
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
        if (!activeMenu) {
            if (readyPlayer) readyPlayer = false;
            else if (activePlayer) activePlayer = false;
            else  transform.parent.parent.gameObject.GetComponent<Animation>().Play("SelectionToMenu");
            UpdateStates();
            SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.CHANGECHARACTER_MENUSELECTION);
        }
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
        SetLight();
    }

    public void SetLight() {
        if (readyPlayer) {
            focus.color = listColor[player - 1];
            focus.intensity = 10;
        } else if (activePlayer) {
            focus.color = Color.white;
            focus.intensity = 10;
        } else {
            focus.color = Color.white;
            focus.intensity = 1;
        }
    }

    public bool GetReady() {
        return readyPlayer;
    }
    #endregion
}
