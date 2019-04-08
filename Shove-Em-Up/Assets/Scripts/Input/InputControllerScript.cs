using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerScript : MonoBehaviour
{
    private CharacterControllerScript characterController;

    [SerializeField] private int numberOfPlayer = 1;
    [SerializeField] private string horizontalAxis;
    [SerializeField] private string verticalAxis;
    [SerializeField] private string rtButton;
    [SerializeField] private string ltButton;

    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterControllerScript>();
        if (!characterController) Debug.LogError("Error. Input Controller without Character Controller.");
    }

    private void CheckMoveAxis()
    {
        float h = Input.GetAxis(horizontalAxis);
        float v = Input.GetAxis(verticalAxis);
        if(h != 0 || v != 0) characterController.Move(h, v);
    }

    private void CheckButtons()
    {
        if (Input.GetButtonDown(rtButton)) characterController.ChargePush();
        if (Input.GetButtonUp(rtButton)) characterController.Push();
        if (Input.GetButton(ltButton)) characterController.Hability();
    }
}
