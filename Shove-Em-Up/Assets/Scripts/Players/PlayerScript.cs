using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum State {MOVING, CHARGING, PUSHING, KNOCKBACK, HABILITY };
    public State currentState;
    private float currentTime = 0;
    private float timeToPush = 0.25f;
    private MoveScript moveScript;
    private PushScript pushScript;
    private KnockbackScript knockbackScript;
    public HabilityScript habilityScript;
    public CanvasPush canvasPlayer;

    //Gestion de modificadores
    private List<ModifierScript> listMods;

    private bool inverted = false;
    private bool isPushable = true;

    //Eliminar en un futuro
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Right = KeyCode.D;
    public KeyCode Left = KeyCode.A;
    public KeyCode PushCode = KeyCode.P;

    private void Start()
    {
        moveScript = GetComponent<MoveScript>();
        if (moveScript == null)
            moveScript = gameObject.AddComponent<MoveScript>();
        pushScript = GetComponent<PushScript>();
        if (pushScript == null)
            pushScript = gameObject.AddComponent<PushScript>();
        knockbackScript = GetComponent<KnockbackScript>();
        if (knockbackScript == null)
            knockbackScript = gameObject.AddComponent<KnockbackScript>();
        foreach(HabilityScript hab in GetComponents<HabilityScript>())
        {
            if (hab.enabled == true)
                habilityScript = hab;
        }
        if (habilityScript == null)
            habilityScript = gameObject.AddComponent<ShieldHabilityScript>();
        currentState = State.MOVING;
        listMods = new List<ModifierScript>();
    }

    private void Update()
    {
        //Modificadores Por habilidades
        CheckMods(Time.deltaTime);
        canvasPlayer.StatusConfused(inverted);

        switch (currentState)
        {
            case State.MOVING:
                break;
            case State.CHARGING:
                break;
            case State.PUSHING:
                pushScript.PushCharacter(Time.deltaTime);
                currentTime += Time.deltaTime;
                if (currentTime >= timeToPush)
                    ChangeState(State.MOVING);
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
        }

        ///*
        if (Input.GetKey(PushCode))
            Charge();                 //PARA PROBAR, QUITAR EN UN FUTURO
        if (Input.GetKeyUp(PushCode))
            Push();
        if (Input.GetKeyDown(KeyCode.K))
            Knockback();
        //*/
    }

    public void ChangeState(State _newState)
    {
        switch(currentState)
        {
            case State.MOVING:
                moveScript.CanMove(false);
                break;
            case State.CHARGING:
                moveScript.CanMove(false);
                moveScript.Charging(false);
                break;
            case State.PUSHING:
                currentTime = 0;
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
        }

        switch(_newState)
        {
            case State.MOVING:
                moveScript.CanMove(true);
                moveScript.Charging(false);
                break;
            case State.CHARGING:
                moveScript.CanMove(true);
                moveScript.Charging(true);
                break;
            case State.PUSHING:
                pushScript.Push();
                break;
            case State.KNOCKBACK:
                pushScript.RestartCharge();
                break;
            case State.HABILITY:
                break;
        }
        currentState = _newState;
    }

    public void Push()
    {
        if (pushScript.CanPush() && currentState == State.CHARGING && isPushable)
            ChangeState(State.PUSHING);
    }

    public void Charge()
    {
        if (pushScript.CanPush() && currentState != State.CHARGING && isPushable)
        {
            pushScript.ChargePush(Time.deltaTime);
            ChangeState(State.CHARGING);
        }
        
    }

    public void Movement(Vector3 _vector)
    {
        if (inverted)
            moveScript.AddVectorToMove(-_vector);
        else
            moveScript.AddVectorToMove(_vector);
    }

    public MoveScript GetMovement()
    {
        return moveScript;
    }

    public bool Knockback()
    {
        if (isPushable)
        {
            ChangeState(State.KNOCKBACK);
            return true;
        }
        else return false;
    }

    public void StopKnockback()
    {
        ChangeState(State.MOVING);
    }

    public void PushSomeoneOther()
    {
        habilityScript.IncrementPerImpact();
        print("veces entrado");
    }

    public void Hability()
    {
        /*if (pushScript.CanPush() && currentState == State.CHARGING)
            ChangeState(State.PUSHING);*/

        //Retocar!!
        if (habilityScript.CanUseHability())
        {
            habilityScript.UseHability();
            if(habilityScript.modToMe != null)
                listMods.Add(habilityScript.modToMe);
        }
    }

    public void AddOtherMod(ModifierScript _mod)
    {
        listMods.Add(_mod);
    }

    private void CheckMods(float _deltaTime) {
        List<ModifierScript> listRemoveMods = new List<ModifierScript>();
        foreach(ModifierScript mod in listMods) {
            if (mod.CheckModifier(_deltaTime))
            {
                listRemoveMods.Add(mod);
            }
        }
        foreach (ModifierScript mod in listRemoveMods) {
            listMods.Remove(mod);
            Destroy(mod);
        }
        Modification();
    }

    private void Modification()
    {
        ResetValues();
        foreach (ModifierScript mod in listMods) {
            if(!mod.isMovible)
                moveScript.isMovible = mod.isMovible;
            if(mod.inverted)
                inverted = mod.inverted;
            if (!mod.isPushable)
                isPushable = mod.isPushable;
        }
    }

    private void ResetValues()
    {
        inverted = false;
        isPushable = true;
        if(currentState == State.MOVING || currentState == State.CHARGING)
            moveScript.isMovible = true;
        //Para que cuando se acabe un modificador tenga los valores por defecto.
        //Hay que vigilar los estados actuales.
        //Los suyo seria que hubiera una variable de modificacion por cada variable real.
    }

}
