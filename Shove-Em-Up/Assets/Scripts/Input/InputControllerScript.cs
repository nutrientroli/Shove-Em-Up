using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerScript : MonoBehaviour
{
    private CharacterControllerScript characterController;

    [SerializeField] private int numberOfPlayer = 1;
    [SerializeField] private string horizontalAxis = "Horizontal_P";
    [SerializeField] private string verticalAxis = "Vertical_P";
    [SerializeField] private string aButton = "A_P";
    [SerializeField] private string ltButton = "LT_P";

    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterControllerScript>();
        if (!characterController) Debug.LogError("Error. Input Controller without Character Controller.");
    }

    private void Update()
    {
        CheckMoveAxis();
        CheckButtons();
    }

    private void CheckMoveAxis()
    {
        float h = Input.GetAxis(horizontalAxis);
        float v = Input.GetAxis(verticalAxis);
        //Debug.Log(h + " : " + v);
        if(h !=0 || v!=0) characterController.Move(h, v);

    }

    private void CheckButtons()
    {
        if (Input.GetButtonDown(aButton)) characterController.ChargePush();
        if (Input.GetButtonUp(aButton)) characterController.Push();
    }
}
