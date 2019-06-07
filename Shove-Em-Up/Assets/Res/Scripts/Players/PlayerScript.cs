using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    public enum State {MOVING, CHARGING, PUSHING, KNOCKBACK, HABILITY };
    public State currentState;
    private float currentTime = 0;
    private float timeToPush = 0.25f;
    private MoveScript moveScript;
    private PushScript pushScript;
    private KnockbackScript knockbackScript;
    public HabilityScript habilityScript;
    public CanvasPush canvasPlayer;
    public ParticleSystem particlesDash;
    public ParticleSystem particlesConfusion;
    public ParticleSystem particlesStun;
    public ParticleSystem particlePowerUp;
    public ParticleSystem particleSlow;

    private CapsuleCollider capsuleCollider;
    private float radiusCapsule;

    //Gestion de modificadores
    private List<ModifierScript> listMods;

    private bool inverted = false;
    private bool isPushable = true;
    private bool isKnockable = true;
    private bool ralenticed = false;

    private PlayerData selfData;
    private PlayerData killer = null;
    private float timeToKillMe = 0;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        radiusCapsule = capsuleCollider.radius;
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
        selfData = gameObject.GetComponent<PlayerData>();
    }

    private void Start()
    {
        switch(selfData.GetPlayer())
        {
            case 1:
                particlesDash.startColor = Color.red;
                break;
            case 2:
                particlesDash.startColor = Color.blue;
                break;
            case 3:
                particlesDash.startColor = Color.green;
                break;
            case 4:
                particlesDash.startColor = Color.yellow;
                break;
        }
    }

    private void Update()
    {
        //Modificadores Por habilidades
        CheckMods(Time.deltaTime);
        if (inverted && !particlesConfusion.isPlaying)
        {
            particlesConfusion.gameObject.SetActive(true);
            particlesConfusion.Play();
        }
        if (!inverted && particlesConfusion.isPlaying)
        {
            particlesConfusion.gameObject.SetActive(false);
            particlesConfusion.Stop();
        }
        if (!GetIsMovible() && !isPushable && !particlesStun.isPlaying && habilityScript.GetCurrentEnergy() != 0)
        {
            particlesStun.gameObject.SetActive(true);
            particlesStun.Play();
        }
        if (GetIsMovible() && isPushable && particlesStun.isPlaying)
        {
            particlesStun.gameObject.SetActive(false);
            particlesStun.Stop();
        }
        if(ralenticed && isKnockable && !particleSlow.isPlaying)
        {
            particleSlow.gameObject.SetActive(true);
            particleSlow.Play();
        }
        if(!ralenticed && particleSlow.isPlaying)
        {
            particleSlow.gameObject.SetActive(false);
            particleSlow.Stop();
        }
    

        if(killer != null)
        {
            timeToKillMe += Time.deltaTime;
            if (timeToKillMe >= 1.5f)
            {
                killer = null;
                timeToKillMe = 0;
            }
        }

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
                capsuleCollider.radius = radiusCapsule;
                particlesDash.Stop();
                currentTime = 0;
                animator.SetTrigger("FinnishDash");
                break;
            case State.KNOCKBACK:
                animator.SetTrigger("Restore");
                break;
            case State.HABILITY:
                break;
        }

        switch(_newState)
        {
            case State.MOVING:
                capsuleCollider.radius = radiusCapsule;
                moveScript.CanMove(true);
                moveScript.Charging(false);
                break;
            case State.CHARGING:
                moveScript.CanMove(true);
                moveScript.Charging(true);
                break;
            case State.PUSHING:
                animator.SetTrigger("Dash");
                capsuleCollider.radius = radiusCapsule * 2f;
                particlesDash.gameObject.transform.position = capsuleCollider.transform.position - transform.forward * capsuleCollider.radius;
                particlesDash.Play();
                pushScript.Push();
                break;
            case State.KNOCKBACK:
                moveScript.CanMove(false);
                animator.SetTrigger("Impact");
                pushScript.RestartCharge();
                break;
            case State.HABILITY:
                break;
        }
        currentState = _newState;
    }

    public void Push() {
        if (pushScript.CanPush() && currentState == State.CHARGING && isPushable)
            ChangeState(State.PUSHING);
    }

    public void Fall() {
        animator.SetBool("Fall", true);
    }
    public void StopFall() {
        animator.SetBool("Fall", false);
    }

    public void Charge()
    {
        if (pushScript.CanPush() && currentState != State.CHARGING && isPushable && currentState != State.KNOCKBACK)
        {
            pushScript.ChargePush(Time.deltaTime);
            ChangeState(State.CHARGING);
        }
        else if(!isPushable)
        {
            pushScript.RestartCharge();
        }
        
    }

    public void Movement(Vector3 _vector, bool _air = false)
    {
        _vector = _vector.normalized;
        if (!_air)
        {
            if (inverted)
                moveScript.AddVectorToMove(-_vector);
            else
                moveScript.AddVectorToMove(_vector);
        }
        else
        {
            moveScript.AddVectorToMove(_vector, _air);
        }
        
        if (_vector.Equals(Vector3.zero)) animator.SetFloat("Speed", 0);
        else if(moveScript.CheckCharging()) animator.SetFloat("Speed", 0.5f);
        else animator.SetFloat("Speed", 1);

        if(!isPushable && !GetIsMovible())
            animator.SetFloat("Speed", 0);
    }

    public MoveScript GetMovement()
    {
        return moveScript;
    }

    public bool Knockback()
    {
        if (isKnockable)
        {
            ChangeState(State.KNOCKBACK);
            return isKnockable;
        }
        else return isKnockable;
    }

    public void StopKnockback()
    {
        ChangeState(State.MOVING);
    }

    public void PushSomeoneOther()
    {
        habilityScript.IncrementPerImpact();

    }

    public void Hability()
    {
        /*if (pushScript.CanPush() && currentState == State.CHARGING)
            ChangeState(State.PUSHING);*/

        //Retocar!!
        if (habilityScript.CanUseHability() && currentState == State.MOVING)
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

    public void RemoveMod(ModifierScript _mod)
    {
        List<ModifierScript> listRemoveMods = new List<ModifierScript>();
        foreach (ModifierScript mod in listMods)
        {
            if (_mod == mod)
            {
                listRemoveMods.Add(mod);
            }
        }
        foreach (ModifierScript mod in listRemoveMods)
        {
            listMods.Remove(mod);
            Destroy(mod);
        }
    }

    public float GetRealCapsuleRadius()
    {
        return radiusCapsule;
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
            if (!mod.isKnockable)
                isKnockable = mod.isKnockable;
            if (mod.honeyRalenticed)
                ralenticed = mod.honeyRalenticed;
        }
    }

    private void ResetValues()
    {
        inverted = false;
        isPushable = true;
        isKnockable = true;
        ralenticed = false;
        if(currentState == State.MOVING || currentState == State.CHARGING)
            moveScript.isMovible = true;
        //Para que cuando se acabe un modificador tenga los valores por defecto.
        //Hay que vigilar los estados actuales.
        //Los suyo seria que hubiera una variable de modificacion por cada variable real.
    }

    public bool GetKnockable()
    {
        return isKnockable;
    }

    public bool GetIsMovible()
    {
        return moveScript.isMovible;
    }

    public bool GetPushable()
    {
        return isPushable;
    }

    public bool GetRalenticed()
    {
        return ralenticed;
    }

    public void SetPlayerPushed(GameObject _otherPlayer)
    {
        _otherPlayer.GetComponent<PlayerScript>().SetKiller(selfData);
    }

    public void SetKiller(PlayerData _killer)
    {
        killer = _killer;
        timeToKillMe = 0;
    }

    public void KillerKillMe()
    {
        if (killer != null)
        {
            killer.AddScore(3);
            killer = null;
            timeToKillMe = 0;
        }
        else
            AddScore(-1);
    }

    public void AddScore(int _score = 0)
    {
        selfData.AddScore(_score);
    }

    public void StartItemParticles()
    {
        particlePowerUp.Play();
    }
}
