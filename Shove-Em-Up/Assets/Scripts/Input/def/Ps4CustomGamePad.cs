public class Ps4CustomGamePad : CustomGamePad {
    CustomGamePadConfiguration defaultConfig = new CustomGamePadConfiguration(
        "PS4_LeftHorizontal_P",
        "PS4_LeftVertical_P",
        "",
        "",
        "",
        "",
        "PS4_button_1_P",
        "PS4_button_2_P",
        "PS4_button_0_P",
        "",
        "",
        "",
        "", //Select
        "", //Start
        "",
        ""
    );

    public Ps4CustomGamePad(string _name, int _index, int _player) {
        name = _name;
        index = _index;
        type = TypeGamePad.XBOX_ONE;
        SetConfiguration(defaultConfig, _player);
    }
}
