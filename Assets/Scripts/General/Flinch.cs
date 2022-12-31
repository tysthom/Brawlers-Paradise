using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flinch : MonoBehaviour
{
    public Animator anim;
    GameObject gameManager;
    GameObject combatManagear;
    IdManagear idManagerInstance;
    ResultStats resultStatsInstance;
    BrawlerStats brawlerStatsInstance;
    public GameObject divePosition;
    public GameObject bearhuggedPosition;
    public GameObject fightStyleManager;

    public bool isReacting;
    public bool isFlinching; //True if flinching (moving backwards & not animation flinching)
    public bool isFlinchBuffering; //Prevents player from attacking while also allowing them to move around
    public bool isDove;
    public bool isBearhugged;
    public bool isKnockedBack; //True if currently being knockedback
    public bool isKnockedDown;
    public bool isStunned; //True if stunned
    public bool isParried; //True if parried
    public bool isParryBuffering; //Prevents player from attacking while also allowing them to move around
    public bool isPoisoned; //True if poisoned

    [Header("Coroutine")]
    public Coroutine reactionTime;
    public Coroutine recovery;
    public Coroutine stun;
    public Coroutine dove;
    public Coroutine groundFlinch;
    public Coroutine bearHugged;
    public Coroutine parried;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager");
        combatManagear = GameObject.Find("Combat Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        resultStatsInstance = gameManager.GetComponent<ResultStats>();
        brawlerStatsInstance = combatManagear.GetComponent<BrawlerStats>();
        fightStyleManager = GameObject.Find("Fight Style Manager");

    }

    private void Update()
    {
        if (isFlinching || isFlinchBuffering || isParried || isParryBuffering || isStunned || isKnockedBack || isKnockedDown || isDove || isBearhugged)
        {
            isReacting = true;
            if(tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; }
        }
        else
        {
            isReacting = false;
        }

        if((isFlinching || isKnockedBack) && tag == "Player") { GetComponent<CharacterController>().enabled = false; }

        if((isFlinching || isParried) && !GetComponent<Dodge>().isColliding) { transform.position -= transform.forward * combatManagear.GetComponent<CombatStats>().flinchDistance * Time.deltaTime; }

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
            //GetComponent<Health>().SubtractHealth(1);
            

        }

        if (tag == "Enemy")
        {
            if (isReacting)
            {
                GetComponent<AiBehavior>().isChargingEnemy = false;
                GetComponent<AiBehavior>().isChasingEnemy = false;
                GetComponent<AiBehavior>().canGlideToEnemy = false;
            }
        }
    }

    public void ReactionInitiation(int state, float damage) //Causes gameobject to flinch
    {
        isStunned = false;
        if (tag == "Player") { GetComponent<CharacterController>().enabled = false; }
        if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; }
        //Handles player's parry
        if (GetComponent<Combat>().isParrying && damage != combatManagear.GetComponent<CombatStats>().throwableDamage
            && damage != fightStyleManager.GetComponent<MMAStats>().mmaDiveInitialDamage) //PARRY
        { //Makes sure that player can't parry if hit by a throwable
            GetComponent<Combat>().isParrying = false;
            GetComponent<Combat>().isParryBuffering = false;
            GetComponent<Combat>().enemy.GetComponent<Flinch>().parried = StartCoroutine(GetComponent<Combat>().enemy.GetComponent<Flinch>().Parried());
            return;
        }
        GetComponent<Combat>().canAttack = false;
        GetComponent<Combat>().canNextAttack = false;
        GetComponent<Combat>().isAttacking = false;
        GetComponent<Combat>().isGuardBreaking = false;
        GetComponent<Combat>().isParrying = false;
        GetComponent<Combat>().isParryBuffering = false;
        GetComponent<Combat>().isDiving = false;
        GetComponent<Combat>().isGroundAttacking = false;
        GetComponent<Combat>().isGroundIdle = false;

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
                StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
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
            #region RESULT STATS
            if (idManagerInstance.brawler1 == GetComponent<Combat>().enemy) //RESULT STATS
            {
                resultStatsInstance.AttacksLanded(idManagerInstance.brawler1);
            }
            else if (idManagerInstance.brawler2 == GetComponent<Combat>().enemy)
            {
                resultStatsInstance.AttacksLanded(idManagerInstance.brawler2);
            }
            #endregion

            if (isFlinching)
            {
                if (GetComponent<Combat>().enemy.GetComponent<Souvenirs>().hasPoison)
                {
                    StartCoroutine(GetComponent<Combat>().enemy.GetComponent<Souvenirs>().RatPoison());
                }
                reactionTime = StartCoroutine(ReactionTime(combatManagear.GetComponent<CombatStats>().flinchTime, damage, state));
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
        GetComponent<Combat>().canParry = false;
        GetComponent<Health>().SubtractHealth(damage);
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
            if (GetComponent<Health>().health < 1)
            {
                GetComponent<Death>().Die();
                yield return null;
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

    public IEnumerator Recovery()
    {
        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = true;
        }

        GetComponent<Combat>().invinsible = true;
        yield return new WaitForSeconds(.5f);
        anim.SetInteger("State", 118);
        yield return new WaitForSeconds(.5f);
        isKnockedDown = false;
        isDove = false;
        GetComponent<Combat>().canParry = true;
        anim.SetInteger("State", 0);
        GetComponent<Combat>().canAttack = true;
        GetComponent<Combat>().invinsible = false;
        GetComponent<Combat>().canUseTechnique = true;

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

        if (GetComponent<Throw>().currentThrowable != null)
        {
            GetComponent<Throw>().Equiping(false);
        }

        anim.SetInteger("State", s);
        #region RESULT STATS
        if (idManagerInstance.brawler1 == GetComponent<Combat>().enemy) //RESULT STATS
        {
            resultStatsInstance.AttacksLanded(idManagerInstance.brawler1);
        }
        else if (idManagerInstance.brawler2 == GetComponent<Combat>().enemy)
        {
            resultStatsInstance.AttacksLanded(idManagerInstance.brawler2);
        }
        #endregion

        yield return new WaitForSeconds(.5f);
        anim.SetBool("canTransition", false);
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
        GetComponent<Combat>().faceEnemy = true;
        isDove = true;

        GetComponent<CoroutineManager>().CancelCoroutines(dove);

        anim.SetInteger("State", state);

        yield return new WaitForSeconds(.1f);
        GetComponent<Combat>().faceEnemy = false;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator GroundFlinch(float damage)
    {
        GetComponent<Health>().SubtractHealth(damage);
        anim.SetInteger("State", 107);
        yield return new WaitForSeconds(.5f);
        anim.SetInteger("State", 115);
    }

    public IEnumerator Bearhugged(float damage)
    {
        GetComponent<Combat>().canNextAttack = false;
        GetComponent<Combat>().isAttacking = false;
        GetComponent<Combat>().faceEnemy = false;
        GetComponent<Combat>().isGuardBreaking = false;
        GetComponent<Throw>().isAiming = false;
        GetComponent<Throw>().isThrowing = false;
        isBearhugged = true;
        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = false;
        }

        GetComponent<CoroutineManager>().CancelCoroutines(bearHugged);
        
        anim.SetInteger("State", 109);
        
        yield return new WaitForSeconds(.1f);
        anim.SetBool("canTransition", false);

        if(tag == "Enemy")
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

    public IEnumerator Parried()
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
        GetComponent<Combat>().canParry = false;
        GetComponent<Dodge>().canDodge = false;
        GetComponent<Throw>().isAiming = false;
        GetComponent<Throw>().isThrowing = false;

        GetComponent<CoroutineManager>().CancelCoroutines(parried);

        StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .5f));
        anim.SetInteger("State", 120);

        if (tag == "Enemy" && idManagerInstance.gameMode == IdManagear.mode.playerVsAi)
        {
            StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, 0f));
            GetComponent<Combat>().enemy.GetComponent<Combat>().canAttack = true;
            Time.timeScale = .1f;
            yield return new WaitForSeconds(.1f);
            Time.timeScale = 1;
        }
        else
        {
            yield return new WaitForSeconds(.4f);
        }
     
        GetComponent<Combat>().canParry = true;
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
        ReactionInitiation(110, GetComponent<Combat>().CalculateDamage());
    }
}
