
using UnityEngine;
using UnityEngine.UI;

public class JoystickTesterScript : MonoBehaviour
{
    public Text joysticks;

    void Start() {
        int i = 0;
        string sticks = "Joysticks \n";
        foreach (string joyName in Input.GetJoystickNames()) {
            sticks += i.ToString() + ":" + joyName + "\n";
            i++;
        }
        joysticks.text = sticks;
    }


    //Xbox bluetooth controller
    //Buttons Mapping
    //A -> joystick button 0
    //B -> joystick button 1
    //X -> joystick button 2
    //Y -> joystick button 3
    //LB -> joystick button 4
    //RB -> joystick button 5
    //Select -> joystick button 6
    //Start -> joystick button 7
    //Left Stick Push -> joystick button 8
    //Right Stick Push -> joystick button 9

    //Axis Mapping
    //RT -> 3rd axis : 1
    //LT -> 3rd axis : -1
}
