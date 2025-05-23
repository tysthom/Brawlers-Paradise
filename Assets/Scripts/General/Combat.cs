using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject hudManager;
    SouvenirsManager souvenirsManagerInstance;
    IdManagear idManagerInstance;
    ResultStats resultStatsInstance;
    BrawlerStats brawlerStatsInstance;
    Difficulty difficultyInstance;
    GameObject particleManager;
    FightingSoundEffects fSEInstance;

    [Header("General Status")]
    public bool winner;
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
    public bool isCounterAttackBuffering = false; //True if is counter attacking
    public bool canFinish; //True if can finish opponent
    public bool isFinishing; //True if is finishing opponent
    public bool canBlock; //True if can block
    public bool isParrying; //True if is parrying
    public bool isBlocking; //True if is blocking
    public bool isBlockBuffering; //True if buffering after finishing blocking
    public bool isParryBuffering; //True if buffering after finishing parrying
    public float primary, secondary; //Trigger values
    bool secondaryLifted; //True if secondary is not pushed down
    public bool canUseTechnique; //True when Technique can be used

    [Header("Karate Status")]
    public bool stanceCooldown; //True if can not switch stance after just switching

    [Header("Boxing Status")]
    public bool isGuardBreaking; //True if is performing guard break
    public bool guardBreakerComplete = true; //False when guardbreaking or when the time is still going to perform that attack

    [Header("MMA Status")]
    public bool shouldDive; //Determines if should be moving while diving
    public bool isDiving; //True if performing diving attack ONLY
    public bool isGroundIdle; //True if Idle while on top of dove opponent
    public bool isGroundAttacking; //True if either groundIdle or performing ground attacks

    [Header("TKD Status")]
    public bool isStretching; //True if currently stretching
    public bool isStretchBuffering; //True if buffering just after finishing parrying

    [Header("Kung Fu Status")]
    public bool isEyePoking; //True if performing Eye Poke attack

    [Header("Pro Wrestling Status")]
    public bool isBearhugGrabbing; //True if performing grabbing portion of Bear Hug attack
    public bool isBearhugging; //True if hugging opponent
    public int bearhugBreakOutCount = 0; //Current amount of break out meter

    [Header("Coroutine")]
    public Coroutine parry;
    public Coroutine block;
    public Coroutine counterAttack;
    public Coroutine guardBreaker; //Once assigned, starts GuardBreaker(). If cancelled, stops the script and doesn't force brawler back to idle like usual
    public Coroutine diveAttack;
    public Coroutine groundAttack;
    public Coroutine stretch;
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
        difficultyInstance = gameManager.GetComponent<Difficulty>();
        hudManager = GameObject.Find("HUD Manager");
        particleManager = GameObject.Find("Particle Manager");
        fSEInstance = GetComponent<FightingSoundEffects>();

        if (tag == "Player") {
            controller = GetComponent<CharacterController>(); controller.enabled = true;
            enemy = idManagerInstance.brawler2;
        }
        if (tag == "Enemy") {
            faceEnemy = true; GetComponent<CapsuleCollider>().enabled = true;
            if (idManagerInstance.brawler1 == gameObject)
            {
                enemy = idManagerInstance.brawler2;
            }
            else if (idManagerInstance.brawler2 == gameObject)
            {
                enemy = idManagerInstance.brawler1;
            }
        }
        foreach (Transform child in enemy.transform)
        {
            if (child.gameObject.name == "Head Position")
            {
                enemyHead = child.gameObject;
            }
        }
        GetComponent<Death>().enemy = enemy;
        anim = GetComponent<Animator>();
        canAttack = true;
        canBlock = true;
        canUseTechnique = true;

        if(tag == "Enemy")
        {
            if(difficultyInstance.difficultyMode == Difficulty.difficulty.easy)
            {
                combatStatsInstance.aiContinuedAttackFrequency = 7;
                combatStatsInstance.aiThrowableFrequency = 2;
                combatStatsInstance.aiDefendFrequency = 4;
            } 
            else if(difficultyInstance.difficultyMode == Difficulty.difficulty.medium)
            {
                combatStatsInstance.aiContinuedAttackFrequency = 9;
                combatStatsInstance.aiThrowableFrequency = 1;
                combatStatsInstance.aiDefendFrequency = 5;
            }
            else if (difficultyInstance.difficultyMode == Difficulty.difficulty.hard)
            {
                combatStatsInstance.aiContinuedAttackFrequency = 10;
                combatStatsInstance.aiThrowableFrequency = 1;
                combatStatsInstance.aiDefendFrequency = 6;
            }
        }
    }

    void Update()
    {
        if (!UniversalFight.fight || PauseMenu.gamePaused || BackMenu.gamePaused || winner) { return; }

        if (tag == "Tourist")
        {
            if (GetComponent<Tourist>().companion == null) { return; }
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
                  GetComponent<Stamina>().stamina >= combatStatsInstance.staminaDodgeBlockCost)
        {
            canUseTechnique = true;
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo && canAttack && !GetComponent<Flinch>().isReacting &&
                  !isStretching && !isStretchBuffering)
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

        if (inCombat || (GetComponent<Flinch>().isReacting && (!GetComponent<Flinch>().isBlockedBack && !GetComponent<Flinch>().isFlinchBuffering)) 
            || isBlockBuffering || GetComponent<Combat>().enemy.GetComponent<Death>().dead)
        {
            canBlock = false;
            if(tag == "Player")
            {
                isBlocking = false;
            }
        }
        else
        {
            canBlock = true;
        }

        if (!inCombat && !isParrying && !isCounterAttackBuffering && !GetComponent<Flinch>().isReacting && !GetComponent<Throw>().isAiming 
            && !attackBuffering && !isBlockBuffering && !GetComponent<Dodge>().isDodging && !GetComponent<Dodge>().isDodgeBuffering 
            && !enemy.GetComponent<Death>().dead || isGroundIdle)
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

            if (!inCombat && !isBlocking && (!GetComponent<Flinch>().isReacting || GetComponent<Flinch>().isStunned || GetComponent<Flinch>().isSurrendering) 
                && !GetComponent<Throw>().isAiming && !GetComponent<Flinch>().isBeingFinished || GetComponent<Flinch>().isDove)
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

            canFinish = GetDistanceToFinish() && enemy.GetComponent<Flinch>().isSurrendering && !isAttacking;

            if (attackButtonDown && (canAttack || canNextAttack) && !attackBuffering && !GetComponent<Throw>().isEquipped) //ATTACK
            {
                if (!canFinish) //Stops player from continuing basic attack combo
                {
                    if (!enemy.GetComponent<Flinch>().isSurrendering)
                    {
                        if (isGroundIdle)
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
                            else if (anim.GetInteger("State") == 6 && canNextAttack == true)
                            {
                                basicAttack = StartCoroutine(Attack(8, combatStatsInstance.brawler1ForthAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                            }
                            else if (anim.GetInteger("State") == 7 && canNextAttack == true)
                            {
                                basicAttack = StartCoroutine(Attack(9, combatStatsInstance.brawler1ForthAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                            }
                            fSEInstance.AttackGrunt();
                        }
                    }
                } else
                {
                    finisher = StartCoroutine(Finisher());
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
            if (GetComponent<AiBehavior>().canGlideToEnemy && Vector3.Distance(transform.position, enemy.transform.position) > GetComponent<AiBehavior>().attackRadius)
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
                if (canBlock)
                {
                    if (tag == "Enemy") //Stops Ai from continuously blocking
                    {
                        secondary = 0;
                        
                        if(!isBlocking)
                            block = StartCoroutine(Block());
                    }
                    else
                    {

                        isBlocking = true;
                        anim.SetInteger("State", 20);
                    }
                }
            }
            else
            {
                isBlocking = false;
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
        else 
        { 
            if (GetComponent<Throw>()) { 
                GetComponent<Throw>().isAiming = false; 
            } 
            if (secondaryLifted == false) secondaryLifted = true;

            if (isBlocking)
            {
                isBlocking = false;
                if(tag == "Player")
                StartCoroutine(BlockBuffer());
            }
        }

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
                else if (anim.GetInteger("State") == 8)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler1ForthAttackV1Distance);
                } 
                else if (anim.GetInteger("State") == 9)
                {
                    MoveWhenAttacking(combatStatsInstance.brawler1ForthAttackV2Distance);
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
                else if (anim.GetInteger("State") == 8)
                {
                    if (anim.GetInteger("Variation") == 1)
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ForthAttackV1Distance);
                    }
                    else
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ForthAttackV2Distance);
                    }
                }
                else if (anim.GetInteger("State") == 9)
                {
                    if (anim.GetInteger("Variation") == 1)
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ForthAttackV1Distance);
                    }
                    else
                    {
                        MoveWhenAttacking(combatStatsInstance.brawler2ForthAttackV2Distance);
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

        if (isAttacking || isGuardBreaking || isCounterAttacking || isGuardBreaking || isDiving || isGroundIdle || isGroundAttacking 
            || isStretching || isStretchBuffering || isEyePoking || isBearhugGrabbing || isBearhugging || isFinishing)
        {
            inCombat = true;
        }
        else
        {
            inCombat = false;
        }

        if (shouldDive)
        {
            if (GetDistanceToDive() && !enemy.GetComponent<Combat>().invinsible) //Lands on enemy
            {
                fSEInstance.DiveSound();
                combatManagear.GetComponent<AttackStatusManager>().Dive(GetComponent<Combat>().gameObject);
                isDiving = false;
                if (tag == "Enemy")
                {
                    faceEnemy = false;
                }
                    faceHead = true;
                    if(GetComponent<Flinch>().reactionTime != null)
                {
                    StopCoroutine(GetComponent<Flinch>().reactionTime);
                }
                    enemy.GetComponent<Flinch>().ReactionInitiation(115, 0);
                
                    anim.SetInteger("State", 12);
                    isGroundIdle = true;
                    groundAttack = StartCoroutine(GroundAttack());
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
                    afterAttack = StartCoroutine(AfterAttack());
                }
               enemy.GetComponent<Flinch>().recovery = StartCoroutine(enemy.GetComponent<Flinch>().Recovery());
            }
        }
    }

    IEnumerator BlockBuffer()
    {
        isBlockBuffering = true;
        yield return new WaitForSeconds(.25f); //Prevents player from attacking immediaetly
        isBlockBuffering = false;
    }

    IEnumerator Block()
    {
        isBlocking = true;
        faceEnemy = true;
        anim.SetInteger("State", 20);
        yield return new WaitForSeconds(.2f);
        
        anim.SetInteger("State", 0);
        isBlocking = false;
        if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; 
            GetComponent<AiBehavior>().AssignIdle(); GetComponent<AiBehavior>().isChasingEnemy = true; }
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
    }

    public IEnumerator GuardBreaker()
    {
        if (!inCombat && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Flinch>().isParried && guardBreakerComplete)
        {   ShouldNotMove();
            canUseTechnique = false;
            guardBreakerComplete = false;
            isGuardBreaking = true;
            anim.SetInteger("State", 10);
            GetComponent<Flinch>().canBeCounterHit = true;
            GetComponent<Flinch>().canBePunishHit = false;

            resultStatsInstance.TechniqueUsage(gameObject);


            GetComponent<CoroutineManager>().CancelCoroutines(guardBreaker);

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
            yield return new WaitForSeconds(.2f);
            fSEInstance.StrainGrunt();
            yield return new WaitForSeconds(combatStatsInstance.guardBreakerTime-.2f);
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
        GetComponent<Flinch>().canBeCounterHit = true;
        GetComponent<Flinch>().canBePunishHit = false;

        resultStatsInstance.TechniqueUsage(gameObject);

        if (tag == "Enemy")
        {
            GetComponent<AiBehavior>().agent.isStopped = true;
        }
        yield return new WaitForSeconds(fightStyleManager.GetComponent<MMAStats>().mmaDiveTime);
        isDiving = false;
        invinsible = false;

        afterAttack = StartCoroutine(AfterAttack());
    }

    public void Dive()
    {
        shouldDive = true;
    }

    public void ShouldNotDive()
    {
        shouldDive = false;
        velocity.z = 0;
    }

    public IEnumerator GroundAttack()
    {
        GetComponent<CoroutineManager>().CancelCoroutines(groundAttack);

        canAttack = false;
        isGroundIdle = false;
        isGroundAttacking = true;
        invinsible = false;
        shouldDive = false;
        GetComponent<Flinch>().canBeCounterHit = true;
        GetComponent<Flinch>().canBePunishHit = false;

        yield return new WaitForSeconds(.1f); //Allows Brawler to go to Ground Idle animation before attacking

        int i = Random.Range(1, 3);
        GetComponent<Stamina>().SubtractStamina(fightStyleManager.GetComponent<MMAStats>().mmaGroundAttackStaminaCost);
        anim.SetInteger("Variation", i);
        anim.SetInteger("State", 13);

        yield return new WaitForSeconds(fightStyleManager.GetComponent<MMAStats>().mmaGroundAttackTime);
        
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

    public IEnumerator Stretch()
    {
        if (!inCombat && !GetComponent<Flinch>().isReacting)
        {
            
            GetComponent<CoroutineManager>().CancelCoroutines(stretch);

            ShouldNotMove();
            canUseTechnique = false;
            isStretching = true;
            resultStatsInstance.TechniqueUsage(gameObject);

            if (tag == "Enemy")
            { 
                faceEnemy = true;
                GetComponent<AiBehavior>().agent.isStopped = true;
            }

            anim.GetComponent<Animator>().SetInteger("Variation", Random.Range(1, 3));
            anim.SetInteger("State", 10);
            
            yield return new WaitForSeconds(fightStyleManager.GetComponent<TkdStats>().stretchParryWindow);

            isStretching = false;
            isStretchBuffering = true;

            if (enemy.GetComponent<Flinch>().isReacting)
            {
                yield return new WaitForSeconds(fightStyleManager.GetComponent<TkdStats>().stretchSuccessTime);
            }
            else
            {
                yield return new WaitForSeconds(fightStyleManager.GetComponent<TkdStats>().stretchMissTime);
            }
            
            if (tag == "Player")
            {
                GetComponent<Movement>().PlayerMovement();
            }
            else
            {
                yield return new WaitForSeconds(.05f);
                anim.SetInteger("State", 0);
                afterAttack = StartCoroutine(AfterAttack());
            }
            isStretchBuffering = false;
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
            resultStatsInstance.TechniqueUsage(gameObject);
            GetComponent<Flinch>().canBeCounterHit = true;
            GetComponent<Flinch>().canBePunishHit = false;

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
            yield return new WaitForSeconds(.5f);
            fSEInstance.StrainGrunt();
            yield return new WaitForSeconds(fightStyleManager.GetComponent<kungFuStats>().eyePokeTime-.5f);
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
            GetComponent<CoroutineManager>().CancelCoroutines(bearhugGrab);

            ShouldNotMove();
            canUseTechnique = false;
            isBearhugGrabbing = true;
            anim.SetInteger("State", 10);
            resultStatsInstance.TechniqueUsage(gameObject);
            GetComponent<Flinch>().canBeCounterHit = true;
            GetComponent<Flinch>().canBePunishHit = false;

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

        isBearhugging = true;
        anim.SetInteger("State", 11);
        yield return new WaitForSeconds(10);
    }

    public IEnumerator BearHugBreakOut()
    { 
        if (tag == "Player")
        {
            bearhugBreakOutCount += Random.Range(fightStyleManager.GetComponent<ProWrestlingStats>().minBreakOutAmount,
                fightStyleManager.GetComponent<ProWrestlingStats>().maxBreakOutAmount); 
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
            GetComponent<Flinch>().parried = StartCoroutine(GetComponent<Flinch>().Parried(true));
            enemy.GetComponent<Flinch>().parried = StartCoroutine(enemy.GetComponent<Flinch>().Parried(true));
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
                GetComponent<Flinch>().canBeCounterHit = true;
                GetComponent<Flinch>().canBePunishHit = false;

                resultStatsInstance.BasicTotalAttacks(idManagerInstance.brawler1);
                anim.SetInteger("State", state);
                isAttacking = true;
                canNextAttack = false;
                faceEnemy = true;
                yield return new WaitForSeconds(time);
                anim.speed = 1;
                StartCoroutine(CouldNextAttack());
                yield return new WaitForSeconds(.5f); //Time that brawler waits before they can move
                    if (state == anim.GetInteger("State")) //Checks to see if the animator has moved to the next attack animation
                    { //If not, returns player to normal movement
                        isAttacking = false;
                        faceEnemy = false;
                    GetComponent<Flinch>().canBePunishHit = false;
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
                        resultStatsInstance.BasicTotalAttacks(idManagerInstance.brawler1);
                    }
                    else if (idManagerInstance.brawler2 == gameObject)
                    {
                        resultStatsInstance.BasicTotalAttacks(idManagerInstance.brawler2);
                    }

                    GetComponent<Flinch>().canBeCounterHit = true;
                    GetComponent<Flinch>().canBePunishHit = false;
                    anim.SetInteger("State", state);
                    isAttacking = true;
                    GetComponent<AiBehavior>().canGlideToEnemy = true;
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
        enemy.GetComponent<Flinch>().isBeingFinished = true;
        if (tag == "Player")
        {
            hudManager.GetComponent<HUDManager>().finisherPrompt.SetActive(false);
            enemy.GetComponent<Combat>().faceEnemy = true;
        }
        
        hudManager.GetComponent<HUDManager>().finisherText.text = "";

        anim.SetInteger("State", 50);   

        yield return new WaitForSeconds(4);

        anim.SetInteger("State", 0);
    }

    public void Finish()
    {
        gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
        gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .2f));
        fSEInstance.Finisher();
        StartCoroutine(enemy.GetComponent<Death>().Die());
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
        GetComponent<Flinch>().canBeCounterHit = false;
        GetComponent<Flinch>().canBePunishHit = true;
        if (tag == "Player" && GetDistanceToEnemy() && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Combat>().invinsible)
        {
            if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
            {
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
                fSEInstance.Uppercut();
                enemy.GetComponent<Flinch>().stun = StartCoroutine(enemy.GetComponent<Combat>().GetComponent<Flinch>().Stun(a));
            }
            else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
            {
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .1f));
                combatManagear.GetComponent<AttackStatusManager>().Poke(GetComponent<Combat>().gameObject);

                fSEInstance.EyePoke();
                StartCoroutine(enemy.GetComponent<Health>().DamageOverTime(fightStyleManager.GetComponent<kungFuStats>().eyePokeDamage, 
                    fightStyleManager.GetComponent<kungFuStats>().eyePokeTickRepeats));
            } else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
            {
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .3f));
                combatManagear.GetComponent<AttackStatusManager>().BearHug(GetComponent<Combat>().gameObject);

                fSEInstance.BearHug();
                enemy.GetComponent<Flinch>().bearHugged = StartCoroutine(enemy.GetComponent<Flinch>().Bearhugged(a));
                bearhugging = StartCoroutine(Bearhugging());
            } else if(anim.GetInteger("State") == 13)
            {
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));

                fSEInstance.BasicAttackConnectionSound();
                enemy.GetComponent<Flinch>().groundFlinch = StartCoroutine(enemy.GetComponent<Flinch>().GroundFlinch(CalculateDamage()));
            }
            else
            {
                #region RESULT STATS
                if (idManagerInstance.brawler1 == gameObject) //RESULT STATS
                {
                    resultStatsInstance.BasicAttacksLanded(idManagerInstance.brawler1);
                }
                else if (idManagerInstance.brawler2 == gameObject)
                {
                    resultStatsInstance.BasicAttacksLanded(idManagerInstance.brawler2);
                }
                #endregion

                if (!enemy.GetComponent<Combat>().invulnerable)
                {
                    int enemyDefend = Random.Range(1, 10);
                    int dodgeParry = Random.Range(1, 10);
                    if (!enemy.GetComponent<Combat>().canBlock && !enemy.GetComponent<Dodge>().canDodge)
                    {
                        enemyDefend = 11;
                    }

                    if (enemyDefend <= combatStatsInstance.aiDefendFrequency && enemy.GetComponent<AiBehavior>().isIdle)
                    {
                        int enemyTkdTech = Random.Range(1, 10);
                        if (enemy.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo && 
                            enemyTkdTech <= fightStyleManager.GetComponent<TkdStats>().aiStretchFrequency)
                        {
                            fSEInstance.Block();
                            enemy.GetComponent<Combat>().stretch = StartCoroutine(enemy.GetComponent<Combat>().Stretch());
                            GetComponent<Flinch>().parried = StartCoroutine(GetComponent<Flinch>().Parried(false));
                        } 
                        else if (dodgeParry <= combatStatsInstance.aiBlockDodgeFrequency && enemy.GetComponent<AiBehavior>().isIdle && enemy.GetComponent<Dodge>().canDodge)
                        {
                            //Dodge
                            enemy.GetComponent<Dodge>().isDodging = true;
                            StartCoroutine(enemy.GetComponent<Dodge>().PerformDodge());
                        }
                        else
                        {
                            //Block
                            enemy.GetComponent<Combat>().secondary = 1;
                            StartCoroutine(enemy.GetComponent<AiBehavior>().IncreaseDefenseFrequency());
                            gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                            gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .5f));
                            enemy.GetComponent<Throw>().isEquipped = false;
                            fSEInstance.Block();
                            enemy.GetComponent<Flinch>().ReactionInitiation(-20, CalculateDamage());
                        }
                    }
                    else
                    {
                        gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                        gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.2f, .5f));
                        if (a == 100 || a == 101) //Perform a normal flinch
                        {
                            fSEInstance.BasicAttackConnectionSound();
                            enemy.GetComponent<Flinch>().ReactionInitiation(a, CalculateDamage());
                        } else if(a == 110) //Perform knockback
                        {
                            fSEInstance.BasicAttackConnectionSound();
                            enemy.GetComponent<Flinch>().Knockback(); 
                        } else if(a == 115) //Perform immediate knockdown
                        {
                            enemy.GetComponent<Flinch>().ReactionInitiation(a, CalculateDamage());
                        }
                    }
                }
                else
                {
                        enemy.GetComponent<Health>().SubtractHealth(CalculateDamage()); //Subtracts armor
                }
            }
        }
        if (tag == "Enemy" && !GetComponent<Flinch>().isReacting && !enemy.GetComponent<Combat>().invinsible)
        {
            GetComponent<AiBehavior>().canGlideToEnemy = false;
            if (Vector3.Distance(transform.position, GetComponent<AiBehavior>().enemy.transform.position) < GetComponent<AiBehavior>().attackRadius)
            {
                if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
                {
                    gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                    gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));
                    fSEInstance.Uppercut();
                    enemy.GetComponent<Flinch>().stun = StartCoroutine(enemy.GetComponent<Combat>().GetComponent<Flinch>().Stun(a));
                }
                else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
                {
                    gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                    gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .1f));
                    combatManagear.GetComponent<AttackStatusManager>().Poke(GetComponent<Combat>().gameObject);

                    fSEInstance.EyePoke();
                    StartCoroutine(enemy.GetComponent<Health>().DamageOverTime(fightStyleManager.GetComponent<kungFuStats>().eyePokeDamage,
                        fightStyleManager.GetComponent<kungFuStats>().eyePokeTickRepeats));
                }
                else if (anim.GetInteger("State") == 10 && GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
                {
                    gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                    gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .3f));
                    combatManagear.GetComponent<AttackStatusManager>().BearHug(GetComponent<Combat>().gameObject);

                    fSEInstance.BearHug();
                    enemy.GetComponent<Flinch>().bearHugged = StartCoroutine(enemy.GetComponent<Flinch>().Bearhugged(a));
                    bearhugging = StartCoroutine(Bearhugging());
                }
                else
                {
                    #region RESULT STATS
                    if (idManagerInstance.brawler1 == gameObject) //RESULT STATS
                    {
                        resultStatsInstance.BasicAttacksLanded(idManagerInstance.brawler1);
                    }
                    else if (idManagerInstance.brawler2 == gameObject)
                    {
                        resultStatsInstance.BasicAttacksLanded(idManagerInstance.brawler2);
                    }
                    #endregion

                    if (!enemy.GetComponent<Combat>().invulnerable)
                    {
                        if (enemy.tag == "Enemy") //Attacks to enemy Ai
                        {
                            int defend = Random.Range(1, 10);
                            int dodgeParry = Random.Range(1, 10);
                            if (!enemy.GetComponent<Combat>().canBlock || !enemy.GetComponent<Dodge>().canDodge)
                            {
                                defend = 11;
                            }
                            if (defend <= combatStatsInstance.aiDefendFrequency && enemy.GetComponent<AiBehavior>().isIdle)
                            {
                                if (dodgeParry <= combatStatsInstance.aiBlockDodgeFrequency && enemy.GetComponent<AiBehavior>().isIdle && enemy.GetComponent<Dodge>().canDodge)
                                {
                                    //Dodge
                                    enemy.GetComponent<Dodge>().isDodging = true;
                                    StartCoroutine(enemy.GetComponent<Dodge>().PerformDodge());
                                }
                                else
                                {
                                    enemy.GetComponent<Combat>().isParrying = true;
                                    enemy.GetComponent<Combat>().secondary = 1;
                                    enemy.GetComponent<Throw>().isEquipped = false;
                                    enemy.GetComponent<Flinch>().ReactionInitiation(a, 0); //Deal 0 damage when parrying

                                }
                                return;
                            }
                        }

                        //Attacks to player only
                        GetComponent<AiBehavior>().canGlideToEnemy = false;
                        gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                        gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.2f, .5f));

                        if (a == 100 || a == 101) //Perform a normal flinch
                        {
                            if(enemy.GetComponent<Combat>().isBlocking)
                            {
                                fSEInstance.Block();
                                enemy.GetComponent<Flinch>().ReactionInitiation(-20, CalculateDamage()); //-20 = BlockedBack
                            }
                            else if (enemy.GetComponent<Combat>().isStretching)
                            {
                                fSEInstance.Block();
                                GetComponent<Flinch>().parried = StartCoroutine(GetComponent<Flinch>().Parried(false));
                            } 
                            else
                            {
                                fSEInstance.BasicAttackConnectionSound();
                                enemy.GetComponent<Flinch>().ReactionInitiation(a, CalculateDamage());
                            }       
                        }
                        else if (anim.GetInteger("State") == 13) //MMA ground attack
                        {
                            gameManager.GetComponent<Vibrations>().vibrateCoroutine = null;
                            gameManager.GetComponent<Vibrations>().vibrateCoroutine = StartCoroutine(gameManager.GetComponent<Vibrations>().Vibrate(.1f, .25f));

                            fSEInstance.BasicAttackConnectionSound();
                            enemy.GetComponent<Flinch>().groundFlinch = StartCoroutine(enemy.GetComponent<Flinch>().GroundFlinch(CalculateDamage()));
                        }
                        else if (a == 110) //Perform knockdown
                        {
                            if (enemy.GetComponent<Combat>().isBlocking)
                            {
                                fSEInstance.Block();
                                enemy.GetComponent<Flinch>().ReactionInitiation(-20, CalculateDamage()); //-20 = BlockedBack
                            }
                            else if (enemy.GetComponent<Combat>().isStretching)
                            {
                                GetComponent<Flinch>().parried = StartCoroutine(GetComponent<Flinch>().Parried(false));
                            }
                            else
                            {
                                fSEInstance.BasicAttackConnectionSound();
                                enemy.GetComponent<Flinch>().Knockback();
                            }                         
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
                StartCoroutine(DecreaseAttackFrequency());
            }
        }
    }

    IEnumerator DecreaseAttackFrequency()
    {
        int holder = combatStatsInstance.aiContinuedAttackFrequency;
        combatStatsInstance.aiContinuedAttackFrequency = 0;
        yield return new WaitForSeconds(2);
        combatStatsInstance.aiContinuedAttackFrequency = holder;
    }

    public float CalculateDamage() // Calculates damage dealt after intaking all multipliers
    {
        float damage = 0;
        if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
        {

            damage = combatStatsInstance.brawler1AttackDamage;

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
            damage = combatStatsInstance.brawler2AttackDamage;

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
        if (anim.GetInteger("State") != 8 && anim.GetInteger("State") != 9) //Stops player from speeding up next attack combo
        {
            canNextAttack = true;
            yield return new WaitForSeconds(.5f); //Time frame after attack that new attack can be performed //Needs variable
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
        GetComponent<Flinch>().canBePunishHit = false;
        anim.SetBool("canTransition", true);
        int i = Random.Range(1, 11);
        ShouldNotMove();
        if ((anim.GetInteger("State") != 3 && anim.GetInteger("State") != 4 && anim.GetInteger("State") != 5 && 
        anim.GetInteger("State") != 6 && anim.GetInteger("State") != 7) || enemy.GetComponent<Flinch>().isSurrendering || tag == "Tourist") 
            { i = 100; }

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
            else if(anim.GetInteger("State") == 6)
            {
                variedAttack = 8;
                anim.GetComponent<Animator>().SetInteger("Variation", Random.Range(1, 3));
                basicAttack = StartCoroutine(Attack(variedAttack, combatStatsInstance.brawler2ForthAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
            }
            else if(anim.GetInteger("State") == 7)
            {
                variedAttack = 9;
                anim.GetComponent<Animator>().SetInteger("Variation", Random.Range(1, 3));
                basicAttack = StartCoroutine(Attack(variedAttack, combatStatsInstance.brawler2ForthAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
            }
            fSEInstance.AttackGrunt();
        }
        else
        {
            if (tag == "Enemy")
            {
                if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
                {
                    if (anim.GetBool("isOffensiveStance")) //Sets AI to defensive/passive stance
                    {
                        int j = Random.Range(0, 10);
                        if (j < fightStyleManager.GetComponent<KarateStats>().aiStanceChangeFrequency)
                        {
                            anim.SetBool("isOffensiveStance", false);
                            int s = Random.Range(1, 4);
                            if (s < 3)
                            {
                                anim.SetBool("isDefensiveStance", true);
                                anim.SetBool("isPassiveStance", false);

                                particleManager.GetComponent<ParticleManager>().KarateDefensiveParticles(gameObject);
                            }
                            else
                            {
                                anim.SetBool("isDefensiveStance", false);
                                anim.SetBool("isPassiveStance", true);

                                particleManager.GetComponent<ParticleManager>().KaratePassiveParticles(gameObject);
                            }
                        }
                    }
                }
                GetComponent<AiBehavior>().AssignIdle();
                
                if (difficultyInstance.difficultyMode == Difficulty.difficulty.easy)
                {
                    yield return new WaitForSeconds(.25f);
                }
                else if (difficultyInstance.difficultyMode == Difficulty.difficulty.medium)
                {
                    yield return new WaitForSeconds(.15f);
                }
                else if (difficultyInstance.difficultyMode == Difficulty.difficulty.hard)
                {
                    yield return new WaitForSeconds(.05f);
                }
                faceEnemy = true;
                faceHead = false;
                isAttacking = false;
                canAttack = true;
                GetComponent<AiBehavior>().isChasingEnemy = true;
            } 
            else if (tag == "Tourist")
            {
                GetComponent<Tourist>().AssignIdle();
                isAttacking = false;
                yield return new WaitForSeconds(3f);
                GetComponent<Tourist>().isChargingEnemy = true;
            }
        }
    }  

    public IEnumerator Winner()
    {
        winner = true;
        faceEnemy = false;

        anim.SetInteger("State", 1000);

        UniversalFight.fight = false;
        gameManager.GetComponent<IdManagear>().playerCamera.enabled = false;
        gameManager.GetComponent<IdManagear>().spectatorCamera.enabled = false;
        gameManager.GetComponent<IdManagear>().winnerCamera.enabled = true;

        transform.position = GameObject.Find("Winner Position").transform.position;
        transform.rotation = Quaternion.Euler(0,0,0);

        fSEInstance.Victory();

        yield return new WaitForSeconds(1);

        anim.SetInteger("State", 0);
    }

    public void ShowboatComplete()
    {
        StartCoroutine(gameManager.GetComponent<PostGame>().ShowboatCompleteCoroutine());
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
}
