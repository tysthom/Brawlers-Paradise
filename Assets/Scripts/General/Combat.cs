using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Combat : MonoBehaviour
{
    [Header("Refences")]
    CharacterController controller;
    public Animator anim;
    public GameObject enemy;
    public GameObject enemyHead;
    GameObject gameManager;
    GameObject combatManagear;
    Vector3 velocity;
    public GameObject grabHolder;
    public CombatStats combatStatsInstance;
    public GameObject souvenirsManager;
    public GameObject fightStyleManager;
    SouvenirsManager souvenirsManagerInstance;
    IdManagear idManagerInstance;
    ResultStats resultStatsInstance;
    BrawlerStats brawlerStatsInstance;

    [Header("General Status")]
    public bool invinsible; //True if can't take damage and flinch
    public bool unkillable; //True if user can't be killed, but can still flinch
    public bool invulnerable; //True if user can take damage but won't be casued to flinch
    public bool faceEnemy; //True if forced to face enemy
    public bool faceHead; //True only for Ai when ground idle after performing dive
    public bool inCombat; // True if performing any type of attack
    public bool shouldMove = false; //Determines if should move when attacking
    public bool canAttack = true; //True if can attack after flinching
    public bool attackBuffering = false; //True while transferring from attack animation to idle, within attack animation. Prevents player from attacking
    public bool isAttacking = false; //Determines if player is attacking
    public bool canNextAttack = false; //True if can perform next attack in attack combo
    public bool isCounterAttacking = false; //True if performing a counter attack after parrying
    public bool isCounterAttackBuffering = false;
    public bool canFinish;
    public bool isFinishing;
    public bool canParry; //True if can parry
    public bool isParrying; //True if is parrying
    public bool isParryBuffering;
    public float primary, secondary; //Trigger values
    bool secondaryLifted;
    public bool canUseTechnique; //True when Technique can be used

    [Header("Karate Status")]
    public bool stanceCooldown;

    [Header("Boxing Status")]
    public bool isGuardBreaking; //True if is performing guard break
    public bool guardBreakerComplete = true; //False when guardbreaking or when the time is still going to perform that attack

    [Header("MMA Status")]
    public bool shouldDive; //Determines if should be moving while diving
    public bool isDiving; //True if performing diving attack ONLY
    public bool isGroundIdle; 
    public bool isGroundAttacking; //True if either groundIdle or performing ground attacks

    [Header("TKD Status")]
    public bool isComboing;

    [Header("Kung Fu Status")]
    public bool isEyePoking;

    [Header("Pro Wrestling Status")]
    public bool isBearhugGrabbing;
    public bool isBearhugging;
    public int bearhugBreakOutCount = 0;

    [Header("Coroutine")]
    public Coroutine parry;
    public Coroutine counterAttack;
    public Coroutine guardBreaker; //Once assigned, starts GuardBreaker(). If cancelled, stops the script and doesn't force brawler back to idle like usual
    public Coroutine diveAttack;
    public Coroutine groundAttack;
    public Coroutine combo;
    public Coroutine eyePoke;
    public Coroutine bearhugGrab;
    public Coroutine bearhugging;
    public Coroutine bearhugBreakOut;
    public Coroutine basicAttack;
    public Coroutine finisher;
    public Coroutine afterAttack;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        combatManagear = GameObject.Find("Combat Manager");
        combatStatsInstance = combatManagear.GetComponent<CombatStats>();
        souvenirsManager = GameObject.Find("Souvenir Manager");
        souvenirsManagerInstance = souvenirsManager.GetComponent<SouvenirsManager>();
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        resultStatsInstance = gameManager.GetComponent<ResultStats>();
        brawlerStatsInstance = combatManagear.GetComponent<BrawlerStats>();
        fightStyleManager = GameObject.Find("Fight Style Manager");

        if (tag == "Player") { 
            controller = GetComponent<CharacterController>(); controller.enabled = true; 
            enemy = idManagerInstance.brawler2; 
        }
        if (tag == "Enemy") {
            faceEnemy = true; GetComponent<CapsuleCollider>().enabled = true; 
            if(idManagerInstance.brawler1 == gameObject)
            {
                enemy = idManagerInstance.brawler2;
            }
            else if(idManagerInstance.brawler2 == gameObject)
            {
                enemy = idManagerInstance.brawler1;
            }
        }
        foreach(Transform child in enemy.transform)
        {
            if(child.gameObject.name == "Head Position")
            {
                enemyHead = child.gameObject;
            }
        }
        GetComponent<Death>().enemy = enemy;
        anim = GetComponent<Animator>();
        canAttack = true;
        canParry = true;
        canUseTechnique = true;
    }

    void Update()
    {
        if (tag == "Tourist")
        {
            if(GetComponent<Tourist>().companion == null) { return; }
        }

        #region Technique Managment
        if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate && !stanceCooldown)
        {
            canUseTechnique = true;
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing &&
                 canAttack)
        {
            canUseTechnique = true;
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA && canAttack && !GetComponent<Flinch>().isReacting &&
                  GetComponent<Stamina>().stamina >= combatStatsInstance.playerDodgeMinAmount)
        {
            canUseTechnique = true;
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo && canAttack && !GetComponent<Flinch>().isReacting &&
                  !isComboing)
        {
            canUseTechnique = true;
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu && canAttack && !GetComponent<Flinch>().isReacting &&
                  !isEyePoking)
        {
            canUseTechnique = true;
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling && canAttack && !GetComponent<Flinch>().isReacting &&
                  !isBearhugging)
            canUseTechnique = true;
        else
        {
            canUseTechnique = false;
        }
        #endregion

        if (inCombat || GetComponent<Flinch>().isReacting || isParryBuffering || isParrying || !secondaryLifted)
        {
            canParry = false;
        }
        else
        {
            canParry = true;
        }

        if (!inCombat && !isParrying && !isCounterAttackBuffering && !GetComponent<Flinch>().isReacting && !GetComponent<Throw>().isAiming && !attackBuffering || isGroundIdle)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }


        if (tag == "Player")
        {
            primary = Input.GetAxis("Primary");
            secondary = Input.GetAxis("Secondary");
            bool techniqueDown = Input.GetButton("Technique");
            bool attackButtonDown = Input.GetButton("Attack");
            bool dodgeButtonDown = Input.GetButtonDown("Dodge");
            bool souvenir = Input.GetButtonDown("Secondary Special");

            if (!inCombat && !isParrying && !GetComponent<Flinch>().isReacting && !GetComponent<Throw>().isAiming || GetComponent<Flinch>().isDove)
            {
                faceEnemy = false;
            }
            else
            {
                faceEnemy = true;
            }
            
            if (dodgeButtonDown && GetComponent<Dodge>().canDodge) //DODGE
            {
                StartCoroutine(GetComponent<Dodge>().PerformDodge());
            } else if(dodgeButtonDown && GetComponent<Flinch>().isBearhugged)
            {
                bearhugBreakOut = StartCoroutine(GetComponent<Combat>().BearHugBreakOut());
            }

            if (souvenir && GetComponent<Souvenirs>().canUseSouvenir) //SOUVENIR
            {
                GetComponent<Souvenirs>().ActivateSouvenir();
            }

            if (techniqueDown && canUseTechnique && canAttack && !GetComponent<Throw>().isEquipped) //TECHNIQUE
            {
                    GetComponent<Technique>().UseTechnique(gameObject);
            }

            canFinish = GetDistanceToFinish() && enemy.GetComponent<Flinch>().isSurrendering;

            if (attackButtonDown && (canAttack || canNextAttack) && !attackBuffering && !GetComponent<Throw>().isEquipped) //ATTACK
            {
                if (!canFinish)
                {
                    if (enemy.GetComponent<Flinch>().isParried)
                    {
                        if (!isCounterAttacking)
                            counterAttack = StartCoroutine(CounterAttack());
                    }
                    else if (isGroundIdle)
                    {
                        groundAttack = StartCoroutine(GroundAttack());
                    }
                    else //Controls attack type/variation
                    {
                        anim.speed = 2 - brawlerStatsInstance.AttackSpeed(gameObject); //Controls attack speed
                        if (anim.GetInteger("State") < 3)
                        {
                            basicAttack = StartCoroutine(Attack(3, combatStatsInstance.brawler1FirstAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                        }
                        else if (anim.GetInteger("State") == 3 && canNextAttack == true)
                        {
                            int i = Random.Range(4, 6);
                            basicAttack = StartCoroutine(Attack(i, combatStatsInstance.brawler1SecondAAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                        }
                        else if (anim.GetInteger("State") == 4 && canNextAttack == true)
                        {
                            int i = Random.Range(1, 3);
                            anim.SetInteger("Variation", i);
                            basicAttack = StartCoroutine(Attack(6, combatStatsInstance.brawler1ThirdAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                        }
                        else if (anim.GetInteger("State") == 5 && canNextAttack == true)
                        {
                            int i = Random.Range(1, 3);
                            anim.SetInteger("Variation", i);
                            basicAttack = StartCoroutine(Attack(7, combatStatsInstance.brawler1ThirdAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                        }
                    }
                } else
                {
                    finisher = StartCoroutine(Finisher());
                    Debug.Log("Finish!");
                }
            }
        }             
        //END OF PLAYER SECTION

        //START OF AI SECTION
        if(tag == "Enemy")
        {
            Emergency();
            if (isParrying) { GetComponent<AiBehavior>().canGlideToEnemy = false; }

            if (GetComponent<Flinch>().isFlinching == true)
            {
                //agent.isStopped set to true in Update of AiBehavior script
                return;
            }
            if (isAttacking)
            {
                if (GetComponent<AiBehavior>().enemy.tag == "Player" && Vector3.Distance(transform.position, enemy.transform.position) > GetComponent<AiBehavior>().attackRadius)
                {
                    GetComponent<AiBehavior>().canGlideToEnemy = true;
                }
                else
                {
                    GetComponent<AiBehavior>().canGlideToEnemy = false;
                }
            }
            if (GetComponent<AiBehavior>().canGlideToEnemy)
            {
                    if (Vector3.Distance(transform.position, enemy.transform.position) > GetComponent<AiBehavior>().attackRange)
                    {
                        transform.position = Vector3.Lerp(transform.position, enemy.transform.position, GetComponent<AiBehavior>().glideAmount * Time.deltaTime);
                    }

            }
        }
        //END OF AI SECTION

        //START OF TRIGGER
        if (secondary != 0) //Makes sure trigger is down
        {
            secondaryLifted = false;
            if (!GetComponent<Throw>().isEquipped)
            {
                if (!inCombat && !GetComponent<Flinch>().isReacting && canParry)
                {
                    if (tag == "Enemy") //Stops Ai from continuously parrying
                    {
                        secondary = 0; 
                    }
                    canParry = false;
                    isParrying = true;
                    parry = StartCoroutine(Parry());
                }
            }
            else
            {
                isParrying = false;
                if (!inCombat && !GetComponent<Flinch>().isReacting)
                {
                    if (primary != 0)
                    {
                        GetComponent<Throw>().ThrowObject();
                    }
                    else
                    {
                        faceEnemy = true;
                        GetComponent<Throw>().AimObject();
                    }
                }
            }
        }
        else { if (GetComponent<Throw>()) { GetComponent<Throw>().isAiming = false; } if (secondaryLifted == false) secondaryLifted = true; }

        if (shouldMove && !GetComponent<Flinch>().isReacting)
        {
            #region Brawler1 Attack Distances
            if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
            {
                if (anim.GetInteger("State") == 3)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler1FirstAttackDistance);
                }
                else if (anim.GetInteger("State") == 4)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler1SecondAAttackDistance);
                }
                else if (anim.GetInteger("State") == 5)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler1SecondBAttackDistance);
                }
                else if (anim.GetInteger("State") == 6)
                {
                    if (anim.GetInteger("Variation") == 1)
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler1ThirdAttackV1Distance);
                    }
                    else
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler1ThirdAttackV2Distance);
                    }
                }
                else if (anim.GetInteger("State") == 7)
                {
                    if (anim.GetInteger("Variation") == 1)
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler1ThirdAttackV3Distance);
                    }
                    else
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler1ThirdAttackV4Distance);
                    }
                }
                else if (anim.GetInteger("State") == 10)
                {
                    MoveWhenAttacking(combatStatsInstance.guardBreakerDistance);
                }
                else if (anim.GetInteger("State") == 15)
                {
                    MoveWhenAttacking(15);
                }
            }
            #endregion

            #region Brawler2 Attack Distances
            if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler2)
            {
                if (anim.GetInteger("State") == 3)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler2FirstAttackDistance);
                }
                else if (anim.GetInteger("State") == 4)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler2SecondAAttackDistance);
                }
                else if (anim.GetInteger("State") == 5)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler2SecondBAttackDistance);
                }
                else if (anim.GetInteger("State") == 6)
                {
                    if (anim.GetInteger("Variation") == 1)
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ThirdAttackV1Distance);
                    }
                    else
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ThirdAttackV2Distance);
                    }
                }
                else if (anim.GetInteger("State") == 7)
                {
                    if (anim.GetInteger("Variation") == 1)
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ThirdAttackV3Distance);
                    }
                    else
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ThirdAttackV4Distance);
                    }
                }
                else if (anim.GetInteger("State") == 10)
                {
                    MoveWhenAttacking(combatStatsInstance.guardBreakerDistance);
                }
                else if (anim.GetInteger("State") == 15)
                {
                    MoveWhenAttacking(15);
                }
            }
            #endregion
        }

        if(shouldDive && !GetComponent<Flinch>().isReacting)
        {
            MoveWhenAttacking(fightStyleManager.GetComponent<MMAStats>().mmaDiveDistance);
        }

        if (isAttacking || isGuardBreaking || isCounterAttacking || isGuardBreaking || isDiving || isGroundIdle || isGroundAttacking || isComboing || isEyePoking || isBearhugGrabbing || isBearhugging || isFinishing)
        {
            inCombat = true;
        }
        else
        {
            inCombat = false;
        }

        if (shouldDive)
        {
            if (GetDistanceToDive() && !enemy.GetComponent<Combat>().invinsible)
            {
                    isDiving = false;
                    if(tag == "Enemy")
                    {
                        faceEnemy = false;
                    }
                    faceHead = true;
                    if(GetComponent<Flinch>().reactionTime != null)
                {
                    StopCoroutine(GetComponent<Flinch>().reactionTime);
                }
                    enemy.GetComponent<Flinch>().ReactionInitiation(115, fightStyleManager.GetComponent<MMAStats>().mmaDiveInitialDamage);
                
                    anim.SetInteger("State", 12);
                    isGroundIdle = true;
            }
        }
        if (isGroundIdle || isGroundAttacking)
        {
            ShouldNotDive();
            if (tag == "Player")
            {
                GetComponent<CharacterController>().enabled = false;
            }
            transform.position = enemy.GetComponent<Flinch>().divePosition.transform.position;

            GetComponent<Stamina>().SubtractStamina(1); //Subtracts stamina while idle on the ground

            if(GetComponent<Stamina>().stamina <= 0)
            {
                isGroundAttacking = false;
                isGroundIdle = false;
                if(groundAttack != null)
                {
                    StopCoroutine(groundAttack);
                }
                if (tag == "Player")
                {
                    GetComponent<CharacterController>().enabled = true;
                    faceHead = false;
                    attackBuffering = true;
                    anim.SetBool("canTransition", true);
                    StartCoroutine(AttackBufferTime()); //Recovery time that prevents player from immediately starting basic attack combo
                }
                else
                {
                    //canUseTechnique = true;
                    afterAttack = StartCoroutine(AfterAttack());
                }
               enemy.GetComponent<Flinch>().recovery = StartCoroutine(enemy.GetComponent<Flinch>().Recovery());
            }
        }
    }

    IEnumerator Parry() 
    { 
        canAttack = false;
        canNextAttack = false;
        isAttacking = false;

        GetComponent<CoroutineManager>().CancelCoroutines(parry);

        GetComponent<Dodge>().canDodge = false;
        //GetComponent<Combat>().enabled = true;
        isParrying = true;
        canParry = false;
        anim.SetInteger("State", 20);
        faceEnemy = true;
        if(tag == "Enemy") 
        { 
            GetComponent<AiBehavior>().canGlideToEnemy = false;
            if (GetComponent<AiBehavior>().waitToAttack != null)
            {
                StopCoroutine(GetComponent<AiBehavior>().waitToAttack);      
            }
        }
        yield return new WaitForSeconds(combatStatsInstance.parryTime);
            isParrying = false;
            isParryBuffering = true;
            if (tag == "Player")
            {
                GetComponent<CharacterController>().enabled = true;
                if(!isCounterAttacking)
                anim.SetInteger("State", 0);
                yield return new WaitForSeconds(combatStatsInstance.parryBufferTime);
            }
            canAttack = true;
            GetComponent<Dodge>().canDodge = true;
            if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; GetComponent<AiBehavior>().AssignIdle(); GetComponent<AiBehavior>().isChasingEnemy = true; }
            yield return new WaitForSeconds(combatStatsInstance.parryBufferTime);
            isParryBuffering = false;
            canParry = true;
        if(tag == "Enemy") { GetComponent<AiBehavior>().wTACalled = false; }
    }

    public IEnumerator CounterAttack()
    {
        Time.timeScale = 1;
        isCounterAttacking = true;
        isParrying = false;
        faceEnemy = true;
        canAttack = false;

        GetComponent<CoroutineManager>().CancelCoroutines(counterAttack);

        GetComponent<CharacterController>().enabled = true;
        ShouldHit(100);
        anim.SetInteger("State", 15);
        yield return new WaitForSeconds(combatStatsInstance.counterAttackTime);
        isCounterAttacking = false;
        faceEnemy = false;

        isCounterAttackBuffering = true;
        GetComponent<Movement>().PlayerMovement();
        yield return new WaitForSeconds(.25f);
        isCounterAttackBuffering = false;
        //canUseTechnique = true;
    }

    public IEnumerator GuardBreaker()
    {
        if (!inCombat && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Flinch>().isParried && guardBreakerComplete)
        {   ShouldNotMove();
            canUseTechnique = false;
            guardBreakerComplete = false;
            isGuardBreaking = true;
            anim.SetInteger("State", 10);

            GetComponent<CoroutineManager>().CancelCoroutines(guardBreaker);

            //GetComponent<CoroutineManager>().CancelCoroutines(parry);
            if (tag == "Player")
            {
                faceEnemy = true;
            }
            else
            {
                faceEnemy = true;
                GetComponent<AiBehavior>().agent.isStopped = true;
                /*if (GetComponent<AiBehavior>().waitToAttack != null)
            {
                StopCoroutine(GetComponent<AiBehavior>().waitToAttack);     
                GetComponent<AiBehavior>().isWaitingToAttack = false;
                GetComponent<AiBehavior>().wTACalled = false;
            } */
            }
            anim.SetInteger("State", 10);
            yield return new WaitForSeconds(combatStatsInstance.guardBreakerTime);
                if (tag == "Player")
                {
                    GetComponent<Movement>().PlayerMovement();
                }
                else
                {
                    yield return new WaitForSeconds(.5f);
                    if (!GetComponent<Flinch>().isReacting)
                    {
                        anim.SetInteger("State", 0);
                    afterAttack = StartCoroutine(AfterAttack());
                }
                    }
            isGuardBreaking = false;
            guardBreakerComplete = true;
            //canUseTechnique = true;
        }
    }

    public IEnumerator DiveAttack()
    {
        GetComponent<CoroutineManager>().CancelCoroutines(diveAttack);

        faceEnemy = true;
        anim.SetInteger("State", 11);
        anim.SetBool("canTransition", false);
        canUseTechnique = false;
        isDiving = true;
        invinsible = true;

        if (tag == "Enemy")
        {
            GetComponent<AiBehavior>().agent.isStopped = true;
        }
        yield return new WaitForSeconds(fightStyleManager.GetComponent<MMAStats>().mmaDiveTime);
        isDiving = false;
        invinsible = false;

        if (!isGroundAttacking && !isGroundIdle && !GetComponent<Flinch>().isReacting)
        {
           if(tag == "Enemy")
            {
                afterAttack = StartCoroutine(AfterAttack());
            }
        } else if (isGroundIdle && tag == "Enemy")
        {
            groundAttack = StartCoroutine(GroundAttack());
        }
    }

    public void Dive()
    {
        shouldDive = true;
    }

    public void ShouldNotDive()
    {
        //invinsible = false;
        shouldDive = false;
        velocity.z = 0;
    }

    public IEnumerator GroundAttack()
    {
        GetComponent<CoroutineManager>().CancelCoroutines(groundAttack);

        canAttack = false;
        isGroundIdle = false;
        isGroundAttacking = true;

        int i = Random.Range(1, 3);
        GetComponent<Stamina>().SubtractStamina(fightStyleManager.GetComponent<MMAStats>().mmaGroundAttackStaminaCost);
        anim.SetInteger("Variation", i);
        anim.SetInteger("State", 13);
        yield return new WaitForSeconds(fightStyleManager.GetComponent<MMAStats>().mmaGroundAttackTime);
        invinsible = false;
        anim.SetInteger("State", 12);
        yield return new WaitForSeconds(fightStyleManager.GetComponent<MMAStats>().mmaGroundBufferTime);
        canAttack = true;
        isGroundAttacking = false;
        isGroundIdle = true;
        if(tag == "Enemy")
        {
           groundAttack = StartCoroutine(GroundAttack());
        }
    }

    public IEnumerator Combo()
    {
            if (!inCombat && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Flinch>().isParried)
            {
                ShouldNotMove();
                canUseTechnique = false;
                isComboing = true;
                anim.SetInteger("State", 10);

                if (tag == "Player")
                {
                    faceEnemy = true;
                }
                else
                {
                    faceEnemy = true;
                    GetComponent<AiBehavior>().agent.isStopped = true;
                }
                anim.SetInteger("State", 10);
                yield return new WaitForSeconds(fightStyleManager.GetComponent<TkdStats>().comboTime);
                if (tag == "Player")
                {
                    GetComponent<Movement>().PlayerMovement();
                }
                else
                {
                    yield return new WaitForSeconds(.5f);
                    if (!GetComponent<Flinch>().isReacting)
                    {
                        anim.SetInteger("State", 0);
                        afterAttack = StartCoroutine(AfterAttack());
                    }
                }
                isComboing = false;
        }
    }

    public IEnumerator EyePoke()
    {
        if (!inCombat && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Flinch>().isParried)
        {
            GetComponent<CoroutineManager>().CancelCoroutines(eyePoke);

            ShouldNotMove();
            canUseTechnique = false;
            isEyePoking = true;
            anim.SetInteger("State", 10);

            if (tag == "Player")
            {
                faceEnemy = true;
            }
            else
            {
                faceEnemy = true;
                GetComponent<AiBehavior>().agent.isStopped = true;
            }

            anim.SetInteger("State", 10);
            yield return new WaitForSeconds(fightStyleManager.GetComponent<kungFuStats>().eyePokeTime);
            if (tag == "Player")
            {
                GetComponent<Movement>().PlayerMovement();
            }
            else
            {
                yield return new WaitForSeconds(.5f);
                if (!GetComponent<Flinch>().isReacting)
                {
                    anim.SetInteger("State", 0);
                    afterAttack = StartCoroutine(AfterAttack());
                }
            }
            isEyePoking = false;
        }
    }

    public IEnumerator BearhugGrab()
    {
        if (!inCombat && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Flinch>().isParried)
        {
            //invulnerable = true;

            GetComponent<CoroutineManager>().CancelCoroutines(bearhugGrab);

            ShouldNotMove();
            canUseTechnique = false;
            isBearhugGrabbing = true;
            anim.SetInteger("State", 10);

            if (tag == "Player")
            {
                faceEnemy = true;
            }
            else
            {
                faceEnemy = true;
                GetComponent<AiBehavior>().agent.isStopped = true;
            }
            yield return new WaitForSeconds(fightStyleManager.GetComponent<ProWrestlingStats>().bearhugGrabTime);

            if (tag == "Player")
            {
                GetComponent<Movement>().PlayerMovement();
            }
            else
            {
                yield return new WaitForSeconds(.5f);
                if (!GetComponent<Flinch>().isReacting)
                {
                    anim.SetInteger("State", 0);
                    afterAttack = StartCoroutine(AfterAttack());
                }
            }

            invulnerable = false;
            isBearhugGrabbing = false;
        }
    }

    public IEnumerator Bearhugging()
    {
        GetComponent<CoroutineManager>().CancelCoroutines(bearhugging);
        //invulnerable = true;

        isBearhugging = true;
        anim.SetInteger("State", 11);
        yield return new WaitForSeconds(10);
    }

    public IEnumerator BearHugBreakOut()
    { 
        if (tag == "Player")
        {
            bearhugBreakOutCount += Random.Range(5, 11);
            
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(1, 6));
            bearhugBreakOutCount = 100;
        }

        if (bearhugBreakOutCount >= 100)
        {
            bearhugBreakOutCount = 0;
            GetComponent<Flinch>().isBearhugged = false;
            enemy.GetComponent<Combat>().isBearhugging = false;
            enemy.GetComponent<Combat>().invulnerable = false;
            anim.SetBool("canTransition", true);

            GetComponent<CoroutineManager>().CancelCoroutines(bearhugBreakOut);

            if (tag == "Player")
            {
                GetComponent<CharacterController>().enabled = true;
            }
            GetComponent<Flinch>().isBearhugged = false;
            GetComponent<Flinch>().parried = StartCoroutine(GetComponent<Flinch>().Parried());
            enemy.GetComponent<Flinch>().parried = StartCoroutine(enemy.GetComponent<Flinch>().Parried());
        }
    }

    public IEnumerator Attack(int state, float time)
    {
        ShouldNotMove();
        canNextAttack = false;
        if (tag == "Player") //Makes player face enemy if close enough and plays the attacking animation for a set amount of time
        {
            if (!GetComponent<Flinch>().isReacting && !isParrying)
            {
                GetComponent<CoroutineManager>().CancelCoroutines(basicAttack);

                resultStatsInstance.TotalAttacks(idManagerInstance.brawler1);
                //canUseTechnique = false;
                anim.SetInteger("State", state);
                isAttacking = true;
                canNextAttack = false;
                faceEnemy = true;
                yield return new WaitForSeconds(time);
                anim.speed = 1;
                StartCoroutine(CouldNextAttack());
                //canUseTechnique = true;
                yield return new WaitForSeconds(.5f); //Time that brawler waits before they can move
                    if (state == anim.GetInteger("State")) //Checks to see if the animator has moved to the next attack animation
                    { //If not, returns player to normal movement
                        isAttacking = false;
                        faceEnemy = false;
                    GetComponent<Movement>().PlayerMovement();
                    }
            }
        }
        if (tag == "Enemy") 
        {       
            if (isAttacking == false) //Makes sure that Ai isn't currently attacking, then makes it attack
            {
                if (!GetComponent<Flinch>().isReacting && !isParrying)
                {
                    if (idManagerInstance.brawler1 == gameObject)
                    {
                        resultStatsInstance.TotalAttacks(idManagerInstance.brawler1);
                    }
                    else if (idManagerInstance.brawler2 == gameObject)
                    {
                        resultStatsInstance.TotalAttacks(idManagerInstance.brawler2);
                    }
                    anim.SetInteger("State", state);
                    isAttacking = true;
                }
                    yield return new WaitForSeconds(time);
                    anim.speed = 1;

                if(!GetComponent<Flinch>().isReacting)
                    afterAttack = StartCoroutine(AfterAttack());
                }
            }
        }

    public IEnumerator Finisher()
    {
        GetComponent<CoroutineManager>().CancelCoroutines(finisher);
        isFinishing = true;

        anim.SetInteger("State", 50);

        yield return new WaitForSeconds(4);

        anim.SetInteger("State", 0);
    }

    public void Finish()
    {
        enemy.GetComponent<Death>().Die();
    }


    //Checks to make sure that player is close enough to face Ai to deal damage when attacking
    bool GetDistanceToEnemy()
    {
        if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < (GetComponent<Movement>().attackRange * brawlerStatsInstance.Range(gameObject)))
            {
                return true;
            }
            return false;
        }
        else
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < (GetComponent<Movement>().attackRange * brawlerStatsInstance.Range(gameObject)))
            {
                return true;
            }
            return false;
        }
    }

    bool GetDistanceToDive()
    {
        if(Vector3.Distance(transform.position, enemy.transform.position) < fightStyleManager.GetComponent<MMAStats>().mmaDiveRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetDistanceToFinish()
    {
        if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= combatStatsInstance.finisherDistance)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= combatStatsInstance.finisherDistance)
            {
                return true;
            }
            return false;
        }
    }

    //Changes the variable "shouldMove" to true, allowing the MoveWhenAttacking() to be called each frame from Update
    public void Move()
    {
        shouldMove = true;
    }

    //Changes the variable "shouldMove" to false, stopping the MoveWhenAttacking() from being called each frame from Update
    public void ShouldNotMove()
    {
        shouldMove = false;
        velocity.z = 0;
    }

    public void MoveWhenAttacking(float dis) {
        if (!GetComponent<Flinch>().isReacting)
        {
            if (tag == "Player")
            {
                velocity = transform.forward * dis;
                controller.Move(velocity * Time.deltaTime);
                return;
            }
                transform.position += transform.forward * dis * .8f * Time.deltaTime;
        }
    }

    public void ShouldHit(int a) //Causes their enemy to flinch
    {
        if (tag == "Player" && GetDistanceToEnemy() && !enemy.GetComponent<Combat>().invinsible)
        {
            if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
            {
                StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
                enemy.GetComponent<Flinch>().stun = StartCoroutine(enemy.GetComponent<Combat>().GetComponent<Flinch>().Stun(a));
            } else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
            {
                StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
                enemy.GetComponent<Health>().SubtractHealth(CalculateDamage());
            } else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
            {
                StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .1f));
                StartCoroutine(enemy.GetComponent<Health>().DamageOverTime(fightStyleManager.GetComponent<kungFuStats>().eyePokeDamage));
            } else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
            {
                StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .3f));

                enemy.GetComponent<Flinch>().bearHugged = StartCoroutine(enemy.GetComponent<Flinch>().Bearhugged(a));
                bearhugging = StartCoroutine(Bearhugging());
            } else if(anim.GetInteger("State") == 13)
            {
                StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));

                enemy.GetComponent<Flinch>().groundFlinch = StartCoroutine(enemy.GetComponent<Flinch>().GroundFlinch(CalculateDamage()));
            }
            else
            {
                if (!enemy.GetComponent<Combat>().invulnerable)
                {
                    int defend = Random.Range(1, 10);
                    int dodgeParry = Random.Range(1, 10);
                    if (!enemy.GetComponent<Combat>().canParry || !enemy.GetComponent<Dodge>().canDodge)
                    {
                        defend = 11;
                    }
                    if (defend <= combatStatsInstance.aiDefendFrequency && enemy.GetComponent<AiBehavior>().isIdle)
                    {
                        if (dodgeParry <= combatStatsInstance.aiParryDodgeFrequency && enemy.GetComponent<AiBehavior>().isIdle && enemy.GetComponent<Dodge>().canDodge)
                        {
                            //Dodge
                            enemy.GetComponent<Dodge>().isDodging = true;
                            StartCoroutine(enemy.GetComponent<Dodge>().PerformDodge());
                        }
                        else
                        {
                            enemy.GetComponent<Combat>().isParrying = true;
                            enemy.GetComponent<Combat>().secondary = 1;
                            StartCoroutine(enemy.GetComponent<AiBehavior>().IncreaseDefenseFrequency());
                            StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.2f, .5f));
                            
                            enemy.GetComponent<Flinch>().ReactionInitiation(a, 0); //Deal 0 damage when parrying
                        }
                    }
                    else
                    {
                        StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.2f, .5f));
                        if (a == 100 || a == 101) //Perform a normal flinch
                        {
                            
                            enemy.GetComponent<Flinch>().ReactionInitiation(a, CalculateDamage());
                        } else if(a == 110) //Perform knockback
                        {
                            enemy.GetComponent<Flinch>().Knockback(); 
                        } else if(a == 115) //Perform immediate knockdown
                        {
                            Debug.Log("Here");
                            enemy.GetComponent<Flinch>().ReactionInitiation(a, CalculateDamage());
                        }
                    }
                }
                else
                {
                        enemy.GetComponent<Health>().SubtractHealth(CalculateDamage());
                }
            }
        }
        if (tag == "Enemy" && !enemy.GetComponent<Combat>().invinsible)
        {
            GetComponent<AiBehavior>().canGlideToEnemy = false;
            if (Vector3.Distance(transform.position, GetComponent<AiBehavior>().enemy.transform.position) < GetComponent<AiBehavior>().attackRadius)
            {
                if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
                {
                    StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
                    enemy.GetComponent<Flinch>().stun = StartCoroutine(enemy.GetComponent<Combat>().GetComponent<Flinch>().Stun(a));
                }
                else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
                {
                    StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
                    enemy.GetComponent<Health>().SubtractHealth(CalculateDamage());
                }
                else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
                {
                    StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .1f));
                    StartCoroutine(enemy.GetComponent<Health>().DamageOverTime(fightStyleManager.GetComponent<kungFuStats>().eyePokeDamage));
                }
                else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
                {
                    StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .3f));

                    enemy.GetComponent<Flinch>().bearHugged = StartCoroutine(enemy.GetComponent<Flinch>().Bearhugged(a));
                    bearhugging = StartCoroutine(Bearhugging());
                }
                else
                {
                    if (!enemy.GetComponent<Combat>().invulnerable)
                    {
                        if (enemy.tag == "Enemy") //Attacks to enemy Ai
                        {
                            int defend = Random.Range(1, 10);
                            int dodgeParry = Random.Range(1, 10);
                            if (!enemy.GetComponent<Combat>().canParry || !enemy.GetComponent<Dodge>().canDodge)
                            {
                                defend = 11;
                            }
                            if (defend <= combatStatsInstance.aiDefendFrequency && enemy.GetComponent<AiBehavior>().isIdle)
                            {
                                if (dodgeParry <= combatStatsInstance.aiParryDodgeFrequency && enemy.GetComponent<AiBehavior>().isIdle && enemy.GetComponent<Dodge>().canDodge)
                                {
                                    //Dodge
                                    enemy.GetComponent<Dodge>().isDodging = true;
                                    StartCoroutine(enemy.GetComponent<Dodge>().PerformDodge());
                                }
                                else
                                {
                                    enemy.GetComponent<Combat>().isParrying = true;
                                    enemy.GetComponent<Combat>().secondary = 1;
                                    enemy.GetComponent<Flinch>().ReactionInitiation(a, 0); //Deal 0 damage when parrying

                                }
                                return;
                            }
                        }

                        //Attacks to player only
                        StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.2f, .5f));

                        if (a == 100 || a == 101) //Perform a normal flinch
                        {
                            enemy.GetComponent<Flinch>().ReactionInitiation(a, CalculateDamage());
                        }
                        else if (anim.GetInteger("State") == 13) //MMA ground attack
                        {
                            Debug.Log("Hit");
                            StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));

                            enemy.GetComponent<Flinch>().groundFlinch = StartCoroutine(enemy.GetComponent<Flinch>().GroundFlinch(CalculateDamage()));
                        }
                        else if (a == 110) //Perform knockdown
                        {
                            enemy.GetComponent<Flinch>().Knockback();
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Health>().SubtractHealth(CalculateDamage());
                    }
                    //Player's parry is on 'Flinch' script
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        }
    }

    public float CalculateDamage() // Calculates damage dealt after intaking all multipliers
    {
        float damage = 0;
        if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
        {
            if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
            {
                damage = fightStyleManager.GetComponent<TkdStats>().comboDamage;
            }
            else
            {
                damage = combatStatsInstance.brawler1AttackDamage;
            }
            if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate && anim.GetBool("isOffensiveStance"))
            {
                damage *= fightStyleManager.GetComponent<KarateStats>().karateDamageIncreaser;
                
            }
            if (enemy.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate && enemy.GetComponent<Combat>().anim.GetBool("isDefensiveStance"))
            {
                damage *= fightStyleManager.GetComponent<KarateStats>().karateDamageDecreaser;
            }
            if (GetComponent<Souvenirs>().hasDamageBoost)
            {
                damage *= souvenirsManagerInstance.tDamageMultiplier;
            }
        } else if(GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler2)
        {
            Debug.Log("Here");
            if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
            {
                damage = fightStyleManager.GetComponent<TkdStats>().comboDamage;
            }
            else
            {
                damage = combatStatsInstance.brawler2AttackDamage;
            }
            if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate && anim.GetBool("isOffensiveStance"))
            {
                damage *= fightStyleManager.GetComponent<KarateStats>().karateDamageIncreaser;
            }
            if (enemy.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate && enemy.GetComponent<Combat>().anim.GetBool("isDefensiveStance"))
            {
                damage *= fightStyleManager.GetComponent<KarateStats>().karateDamageDecreaser;
            }
            if (GetComponent<Souvenirs>().hasDamageBoost)
            {
                damage *= souvenirsManagerInstance.tDamageMultiplier;
            }
        }

        if (enemy.GetComponent<Souvenirs>().hasDamageReduction)
        {
            damage *= souvenirsManagerInstance.fDamageReductionPercentage;
        }

        damage *= brawlerStatsInstance.Strength(gameObject);

        if (GetComponent<Souvenirs>().hasLifeSteal)
        {
            GetComponent<Health>().AddHealth(souvenirsManagerInstance.vipcHealthRecoveryAmount);
        }

        return damage;
    }

    IEnumerator CouldNextAttack()
    {
        if (anim.GetInteger("State") != 6 && anim.GetInteger("State") != 7) //Stops player from speeding up next attack combo
        {
            canNextAttack = true;
            yield return new WaitForSeconds(1f); //Time frame after attack that new attack can be performed //Needs variable
            canNextAttack = false;
        }
        else //If at the end attack combo, forces another brief pause between attack combos
        {
            attackBuffering = true;
            StartCoroutine(AttackBufferTime());
        }
    }

    IEnumerator AttackBufferTime() //Brief pause between attack combos
    {
        yield return new WaitForSeconds(.75f); //Needs variable
        attackBuffering = false;
    }

    public IEnumerator AfterAttack() //Times that it takes for Ai to recover after attacking
    {
        GetComponent<CoroutineManager>().CancelCoroutines(afterAttack);
        anim.SetBool("canTransition", true);
        int i = Random.Range(1, 10);
            ShouldNotMove();
            if ((anim.GetInteger("State") != 3 && anim.GetInteger("State") != 4 && anim.GetInteger("State") != 5) || enemy.GetComponent<Flinch>().isSurrendering || tag == "Tourist") { i = 100; }
            if (i <= combatStatsInstance.aiContinuedAttackFrequency)
            {
                isAttacking = false;
                int variedAttack;
                if(basicAttack != null)
                {
                    StopCoroutine(basicAttack);
                }
                if (anim.GetInteger("State") == 3) //Leads to V1 & V2 of next attack
                {
                    variedAttack = Random.Range(4, 6);
                    if (variedAttack == 4)
                    {
                        basicAttack = StartCoroutine(Attack(variedAttack, combatStatsInstance.brawler2SecondAAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                    }
                    else
                    {
                    basicAttack = StartCoroutine(Attack(variedAttack, combatStatsInstance.brawler2SecondBAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                    }

                }
                else if(anim.GetInteger("State") == 4)
                {
                    variedAttack = 6;
                    anim.GetComponent<Animator>().SetInteger("Variation", Random.Range(1, 3));
                    basicAttack = StartCoroutine(Attack(variedAttack, combatStatsInstance.brawler2ThirdAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                }
                else if(anim.GetInteger("State") == 5)
                {
                    variedAttack = 7;
                    anim.GetComponent<Animator>().SetInteger("Variation", Random.Range(1, 3));
                    basicAttack = StartCoroutine(Attack(variedAttack, combatStatsInstance.brawler2ThirdAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                }             
            }
            else
            {
                if (tag == "Enemy")
                {
                    if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
                    {
                        if (anim.GetBool("isAttackStance")) //Sets AI to passive stance
                        {
                            int j = Random.Range(0, 10);
                            if (j < fightStyleManager.GetComponent<KarateStats>().aiStanceChangeFrequency)
                            {
                                anim.SetBool("isAttackStance", false);
                            }
                        }
                    }
                    GetComponent<AiBehavior>().AssignIdle();
                    yield return new WaitForSeconds(.15f);
                    faceEnemy = true;
                    faceHead = false;
                    isAttacking = false;
                    //canUseTechnique = true;
                    canAttack = true;
                    GetComponent<AiBehavior>().isChasingEnemy = true;
                Debug.Log("Hey");
            } else if (tag == "Tourist")
                {
                    GetComponent<Tourist>().AssignIdle();
                    isAttacking = false;
                    yield return new WaitForSeconds(3f);
                    GetComponent<Tourist>().isChargingEnemy = true;
                }
            }
    }  

    void Emergency()
    {
        if(isAttacking && anim.GetInteger("State") == 2)
        {
            isAttacking = false;
            anim.SetInteger("State", 0);
        }
        if(GetComponent<AiBehavior>().canGlideToEnemy && anim.GetInteger("State") == 0)
        {
            GetComponent<AiBehavior>().canGlideToEnemy = false;
        }

    }

    /*
    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, GetComponent<Movement>().attackRange * brawlerStatsInstance.Range(gameObject));
    
    }
    */
}
