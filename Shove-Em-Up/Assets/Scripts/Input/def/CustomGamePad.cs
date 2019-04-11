using UnityEngine;

public struct CustomGamePadConfiguration
{
    public string horizontalLeftAxis;
    public string verticalLeftAxis;
    public string horizontalRightAxis;
    public string verticalRightAxis;
    public string triggerLeftAxis;
    public string triggerRightAxis;
    public string button_0;
    public string button_1;
    public string button_2;
    public string button_3;
    public string button_4;
    public string button_5;
    public string button_6;
    public string button_7;
    public string button_8;
    public string button_9;

    public CustomGamePadConfiguration(string _horizontalLeftAxis, string _verticalLeftAxis, 
        string _horizontalRightAxis, string _verticalRightAxis, string _triggerLeftAxis, 
        string _triggerRightAxis, string _button_0, string _button_1, string _button_2,
        string _button_3, string _button_4, string _button_5, string _button_6, string _button_7,
        string _button_8, string _button_9
        )
    {
        horizontalLeftAxis = _horizontalLeftAxis;
        verticalLeftAxis = _horizontalLeftAxis;
        horizontalRightAxis = _horizontalLeftAxis;
        verticalRightAxis = _horizontalLeftAxis;
        triggerLeftAxis = _horizontalLeftAxis;
        triggerRightAxis = _horizontalLeftAxis;
        button_0 = _horizontalLeftAxis;
        button_1 = _horizontalLeftAxis;
        button_2 = _horizontalLeftAxis;
        button_3 = _horizontalLeftAxis;
        button_4 = _horizontalLeftAxis;
        button_5 = _horizontalLeftAxis;
        button_6 = _horizontalLeftAxis;
        button_7 = _horizontalLeftAxis;
        button_8 = _horizontalLeftAxis;
        button_9 = _horizontalLeftAxis;
    }
}

public class CustomGamePad
{
    public string name;
    public int index;
    public enum TypeGamePad { XBOX_ONE, PS4 };
    public TypeGamePad type;
    public CustomGamePadConfiguration config;

    private bool GetButtonDown(string _button) {
        return Input.GetButtonDown(_button);
    }

    private bool GetButtonUp(string _button) {
        return Input.GetButtonUp(_button);
    }

    private float GetAxis(string _axis) {
        return Input.GetAxis(_axis);
    }

    public void SetConfiguration(CustomGamePadConfiguration _config, int _player)
    {
        config = _config;
        config.horizontalLeftAxis += _player;
        config.verticalLeftAxis += _player;
        config.horizontalRightAxis += _player;
        config.verticalRightAxis += _player;
        config.triggerLeftAxis += _player;
        config.triggerRightAxis += _player;
        config.button_0 += _player;
        config.button_1 += _player;
        config.button_2 += _player;
        config.button_3 += _player;
        config.button_4 += _player;
        config.button_5 += _player;
        config.button_6 += _player;
        config.button_7 += _player;
        config.button_8 += _player;
        config.button_9 += _player;

    }
}
