using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    public enum State { MOVING, CHARGING, PUSHING, KNOCKBACK, HABILITY };
    public State currentState;
    private bool inverted = false;
    private bool isPushable = true;

    //+ Gestion de modificaciones
}
