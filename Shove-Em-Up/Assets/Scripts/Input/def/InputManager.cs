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
    private ArrayList listPlayersControllers = new ArrayList();
    #endregion

    #region Methods
    private void AddPlayer(int _player, ArrayList _listControllers = null)
    {
        if (_listControllers == null) _listControllers = new ArrayList();
        listPlayersControllers.Insert(_player, _listControllers);
    }

    private void AddController(int _player, CustomGamePad _controller)
    {
        ((ArrayList)listPlayersControllers[_player-1]).Add(_controller);
    }


    #endregion


}
