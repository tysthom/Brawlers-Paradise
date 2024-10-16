using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flinch : MonoBehaviour
{
    public Animator anim;
    GameObject gameManager;
    GameObject combatManagear;
    IdManagear idManagerInstance;
    public GameObject divePosition;
    public GameObject bearhuggedPosition;
    public GameObject fightStyleManager;
    public GameObject hudManager;
    public GameObject surrenderCanvas;
    FightingSoundEffects fSEInstance;

    public bool isReacting;
    public bool isFlinching; //True if flinching (moving backwards & not animation flinching)
    public bool isFlinchBuffering; //Prevents player from attacking while also allowing them to move around
    public bool isDove; //True if dove on floor
    public bool isRecovering; //True if recovering after being knocked down
    public bool isBearhugged; //True if being Bear Hugged
    public bool isBlockedBack; //True if moving backwards after blocking an attack
    public bool isKnockedBack; //True if currently being knockedback
    public bool isKnockedDown; //True if knocked on the floor
    public bool isStunned; //True if stunned
    public bool isParried; //True if parried
    public bool isParryBuffering; //Prevents player from attacking while also allowing them to move around
    public bool isPoisoned; //True if poisoned
    public bool isSurrendering; //True if surrendering
    public bool isBeingFinished; //True if about to be finished

    public bool canBeCounterHit;
    public bool canBePunishHit;

    [Header("Coroutine")]
    public Coroutine reactionTime;
    public Coroutine blockedBack;
    public Coroutine recovery;
    public Coroutine stun;
    public Coroutine dove;
    public Coroutine groundFlinch;
    public Coroutine bearHugged;
    public Coroutine parried;
    public Coroutine surrender;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager");
        combatManagear = GameObject.Find("Combat Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        fightStyleManager = GameObject.Find("Fight Style Manager");
        hudManager = GameObject.Find("HUD Manager");
        hudManager.GetComponent<HUDManager>().finisherPrompt = GameObject.Find("Finisher Prompt");
        fSEInstance = GetComponent<FightingSoundEffects>();
    }

    private void Start()
    {
        hudManager.GetComponent<HUDManager>().finisherText.text = "";
        hudManager.GetComponent<HUDManager>().finisherPrompt.SetActive(false);
    }

    private void Update()
    {
        if (isFlinching || isFlinchBuffering || isBlockedBack || isParried || isParryBuffering || isStunned 
            || isKnockedBack || isKnockedDown || isDove || isRecovering || isBearhugged || isSurrendering)
        {
            isReacting = true;
            anim.speed = 1;
            if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; }
        }
        else
        {
            isReacting = false;
        }

        if((isFlinching || isBlockedBack || isKnockedBack) && tag == "Player") { GetComponent<CharacterController>().enabled = false; }

        if((isFlinching || isParried) && !GetComponent<Dodge>().isColliding) { transform.position -= transform.forward * combatManagear.GetComponent<CombatStats>().flinchDistance * Time.deltaTime; }

        if(isBlockedBack)
        {
            transform.position -= transform.forward * combatManagear.GetComponent<CombatStats>().flinchDistance * 2 * Time.deltaTime;
        }

        if (isKnockedBack && !GetComponent<Dodge>().isColliding) 
        {
            transform.position -= transform.forward * combatManagear.GetComponent<CombatStats>().knockbackDistance * Time.deltaTime;
        }

        if(isStunned && anim.GetInteger("State") != 105)
        {
            anim.SetInteger("State", 105);
            anim.SetBool("canTransition", true);
        }

        if (isBearhugged)
        {
            transform.position = GetComponent<Combat>().enemy.GetComponent<Flinch>().bearhuggedPosition.transform.position;
        }

        if (tag == "Enemy")
        {
            if (isReacting)
            {
                GetComponent<AiBehavior>().isChargingEnemy = false;
                GetComponent<AiBehavior>().isChasingEnemy = false;
                GetComponent<AiBehavior>().canGlideToEnemy = false;
            }

            if (isSurrendering && GetComponent<Combat>().enemy.GetComponent<Combat>().canFinish && !GetComponent<Combat>().enemy.GetComponent<Combat>().isFinishing)
            {
                hudManager.GetComponent<HUDManager>().finisherPrompt.SetActive(true);
            }
            else
            {
                hudManager.GetComponent<HUDManager>().finisherPrompt.SetActive(false);
            }
        }
    }

    public void ReactionInitiation(int state, float damage) //Causes gameobject to flinch 
    {
        if (tag == "Player") { GetComponent<CharacterController>().enabled = false; }
        if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; GetComponent<AiBehavior>().isLookingForThrowable = false; }

        GetComponent<Combat>().canAttack = false;
        GetComponent<Combat>().canNextAttack = false;
        GetComponent<Combat>().isAttacking = false;
        GetComponent<Combat>().isGuardBreaking = false;
        GetComponent<Combat>().isParrying = false;
        GetComponent<Combat>().isParryBuffering = false;
        GetComponent<Combat>().isDiving = false;
        GetComponent<Combat>().isGroundAttacking = false;
        GetComponent<Combat>().isGroundIdle = false;
        GetComponent<Throw>().isEquipped = false;

        isParried = false;
        isStunned = false;

        GetComponent<CoroutineManager>().CancelCoroutines(reactionTime);
        if (tag == "Enemy")
        {
            if (GetComponent<AiBehavior>().waitToAttack != null)
            {
                StopCoroutine(GetComponent<AiBehavior>().waitToAttack);
                GetComponent<AiBehavior>().wTACalled = false;
            }
        }
        GetComponent<Combat>().ShouldNotMove();
        GetComponent<Combat>().ShouldNotDive();
        anim.SetBool("canTransition", true);

            if (GetComponent<Combat>().parry != null)
            {
                StopCoroutine(GetComponent<Combat>().parry);
            }

            if (tag == "Player")
            {
                GetComponent<Combat>().faceEnemy = true;
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
            }
            else if (tag == "Enemy")
            {
                GetComponent<Combat>().faceEnemy = true;
            }
            else if (tag == "Tourist")
            {
                GetComponent<Combat>().faceEnemy = false;
                GetComponent<Tourist>().isChargingEnemy = false;
            }

            if (state == 100 || state == 101)
            {
                isFlinching = true;
            }
            else if (state == -20)
            {
                isBlockedBack = true;
            }
            else if (state == 110)
            {
                isKnockedBack = true;
            }
            else if (state == 115)
            {
                if (GetComponent<Combat>().enemy.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
                {
                    isDove = true;
                } else if(GetComponent<Combat>().enemy.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
                {
                    isBearhugged = true;
                }
            }

            if (isFlinching)
            {
                if (GetComponent<Combat>().enemy.GetComponent<Souvenirs>().hasPoison)
                {
                    StartCoroutine(GetComponent<Combat>().enemy.GetComponent<Souvenirs>().RatPoison());
                }
                reactionTime = StartCoroutine(ReactionTime(combatManagear.GetComponent<CombatStats>().flinchTime, damage, state));
            }
            else if (isBlockedBack)
            {
                if (GetComponent<Combat>().enemy.GetComponent<Souvenirs>().hasPoison)
                {
                    StartCoroutine(GetComponent<Combat>().enemy.GetComponent<Souvenirs>().RatPoison());
                }
                blockedBack = StartCoroutine(BlockedBack(damage, combatManagear.GetComponent<CombatStats>().flinchTime));
            }
            else if (isKnockedBack)
            {
                if (GetComponent<Combat>().enemy.GetComponent<Souvenirs>().hasPoison)
                {
                    StartCoroutine(GetComponent<Combat>().enemy.GetComponent<Souvenirs>().RatPoison());
                }
                reactionTime = StartCoroutine(ReactionTime(combatManagear.GetComponent<CombatStats>().knockbackTime, damage, state));
            } else if (isDove)
            {
                dove = StartCoroutine(Dove(state));
            }
    }

    public IEnumerator ReactionTime(float t, float damage, int state) //Time that gameobject is flinching
    {
        GetComponent<Combat>().canBlock = false;
        GetComponent<Health>().SubtractHealth(damage);
        if (canBeCounterHit && state != 110)
        {
            combatManagear.GetComponent<AttackStatusManager>().CounterHit(GetComponent<Combat>().enemy);
            canBeCounterHit = false;
        } else if (canBePunishHit && state != 110)
        {
            combatManagear.GetComponent<AttackStatusManager>().PunishHit(GetComponent<Combat>().enemy);
            canBePunishHit = false;
        } else if (state == 110)
        {
            combatManagear.GetComponent<AttackStatusManager>().Knockdown(GetComponent<Combat>().enemy);
        }

        fSEInstance.FlinchGrunt();
        anim.SetInteger("State", state);

        if (tag != "Tourist")
        {
            if (GetComponent<Throw>().currentThrowable != null)
            {
                GetComponent<Throw>().Equiping(false);
            }
        }

        yield return new WaitForSeconds(.15f);
        anim.SetBool("canTransition", false);

        yield return new WaitForSeconds(t);
        isFlinching = false;
        isFlinchBuffering = true;

        if (isKnockedBack)
        {
            isKnockedBack = false;
            isKnockedDown = true;
            anim.SetInteger("State", 115);
        } 

        if (tag == "Enemy")
        {
            GetComponent<Combat>().isParrying = false;
            GetComponent<Combat>().secondary = 0;
        }

        if (!isKnockedDown)
        {
            if (GetComponent<Throw>().isEquipped == false)
            {
                anim.SetInteger("State", 0);
            }
            else
            {
                anim.SetInteger("State", 40);
            }

            yield return new WaitForSeconds(.15f); //Stun time after flinching
            isFlinchBuffering = false;

            if (tag == "Enemy")
            {
                GetComponent<AiBehavior>().isChasingEnemy = true;
                GetComponent<Combat>().faceEnemy = true;
            }
            if (tag == "Player")
            {
                GetComponent<Combat>().canAttack = true;
                GetComponent<CharacterController>().enabled = true;
            }
        }
        else
        {
            isFlinchBuffering = false;
            recovery = StartCoroutine(Recovery());
            GetComponent<Combat>().canUseTechnique = true;

            if (tag == "Player") 
            { 
                GetComponent<Combat>().canAttack = true;
            }
            else
            {
                GetComponent<AiBehavior>().isChargingEnemy = true;
            }
        }
        anim.SetBool("canTransition", true);
    }

    public IEnumerator BlockedBack(float damage, float time)
    {
        GetComponent<CoroutineManager>().CancelCoroutines(blockedBack);

        isBlockedBack = true;
        if(GetComponent<Stamina>().stamina < combatManagear.GetComponent<CombatStats>().blockMinAmount)
        {
            GetComponent<Health>().SubtractHealth(damage * combatManagear.GetComponent<CombatStats>().blockMultiplier);
        }
        
        GetComponent<Stamina>().SubtractStamina(combatManagear.GetComponent<CombatStats>().staminaDodgeBlockCost);
        yield return new WaitForSeconds(time);
        isBlockedBack = false;

        if(tag == "Player")
            GetComponent<CharacterController>().enabled = true;

        if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; GetComponent<AiBehavior>().isChasingEnemy = true; }
    }

    public IEnumerator Recovery()
    {
        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = true;
        }

        GetComponent<CoroutineManager>().CancelCoroutines(recovery);

        isKnockedDown = false;
        isDove = false;
        isRecovering = true;
        GetComponent<Combat>().invinsible = true;

        anim.SetInteger("State", 115);
        
        yield return new WaitForSeconds(.5f);
        anim.SetInteger("State", 118);
        yield return new WaitForSeconds(.7f);
        
        GetComponent<Combat>().canBlock = true;
        anim.SetInteger("State", 0);
        GetComponent<Combat>().canAttack = true;
        GetComponent<Combat>().invinsible = false;
        GetComponent<Combat>().canUseTechnique = true;
        isRecovering = false;

        if (tag == "Enemy")
        {
            GetComponent<Combat>().faceEnemy = true;
            GetComponent<AiBehavior>().isChargingEnemy = true;
        }
    }

    public IEnumerator Stun(int s)
    {
        GetComponent<Combat>().canNextAttack = false;
        GetComponent<Combat>().isAttacking = false;
        GetComponent<Combat>().faceEnemy = false;
        GetComponent<Combat>().isGuardBreaking = false;
        GetComponent<Throw>().isAiming = false;
        GetComponent<Throw>().isThrowing = false;
        isStunned = true;

        GetComponent<CoroutineManager>().CancelCoroutines(stun);

        combatManagear.GetComponent<AttackStatusManager>().Stun(GetComponent<Combat>().enemy);

        if (GetComponent<Throw>().currentThrowable != null)
        {
            GetComponent<Throw>().Equiping(false);
        }

        fSEInstance.FlinchGrunt();
        anim.SetInteger("State", s);

        yield return new WaitForSeconds(combatManagear.GetComponent<CombatStats>().stunDuration);

        isStunned = false;
        GetComponent<Combat>().canUseTechnique = true;
        GetComponent<Combat>().faceEnemy = true;

        if (tag == "Enemy")
        {
            if (!GetComponent<AiBehavior>().isPunchingBag)
            {
                GetComponent<AiBehavior>().isChargingEnemy = true;
                GetComponent<AiBehavior>().isChasingEnemy = false;
            }
            else { GetComponent<AiBehavior>().AssignIdle(); }
        }

        GetComponent<Combat>().ShouldNotMove();
        anim.SetBool("canTransition", true);
    }

    public IEnumerator Dove(int state)
    {
        GetComponent<CoroutineManager>().CancelCoroutines(dove);

        yield return new WaitForSeconds(.05f);

        GetComponent<Combat>().faceEnemy = true;
        isDove = true;

        anim.SetInteger("State", state);

        yield return new WaitForSeconds(.1f);
        GetComponent<Combat>().faceEnemy = false;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator GroundFlinch(float damage)
    {
        GetComponent<Health>().SubtractHealth(damage);
        fSEInstance.FlinchGrunt();
        anim.SetInteger("State", 107);
        yield return new WaitForSeconds(.25f);
        anim.SetInteger("State", 115);
    }

    public IEnumerator Bearhugged(float damage)
    {
        GetComponent<CoroutineManager>().CancelCoroutines(bearHugged);

        yield return new WaitForSeconds(.05f);

        isBearhugged = true;
        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = false;
        }

        StartCoroutine(fSEInstance.BearHuggedGrunt(gameObject));
        anim.SetInteger("State", 109);
        yield return new WaitForSeconds(.1f);
        anim.SetInteger("State", 109);

        if (tag == "Enemy")
        {
            GetComponent<Combat>().bearhugBreakOut = StartCoroutine(GetComponent<Combat>().BearHugBreakOut());
        }

        while (isBearhugged)
        {
            if (tag == "Enemy")
            {
                GetComponent<Combat>().faceEnemy = true;
            }
            GetComponent<Health>().SubtractHealth(fightStyleManager.GetComponent<ProWrestlingStats>().bearhugDamagePerTick);
            yield return new WaitForSeconds(.15f);
        }

    }

    public IEnumerator Parried(bool wasBearHugged)
    {
        isParried = true;

        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = false;
        }

        GetComponent<Combat>().canAttack = false;
        GetComponent<Combat>().isAttacking = false;
        GetComponent<Combat>().isAttacking = false;
        GetComponent<Combat>().faceEnemy = false;
        GetComponent<Combat>().isGuardBreaking = false;
        GetComponent<Combat>().canBlock = false;
        GetComponent<Dodge>().canDodge = false;
        GetComponent<Throw>().isAiming = false;
        GetComponent<Throw>().isThrowing = false;

        if (!wasBearHugged)
        {
            GetComponent<CoroutineManager>().CancelCoroutines(parried);
            combatManagear.GetComponent<AttackStatusManager>().Parry(GetComponent<Combat>().enemy);
        }

        gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
        gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .5f));
        anim.SetInteger("State", 120);

        if (tag == "Enemy" && idManagerInstance.gameMode == IdManagear.mode.playerVsAi)
        {
            gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
            gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .2f));
        }

        yield return new WaitForSeconds(combatManagear.GetComponent<CombatStats>().parriedTime);

        GetComponent<Combat>().canBlock = true;
        GetComponent<Dodge>().canDodge = true;

        if (tag == "Enemy")
        {
            isParried = false;
            GetComponent<Combat>().faceEnemy = true;
            GetComponent<Combat>().afterAttack = StartCoroutine(GetComponent<Combat>().AfterAttack());
        }

        if (tag == "Player")
        {
            anim.SetInteger("State", 0);
            yield return new WaitForSeconds(.1f);
            isParried = false;
            GetComponent<CharacterController>().enabled = true;
        }

    }

    public void Knockback()
    {
        ReactionInitiation(110, GetComponent<Combat>().enemy.GetComponent<Combat>().CalculateDamage());
    }

    public IEnumerator Surrender()
    {
        GetComponent<CoroutineManager>().CancelCoroutines(surrender);

        isSurrendering = true;
        GetComponent<Combat>().isBlocking = false;
        GetComponent<Combat>().invulnerable = true;

        if (tag == "Enemy" && idManagerInstance.gameMode != IdManagear.mode.AiVsAi)
        {
            if(hudManager.GetComponent<HUDManager>().hudType != HUDManager.hud.none)
                hudManager.GetComponent<HUDManager>().finisherPrompt.SetActive(true);

            GetComponent<Combat>().faceEnemy = false;
        }

        hudManager.GetComponent<HUDManager>().finisherText.text = "FINISH HIM!";
        yield return new WaitForSeconds(.05f);
        anim.SetInteger("State", 125);
        yield return new WaitForSeconds(6);

        if (!GetComponent<Combat>().enemy.GetComponent<Combat>().isFinishing)
        {
            hudManager.GetComponent<HUDManager>().finisherText.text = "";
            hudManager.GetComponent<HUDManager>().finisherPrompt.SetActive(false);
            GetComponent<Health>().AddHealth(200); //Adds health if failed to be finished
            GetComponent<Combat>().invulnerable = false;
            recovery = StartCoroutine(Recovery());
        }
    }
}
