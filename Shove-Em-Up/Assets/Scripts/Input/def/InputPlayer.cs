using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private int player;
    [SerializeField] private PlayerScript playerScript;

   

    private void Start()
    {
        InputManager.GetInstance().AddPlayer(player);
    }



    void Update()
    {
        //InputManager.GetInstance().ShowPlayersControllers();
        CheckMoveAxis();
        CheckButtons();
    }

    private void CheckMoveAxis() {

        float h = InputManager.GetInstance().GetController(player).GetAxis(InputManager.GetInstance().GetController(player).config.horizontalLeftAxis);
        float v = InputManager.GetInstance().GetController(player).GetAxis(InputManager.GetInstance().GetController(player).config.verticalLeftAxis);
        //Debug.Log(new Vector3(h, 0, v));
        Vector3 move = new Vector3(h, 0, v);
        move.Normalize();
        playerScript.Movement(move);
    }

    private void CheckButtons() {
        if (InputManager.GetInstance().GetController(player).GetButtonDown(InputManager.GetInstance().GetController(player).config.button_A)) playerScript.Charge();
        if (InputManager.GetInstance().GetController(player).GetButtonUp(InputManager.GetInstance().GetController(player).config.button_A)) playerScript.Push();
    }

    private float CheckSensibility(float _value, float _sensibility = 0.0f) {
        if (_value >= _sensibility || _value <= (-_sensibility)) return _value;
        return 0.0f;
    }




}
