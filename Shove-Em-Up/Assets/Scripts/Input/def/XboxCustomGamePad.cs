public class XboxCustomGamePad : CustomGamePad {
    CustomGamePadConfiguration defaultConfig = new CustomGamePadConfiguration(
        "Xbox_LeftHorizontal_P",
        "Xbox_LeftVertical_P",
        "",
        "",
        "",
        "",
        "Xbox_button_0_P",
        "Xbox_button_1_P",
        "Xbox_button_2_P",
        "",
        "",
        "",
        "", //Select
        "", //Start
        "",
        ""
    );

    public XboxCustomGamePad(string _name, int _index, int _player) {
        name = _name;
        index = _index;
        type = TypeGamePad.XBOX_ONE;
        SetConfiguration(defaultConfig, _player);
    }
}
