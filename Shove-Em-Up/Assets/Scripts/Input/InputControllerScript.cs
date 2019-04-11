using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerScript : MonoBehaviour
{
    private CharacterControllerScript characterController;

    [SerializeField] private int numberOfPlayer = 1;
    private string horizontalAxis = "Horizontal_P";
    private string verticalAxis = "Vertical_P";
    private string aButton = "A_P";

    private void Awake() {
        characterController = gameObject.GetComponent<CharacterControllerScript>();
        if (!characterController) Debug.LogError("Error. Input Controller without Character Controller.");
    }

    private void Start() {
        horizontalAxis += numberOfPlayer;
        verticalAxis += numberOfPlayer;
        aButton += numberOfPlayer;
    }

    private void Update() {
        CheckMoveAxis();
        CheckButtons();
    }

    private void CheckMoveAxis() {
        float h = CheckSensibility(Input.GetAxis(horizontalAxis));
        float v = CheckSensibility(Input.GetAxis(verticalAxis));
        //Debug.Log("InputController: " + h + ", "+ v);
        characterController.Move(h, v * -1);
    }

    private void CheckButtons() {
        if (Input.GetButtonDown(aButton)) characterController.ChargePush();
        if (Input.GetButtonUp(aButton)) characterController.Push();
    }

    private float CheckSensibility(float _value, float _sensibility = 0.0f)
    {
        if (_value >= _sensibility || _value <= (-_sensibility)) return _value;
        return 0.0f;
    }
}
