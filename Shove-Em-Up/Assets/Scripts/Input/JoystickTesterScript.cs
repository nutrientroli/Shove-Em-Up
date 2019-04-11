
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
        joysticks.text = sticks;
    }


    /*private void JoysticksDebug()
    {
        float _bigCircleRadius = 0.12f;
        float _stikRepresentationRadius = 0.01f;

        if (gameCamera != null)
        {
            // Left joy Representation ----------------------------------------------------------------------------------------- //
            // joy area
            Vector3 _desiredPositionLeft = gameCamera.ViewportToWorldPoint(new Vector3(0.3f, 0.25f, gameCamera.nearClipPlane));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_desiredPositionLeft, _bigCircleRadius);

            // Point on area
            Vector2 _leftAxis = movementInput * _bigCircleRadius;
            Vector3 _leftJoyRelativePosition = new Vector2(_desiredPositionLeft.x + _leftAxis.x,
                                                           _desiredPositionLeft.y + _leftAxis.y);
            _leftJoyRelativePosition.z = gameCamera.transform.position.z + 1f;                      // nos colocamos por delante de la camra

            Gizmos.DrawSphere(_leftJoyRelativePosition, _stikRepresentationRadius);


            // Right joy Representation ---------------------------------------------------------------------------------------- //
            // joy area
            Vector3 _desiredPositionRight = gameCamera.ViewportToWorldPoint(new Vector3(0.7f, 0.25f, gameCamera.nearClipPlane));
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_desiredPositionRight, _bigCircleRadius);

            // Point on area
            Vector2 _rightAxis = aimInput * _bigCircleRadius;
            Vector3 _rightRelativePosition = new Vector2(_desiredPositionRight.x + _rightAxis.x,
                                                         _desiredPositionRight.y + _rightAxis.y);
            _rightRelativePosition.z = gameCamera.transform.position.z + 1f;                        // nos colocamos por delante de la camra

            Gizmos.DrawSphere(_rightRelativePosition, _stikRepresentationRadius);

        }*/
    //}



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
