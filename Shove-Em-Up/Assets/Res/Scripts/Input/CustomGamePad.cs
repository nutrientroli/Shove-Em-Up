using UnityEngine;

public struct CustomGamePadConfiguration
{
    public string horizontalLeftAxis;
    public string verticalLeftAxis;
    public string horizontalRightAxis;
    public string verticalRightAxis;
    public string triggerLeftAxis;
    public string triggerRightAxis;
    public string button_A;
    public string button_B;
    public string button_X;
    public string button_Y;
    public string button_LB;
    public string button_RB;
    public string button_Select;
    public string button_Start;
    public string button_LeftStickPush;
    public string button_RightStickPush;

    public CustomGamePadConfiguration(string _horizontalLeftAxis, string _verticalLeftAxis, 
        string _horizontalRightAxis, string _verticalRightAxis, string _triggerLeftAxis, 
        string _triggerRightAxis, string _button_a, string _button_b, string _button_x,
        string _button_y, string _button_lb, string _button_rb, string _button_select, string _button_start,
        string _button_leftStick, string _button_rightStick
        )
    {
        horizontalLeftAxis = _horizontalLeftAxis;
        verticalLeftAxis = _verticalLeftAxis;
        horizontalRightAxis = _horizontalRightAxis;
        verticalRightAxis = _verticalRightAxis;
        triggerLeftAxis = _triggerLeftAxis;
        triggerRightAxis = _triggerRightAxis;
        button_A = _button_a;
        button_B = _button_b;
        button_X = _button_x;
        button_Y = _button_y;
        button_LB = _button_lb;
        button_RB = _button_rb;
        button_Select = _button_select;
        button_Start = _button_start;
        button_LeftStickPush = _button_leftStick;
        button_RightStickPush = _button_rightStick;
    }
}

public class CustomGamePad
{
    public string name;
    public int index;
    public enum TypeGamePad { XBOX_ONE, PS4 };
    public TypeGamePad type;
    public CustomGamePadConfiguration config;

    public bool GetButtonDown(string _button) {
        //Debug.Log(index+1 +" Press Button " + Input.GetButtonDown(_button));
        return Input.GetButtonDown(_button);
    }

    public bool GetButton(string _button)
    {
        return Input.GetButton(_button);
    }

    public bool GetButtonUp(string _button) {
        return Input.GetButtonUp(_button);
    }

    public float GetAxis(string _axis) {
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
        config.button_A += _player;
        config.button_B += _player;
        config.button_X += _player;
        config.button_Y += _player;
        config.button_LB += _player;
        config.button_RB += _player;
        config.button_Select += _player;
        config.button_Start += _player;
        config.button_LeftStickPush += _player;
        config.button_RightStickPush += _player;
    }

    public bool IsXboxController(string _name) {
        return _name.Contains("Xbox");
    }
}
