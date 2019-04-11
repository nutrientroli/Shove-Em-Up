public class Ps4CustomGamePad : CustomGamePad
{
    CustomGamePadConfiguration defaultConfig = new CustomGamePadConfiguration(
        //Todos los axis
    );

    public Ps4CustomGamePad(string _name, int _index)
    {
        name = _name;
        index = _index;
        type = TypeGamePad.XBOX_ONE;
        SetConfiguration(defaultConfig, _index);


    }
}
