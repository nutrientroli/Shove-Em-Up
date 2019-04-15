using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    #region Singleton Pattern
    private static InputManager instance;
    private InputManager() { }
    public static InputManager GetInstance() {
        if (instance == null) instance = new InputManager();
        return instance;
    }
    #endregion

    #region Attributes
    private Hashtable listPlayersControllers = new Hashtable();
    #endregion

    #region Methods
    public void AddPlayer(int _player)
    {
        //Debug.Log("Hola " + _player);
        string[] joys = Input.GetJoystickNames();

        //Debug.Log("Hola " + _player + " " + joys.Length);
        if (joys.Length > 0 && _player <= joys.Length && _player > 0) {
            CustomGamePad gamepad;
            string name = joys[_player-1];
            if (IsXboxController(name)) gamepad = new XboxCustomGamePad(name, _player - 1, _player);
            else gamepad = new Ps4CustomGamePad(name, _player - 1, _player);
            listPlayersControllers.Add(_player - 1, gamepad);
        } else {
            Debug.LogWarning("Player : " + _player + " - No tiene controllador disponible");
        }
    }

    public void AddController(int _player, CustomGamePad _controller) {
        listPlayersControllers.Add(_player - 1, _controller);
    }

    public CustomGamePad GetController(int _player) {
        return (CustomGamePad)listPlayersControllers[_player - 1];
    }

    public void ShowPlayersControllers() {
        for(int i = 0; i<listPlayersControllers.Count; i++) {
            //for (int j = 0; j < ((ArrayList)listPlayersControllers[i]).Count; j++) {
                Debug.Log(i+1 + " :: " + listPlayersControllers[i]);
            //}
        }
    }

    public bool IsXboxController(string _name) {
        return _name.Contains("Xbox");
    }

    public bool CanCheckInputs(int _player)
    {
        return listPlayersControllers.ContainsKey(_player -1);
    }
    #endregion


}
