
using UnityEngine;
using UnityEngine.UI;

public class JoystickTesterScript : MonoBehaviour
{
    public Text joysticks;
    private float time = 0.6f;
    private float currentTime = 0.0f;

    void FixedUpdate() {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            currentTime = 0.0f;
            CheckJoysticks();
        }
    }

    private void CheckJoysticks()
    {
        int i = 0;
        string sticks = "Joysticks \n";
        foreach (string joyName in Input.GetJoystickNames())
        {
            sticks += i.ToString() + ":" + joyName + "\n";
            i++;
        }

        //sticks += "\nXbox_LeftHorizontal_P1: " + Input.GetAxis("Xbox_LeftHorizontal_P1");
        //sticks += "\nXbox_LeftVertical_P1: " + Input.GetAxis("Xbox_LeftVertical_P1");
        //sticks += "\nXbox_button_0_P1: " + Input.GetButton("Xbox_button_0_P1");
        //sticks += "\nXbox_button_1_P1: " + Input.GetButton("Xbox_button_1_P1");

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
