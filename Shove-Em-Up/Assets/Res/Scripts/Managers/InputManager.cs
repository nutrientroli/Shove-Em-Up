using System.Collections;
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
    public void AddPlayer(int _player) {
        if (!CanCheckInputs(_player)) {
            string[] joys = Input.GetJoystickNames();
            if (joys.Length > 0 && _player <= joys.Length && _player > 0) {
                //Segundo parametro "_player-1" porque no hacemos control de si hay problemas 
                //con los mandos. En futuras versiones, se puede controlar facilmente. 
                //Requeriria hacer gestion de player-index una hashtable por ejemplo...
                AddController(_player, _player - 1, joys);
            } else {
                Debug.LogWarning("Player : " + _player + " - No tiene controllador disponible");
            }
        }
    }

    public void AddGamepad(int _index, CustomGamePad _gamepad) {
        listPlayersControllers.Add(_index, _gamepad);
    }

    public void AddController(int _player, int _index, string[] joys) {
        CustomGamePad gamepad;
        string name = joys[_index];
        if (IsXboxController(name)) gamepad = new XboxCustomGamePad(name, _index, _player);
        else gamepad = new Ps4CustomGamePad(name, _index, _player);
        AddGamepad(_index, gamepad);
    }

    public CustomGamePad GetController(int _player) {
        return (CustomGamePad)listPlayersControllers[_player - 1];
    }

    public void ShowPlayersControllers() {
        for(int i = 0; i<listPlayersControllers.Count; i++) {
            Debug.Log(i+1 + " :: " + listPlayersControllers[i]);
        }
    }

    public bool IsXboxController(string _name) {
        return _name.Contains("Xbox");
    }

    public bool CanCheckInputs(int _player) {
        return listPlayersControllers.ContainsKey(_player -1);
    }
    #endregion

}
