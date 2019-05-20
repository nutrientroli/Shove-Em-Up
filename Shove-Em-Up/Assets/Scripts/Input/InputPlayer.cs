using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private int player;
    [SerializeField] private PlayerScript playerScript;
    

   

    private void Start()
    {
        player = GetComponent<PlayerData>().GetPlayer();
        InputManager.GetInstance().AddPlayer(player);
    }



    void Update()
    {
        //InputManager.GetInstance().ShowPlayersControllers();
        CheckMoveAxis();
        CheckButtons();
    }

    private void CheckMoveAxis() {

        if (InputManager.GetInstance().CanCheckInputs(player))
        {
            float h = InputManager.GetInstance().GetController(player).GetAxis(InputManager.GetInstance().GetController(player).config.horizontalLeftAxis);
            float v = InputManager.GetInstance().GetController(player).GetAxis(InputManager.GetInstance().GetController(player).config.verticalLeftAxis);
            playerScript.Movement(new Vector3(h, 0, v));
        }
    }

    private void CheckButtons()
    {
        if (InputManager.GetInstance().CanCheckInputs(player))
        {
            if (InputManager.GetInstance().GetController(player).GetButton(InputManager.GetInstance().GetController(player).config.button_A)) playerScript.Charge();
            if (InputManager.GetInstance().GetController(player).GetButtonUp(InputManager.GetInstance().GetController(player).config.button_A)) playerScript.Push();
            if (InputManager.GetInstance().GetController(player).GetButtonDown(InputManager.GetInstance().GetController(player).config.button_B)) playerScript.Hability();
        }
    }
}
