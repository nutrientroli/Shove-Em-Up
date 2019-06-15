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
    private float speedNoSelect = 0;

    private float currentSoundTime = 0;
    [SerializeField] private float maxSoundTime = 2;
    private bool soundEmit = false;
    #endregion

    #region MonoBehaviour Methods
    private void Start() {
        speedNoSelect = Random.Range(0.25f, 0.55f);
        Show();
        InputManager.GetInstance().AddPlayer(player);
    }

    private void Update() {
        if (!activeMenu) {
            CheckButtons();
            currentTime += Time.deltaTime;
            if (activePlayer && !readyPlayer) CheckAxis(currentTime);
            if (soundEmit) {
                currentSoundTime += Time.deltaTime;
                if (currentSoundTime >= maxSoundTime) {
                    currentSoundTime = 0;
                    MuteSound();
                }
            }
        } else {
            activePlayer = false;
            readyPlayer = false;
            UpdateStates();
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
        obj.GetComponent<SelectionScript>().SpeedAnimation(speedNoSelect);
        focus = obj.GetComponent<PlayerHelperSelectionScript>().focus;
        SetLight();
    }

    private void LeftSelection() {
        MuteSound();
        defaultData--;
        if (defaultData < 0) defaultData = listData.Count - 1;
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
        Show();
    }

    private void RightSelection() {
        MuteSound();
        defaultData++;
        if (defaultData >= listData.Count) defaultData = 0;
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
        Show();
    }

    private void Back() {
        if (!activeMenu) {
            if (readyPlayer)
            {
                obj.GetComponent<SelectionScript>().SpeedAnimation(speedNoSelect);
                readyPlayer = false;
            }
            else if (activePlayer)
            {
                obj.GetComponent<SelectionScript>().SpeedAnimation(speedNoSelect);
                activePlayer = false;
            }
            else transform.parent.parent.gameObject.GetComponent<Animation>().Play("SelectionToMenu");
            UpdateStates();
            SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
        }
    }

    private void Confirm() {
        if (activePlayer) Ready();
        else ActivePlayer();
        UpdateStates();
        SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER);
    }

    private void Ready() {
        obj.GetComponent<SelectionScript>().SetReady();
        obj.GetComponent<SelectionScript>().SpeedAnimation(1f);
        readyPlayer = true;
        readyPlayer = true;
        PlaySound();
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

    private void PlaySound()
    {
        switch (listData[defaultData].name) {
            case "Abeja": SelectionSoundScript.PlaySoundPlayerSelection(player, SoundManager.SoundEvent.MENU_BEE_PICK); break;
            case "Toro": SelectionSoundScript.PlaySoundPlayerSelection(player, SoundManager.SoundEvent.MENU_BULL_PICK); break;
            case "Gallina": SelectionSoundScript.PlaySoundPlayerSelection(player, SoundManager.SoundEvent.MENU_CHICKEN_PICK); break;
            case "Mono": SelectionSoundScript.PlaySoundPlayerSelection(player, SoundManager.SoundEvent.MENU_MONKEY_PICK); break;
            default: SoundManager.GetInstance().PlaySound(SoundManager.SoundEvent.MENU_CHANGE_CHARACTER); break;
        }
        soundEmit = true;
    }

    private void MuteSound() {
        switch (listData[defaultData].name) {
            case "Abeja": SelectionSoundScript.StopSoundPlayerSelection(player); break;
            case "Toro": SelectionSoundScript.StopSoundPlayerSelection(player); break;
            case "Gallina": SelectionSoundScript.StopSoundPlayerSelection(player); break;
            case "Mono": SelectionSoundScript.StopSoundPlayerSelection(player); break;
            default: break;
        }
        soundEmit = false;
    }
    #endregion
}
