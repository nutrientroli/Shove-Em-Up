public class XboxCustomGamePad : CustomGamePad
{
    CustomGamePadConfiguration defaultConfig = new CustomGamePadConfiguration(
        //Todos los axis 
    );

    public XboxCustomGamePad(string _name, int _index, int _player) {
        name = _name;
        index = _index;
        type = TypeGamePad.XBOX_ONE;
        SetConfiguration(defaultConfig, _player);
    }
}
