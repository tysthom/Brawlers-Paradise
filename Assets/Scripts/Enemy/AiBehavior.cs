using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehavior : MonoBehaviour
{
    [Header("Refrences")]
    public Animator anim;
    public LayerMask groundMask;
    GameObject gameManager;
    GameObject combatManagear;
    public GameObject enemy;
    public NavMeshAgent agent;
    GameObject souvenirsManager;
    SouvenirsManager souvenirsManagerInstance;
    IdManagear idManagerInstance;
    BrawlerStats brawlerStatsInstance;
    GameObject fightStyleManager;

    [Header("Stats")]
    float baseSpeed;
    public float enemyRange = 10; //Range where Ai will run when chasing player
    public float attackRange = 1.5f; //Range where Ai will run when charging and attacking
    public float attackRadius = 3; //Range where Ai must be in order for attack to count as a hit
    public float glideAmount = 1; //How far Ai can glide when attacking;
    public float pickUpRange = 5; //Range where Ai will be able to pick up a throwable
    public float throwableSearchrange = 500; //Range where Ai can search for a throwable
    int baseDefense = 0;

    [Header("Status")]   
    public bool isPunchingBag; //Idle
    public bool isIdle; //True when Ai is Idle/Neutral
    public bool isChasingEnemy; //True when Ai is closing th distance to the player
    public bool isWaitingToAttack; //True when Ai not on cooldown to attack. False when Ai can't attack to stop animation cancelling
    public bool wTACalled; //True when WaitToAttack() has been called to prevent it from continually being called, and initiaiting attacks w/o rest periods
    public bool isChargingEnemy;// True when Ai is running to attack palyer
    public bool isLookingForThrowable; //True when Ai is doesn't have throwable and is instead looking for one
    public bool canGlideToEnemy; //True when Ai is gliding to player when attacking

    [Header("Coroutine")]
    public Coroutine waitToAttack;


    #region LegVariables
    [Header("Feet Grounder")]
    public bool enableFeetIk = true;
    [Range(0, 2)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private float pelvisOffset = 0;
    [Range(0, 1)] [SerializeField] private float pelvisSpeed = 0.228f;
    [Range(0, 1)] [SerializeField] private float feetToIkSpeed = .5f;
    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";
    public bool useProIkFeature = false;
    public bool showSolverDebug = true;
    private Vector3 rightFootPosition, leftFootPosition, rightFootIkPosition, leftFootIkPosition;
    private Quaternion leftFootIkRotation, rightFootIkRotation;
    private float lastPelvisPositionY, lastRightFootPosY, lastLeftFootPosY;
    #endregion 
    private void Awake()
    {
        souvenirsManager = GameObject.Find("Souvenir Manager");
        souvenirsManagerInstance = souvenirsManager.GetComponent<SouvenirsManager>();
        combatManagear = GameObject.Find("Combat Manager");
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        brawlerStatsInstance = combatManagear.GetComponent<BrawlerStats>();
        fightStyleManager = GameObject.Find("Fight Style Manager");
        agent = GetComponent<NavMeshAgent>();
        baseSpeed = agent.speed;
        baseDefense = combatManagear.GetComponent<CombatStats>().aiDefendFrequency;
    }
    void Start()
    {
        #region Brawler ID Assignment
        if (idManagerInstance.gameMode == IdManagear.mode.playerVsAi)
        {

        }
        if(GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
        {
            enemy = idManagerInstance.brawler2;
        }
        else
        {
            enemy = idManagerInstance.brawler1;
        }
        #endregion

        if (!isPunchingBag)
        {
            agent.stoppingDistance = attackRange;
            isChargingEnemy = true;
        }

        

    }

    void Update()
    {
        GetComponent<BoxCollider>().enabled = true;

        if (isPunchingBag)
        {
            if (!GetComponent<Flinch>().isDove)
            {
                GetComponent<Combat>().faceEnemy = true;
            }
            if(!GetComponent<Flinch>().isStunned)
                return; 
        }

        if (!isChargingEnemy && !isChasingEnemy && !isLookingForThrowable)
        {
            if(!GetComponent<Combat>().isAttacking  && !GetComponent<Combat>().isParrying && !GetComponent<Flinch>().isParried &&
                !GetComponent<Flinch>().isFlinching && !GetComponent<Flinch>().isStunned && !GetComponent<Combat>().isGuardBreaking)
            {
                isIdle = true; //Assigned isIdle to true after everything is checked and nothing is happening
            }
            else { isIdle = false; }
        }
        else { isIdle = false; }

        if (GetComponent<Flinch>().isReacting) //Helps moves Ai backwards when they get hit
        {
            agent.isStopped = true;
        }

        if (!GetComponent<Combat>().inCombat) //Makes Ai preform main sequence
        {
            if (GetComponent<Souvenirs>().hasSpeedBoost)
            {
                agent.speed = baseSpeed* souvenirsManagerInstance.speedMultiplier;
                anim.speed = 1*souvenirsManagerInstance.GetComponent<SouvenirsManager>().speedMultiplier;
            }
            else
            {
                agent.speed = baseSpeed;
                anim.speed = 1;
            }

            if (isChasingEnemy == true) //Constantly called to update destiantion based on enemy's location
            {
                    MoveTo(enemyRange, enemy);
            }

            if (isChargingEnemy == true) //Constantly called to update destiantion based on enemy's location
            {
                    MoveToAttack(enemy);
            }

            if (isWaitingToAttack && !wTACalled && !GetComponent<Combat>().inCombat) //Called once to initaiate attack wait time ONCE
            {
                //AssignIdle();
                waitToAttack = StartCoroutine(WaitToAttack());
            }

            if(isLookingForThrowable)
            {
                if (GetComponent<Throw>().AISearchForThrowable())
                {
                    GetComponent<Combat>().faceEnemy = false;
                    MoveTo(pickUpRange, GetComponent<Throw>().closestThrowable);
                }
                else
                {
                    isChasingEnemy = true;
                }
            }

            if (GetComponent<Throw>().isAiming)
            {
                anim.SetInteger("State", 41);   
            }
        }
        else
        {
            isChargingEnemy = false;
            isChasingEnemy = false;
        }
    }

    public void AssignIdle()
    {
        if(GetComponent<Throw>().hasThrowable && GetComponent<Throw>().isEquipped)
        {
            anim.SetInteger("State", 40);
        }
        else
        {
            anim.SetInteger("State", 0);
        }
    }

    public void MoveTo(float dis, GameObject destination) // Moves Ai to player/throwable pick-up range
    {
        if (!GetComponent<Flinch>().isStunned && !GetComponent<Flinch>().isReacting 
            && !GetComponent<Dodge>().isDodging)
        {
            agent.isStopped = false;
            if ((Vector3.Distance(transform.position, destination.transform.position) >= dis))
            {
                anim.SetInteger("State", 2);
                agent.SetDestination(destination.transform.position);  
            }
            else
            {
                AssignIdle();
                if (destination.tag != "Throwable")
                {
                    agent.isStopped = true;
                    isChasingEnemy = false;
                    isWaitingToAttack = true;
                }
                if(destination.tag == "Throwable")
                {
                    GetComponent<Throw>().PickUp();
                }
            }
        }
    }
     
    public IEnumerator WaitToAttack() //The time it takes the Ai to wait from a distance & charge towards player/throable
    {
        GetComponent<CoroutineManager>().CancelCoroutines(waitToAttack);

        if (!GetComponent<Flinch>().isReacting && !GetComponent<Dodge>().isDodging)
        {
            #region Souvenirs
            int a = Random.Range(1, 10);
            if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.medicine && 
                GetComponent<Health>().health < GetComponent<Health>().maxHealth*.5f && GetComponent<Souvenirs>().canUseSouvenir
                && a <= souvenirsManagerInstance.medicineUsage)
            {
                GetComponent<Souvenirs>().ActivateSouvenir(); //MEDICINE
            }
            if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.sunscreen &&
                GetComponent<Health>().health < GetComponent<Health>().maxHealth *.75f && GetComponent<Souvenirs>().canUseSouvenir
                && a <= souvenirsManagerInstance.sunscreenUsage)
            {
                GetComponent<Souvenirs>().ActivateSouvenir(); //SUNSCREEN
            }
            if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.lifeJacket &&
                GetComponent<Health>().health < GetComponent<Health>().maxHealth * .1f && GetComponent<Souvenirs>().canUseSouvenir
                && a <= souvenirsManagerInstance.lifeJacketUsage)
            {
                GetComponent<Souvenirs>().ActivateSouvenir(); //LIFE JACKET
            }
            if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.ratPoison && GetComponent<Souvenirs>().canUseSouvenir
                && a <= souvenirsManagerInstance.ratPoisonUsage)
            {
                GetComponent<Souvenirs>().ActivateSouvenir(); //RAT POISON
            }
            if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.tequila && GetComponent<Souvenirs>().canUseSouvenir
                && a <= souvenirsManagerInstance.tequilaUsage)
            {
                GetComponent<Souvenirs>().ActivateSouvenir(); //TEQUILA
            }
            #endregion

            if (GetComponent<Combat>().canAttack)
            {
                wTACalled = true;
                yield return new WaitForSeconds(Random.Range(.5f, 1.75f)); //Time Ai has to wait before attacking
                GetComponent<Throw>().isThrowing = false; //Possible fix

                if (!GetComponent<Flinch>().isReacting && !GetComponent<Dodge>().isDodging)
                {
                    isWaitingToAttack = false;
                    int i = Random.Range(1, 10);
                    if (i <= combatManagear.GetComponent<CombatStats>().aiThrowableFrequency && !enemy.GetComponent<Flinch>().isSurrendering) //Performs throwing attack instead of basic attack
                    {
                        int b = Random.Range(1, 10);
                        if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.briefcase && GetComponent<Souvenirs>().canUseSouvenir
                       && b <= souvenirsManagerInstance.briefcaseUsage)
                        {
                            GetComponent<Souvenirs>().ActivateSouvenir(); //BRIEFCASE
                            yield return null;
                        }

                        yield return new WaitForSeconds(.25f);
                        if (!GetComponent<Throw>().hasThrowable) //Causes AI to look for throwable
                        {
                            GetComponent<AiBehavior>().isLookingForThrowable = true;
                        }
                        else
                        {
                            if (!GetComponent<Combat>().inCombat)
                            {
                                GetComponent<Throw>().Equiping(true);
                                GetComponent<Combat>().canBlock = false;
                                AssignIdle();
                                yield return new WaitForSeconds(.1f);

                                GetComponent<Throw>().isAiming = true;
                                GetComponent<Throw>().AimObject();
                                yield return new WaitForSeconds(.5f);

                                if (!GetComponent<Flinch>().isReacting && !GetComponent<Combat>().inCombat 
                                    && !GetComponent<Dodge>().isDodging)
                                {
                                    GetComponent<Throw>().ThrowObject();
                                    yield return new WaitForSeconds(.5f);
                                    GetComponent<Combat>().afterAttack = StartCoroutine(GetComponent<Combat>().AfterAttack());
                                }
                            }
                        }
                    }
                    else
                    {
                        int j = Random.Range(1, 10);
                        if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA && !enemy.GetComponent<Flinch>().isSurrendering &&
                                j <= fightStyleManager.GetComponent<MMAStats>().aiDiveFrequency &&
                                !enemy.GetComponent<Flinch>().isStunned && GetComponent<Combat>().canUseTechnique)
                        {
                                GetComponent<Combat>().diveAttack = StartCoroutine(GetComponent<Combat>().DiveAttack());
                        }
                        else
                        {

                            if (GetComponent<Throw>().currentThrowable != null)
                            {
                                GetComponent<Throw>().Equiping(false); //Unequips throwable
                            }
                            int c = Random.Range(1, 10);
                            if (GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.coffee &&
                             GetComponent<Souvenirs>().canUseSouvenir && c <= souvenirsManagerInstance.coffeeUsage)
                            {
                                GetComponent<Souvenirs>().ActivateSouvenir(); //COFFEE
                            }
                            wTACalled = false;
                            MoveToAttack(enemy);
                        }
                    }
                }
                else
                { //Error happens when Ai tries to attack while being downed, thus causing them to freeze
                    wTACalled = false;
                    waitToAttack = StartCoroutine(WaitToAttack());
                    Debug.Log("Can't Delete");
                }
            }
        }
        wTACalled = false;
        isChasingEnemy = true;
    }

    public void MoveToAttack(GameObject destination) //Ai charges towards player and assigns attack variation
    {
        if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            if (!anim.GetBool("isOffensiveStance")) //Sets AI to attack stance
            {
                int i = Random.Range(0, 10);
                if (i < fightStyleManager.GetComponent<KarateStats>().aiStanceChangeFrequency)
                {
                    anim.SetBool("isOffensiveStance", true);
                    anim.SetBool("isDefensiveStance", false);
                    anim.SetBool("isPassiveStance", false);
                }
            }
        }

        if (!GetComponent<Flinch>().isReacting && !GetComponent<Dodge>().isDodging)
        {
            agent.isStopped = false;
            if (Vector3.Distance(transform.position, destination.transform.position) > attackRange + 1)
            {
                isChargingEnemy = true;
                anim.SetInteger("State", 2);
                agent.SetDestination(destination.transform.position);
            }
            else
            {
                agent.isStopped = true;
                isChargingEnemy = false;
                int i = Random.Range(1, 10);
                if(GetComponent<Combat>().GetDistanceToFinish() && enemy.GetComponent<Flinch>().isSurrendering)
                {
                    GetComponent<Combat>().finisher = StartCoroutine(GetComponent<Combat>().Finisher());
                }
                else 
                if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing &&
                    i <= fightStyleManager.GetComponent<BoxingStats>().aiGuardbreakerFrequency && !destination.GetComponent<Combat>().isGuardBreaking && 
                    !enemy.GetComponent<Flinch>().isStunned && !GetComponent<Flinch>().isReacting && !GetComponent<Dodge>().isDodging)
                {
                        GetComponent<Combat>().guardBreaker = StartCoroutine(GetComponent<Combat>().GuardBreaker());
                } else
                if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu &&
                   i <= fightStyleManager.GetComponent<kungFuStats>().aiEyePokeFrequency &&
                   !enemy.GetComponent<Flinch>().isStunned)
                {
                    if (!GetComponent<Flinch>().isReacting && !GetComponent<Dodge>().isDodging)
                    {
                        GetComponent<Combat>().eyePoke = StartCoroutine(GetComponent<Combat>().EyePoke());
                    }
                } else if(GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling &&
                    i <= fightStyleManager.GetComponent<ProWrestlingStats>().aiBearhugFrequency && !destination.GetComponent<Combat>().isBearhugging &&
                    !enemy.GetComponent<Flinch>().isStunned && GetComponent<Combat>().canUseTechnique)
                {
                    GetComponent<Combat>().bearhugGrab = StartCoroutine(GetComponent<Combat>().BearhugGrab());
                } else
                {
                    // MAKES AI ATTACK
                    if (GetComponent<Throw>().currentThrowable != null)
                    {
                        GetComponent<Throw>().Equiping(false);
                    }
                    anim.speed = 2 - brawlerStatsInstance.AttackSpeed(gameObject);
                    GetComponent<Combat>().basicAttack = StartCoroutine(GetComponent<Combat>().Attack(3, combatManagear.GetComponent<CombatStats>().brawler2FirstAttackTime * brawlerStatsInstance.AttackSpeed(gameObject)));
                }
            }
        }
        else
        {
            Debug.Log("Can't Attack!");
        }
    }

    public IEnumerator IncreaseDefenseFrequency()
    {
        if (combatManagear.GetComponent<CombatStats>().aiDefendFrequency < 10)
        {
            combatManagear.GetComponent<CombatStats>().aiDefendFrequency++;

            yield return new WaitForSeconds(1);

            combatManagear.GetComponent<CombatStats>().aiDefendFrequency--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5000);
    }

    #region FeetGrounding

    /// <summary>
    /// We are updating the AdjustFeetTarget method and also finding the position of each foot in our Solver Position.
    /// </summary>
    private void FixedUpdate()
    {
        if (enableFeetIk == false) { return; }
        if (anim == null) { return; }
        AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        //Find and raycast to the ground
        FeetPositionSolver(rightFootPosition, ref rightFootIkPosition, ref rightFootIkRotation); //Handles the solver for the right foot
        FeetPositionSolver(leftFootPosition, ref leftFootIkPosition, ref leftFootIkRotation); //Handles the solver for the left foot

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (enableFeetIk == false) { return; }
        if (anim == null) { return; }

        MovePelvisHeight();
        //Right foot Ik position and rotation -- utilise the pro feature in here
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        if (useProIkFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        }

        MoveFeetToIk(AvatarIKGoal.RightFoot, rightFootIkPosition, rightFootIkRotation, ref lastRightFootPosY);


        //Left foot Ik position and rotation -- utilise the pro feature in here
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        if (useProIkFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        }

        MoveFeetToIk(AvatarIKGoal.LeftFoot, leftFootIkPosition, leftFootIkRotation, ref lastLeftFootPosY);
    }

    #endregion

    #region FeetGroundingMethods

    /// <summary>
    /// Moves feet to Ik position
    /// </summary>
    /// <param name="foot"></param>
    /// <param name="posIkHolder"></param>
    /// <param name="rotIkHolder"></param>
    /// <param name="lastFootPosY"></param>
    void MoveFeetToIk(AvatarIKGoal foot, Vector3 posIkHolder, Quaternion rotIkHolder, ref float lastFootPosY)
    {
        Vector3 targetIkPos = anim.GetIKPosition(foot);

        if (posIkHolder != Vector3.zero)
        {
            targetIkPos = transform.InverseTransformPoint(targetIkPos);
            posIkHolder = transform.InverseTransformPoint((posIkHolder));

            float yVar = Mathf.Lerp(lastFootPosY, posIkHolder.y, feetToIkSpeed);
            targetIkPos.y += yVar;

            lastFootPosY = yVar;

            targetIkPos = transform.TransformPoint(targetIkPos);

            anim.SetIKRotation(foot, rotIkHolder);
        }

        anim.SetIKPosition(foot, targetIkPos);
    }


    /// <summary>
    /// Moves the height of the pelvis
    /// </summary>
    private void MovePelvisHeight()
    {
        if (rightFootIkPosition == Vector3.zero || leftFootIkPosition == Vector3.zero || lastLeftFootPosY == 0)
        {
            lastPelvisPositionY = anim.bodyPosition.y;
            return;
        }

        float lOffsetPos = leftFootIkPosition.y - transform.position.y;
        float rOffsetPos = rightFootIkPosition.y - transform.position.y;

        float totalOffset = (lOffsetPos < rOffsetPos) ? lOffsetPos : rOffsetPos;

        Vector3 newPelvisPos = anim.bodyPosition + Vector3.up * totalOffset;

        newPelvisPos.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPos.y, pelvisSpeed);

        anim.bodyPosition = newPelvisPos;

        lastPelvisPositionY = anim.bodyPosition.y;
    }

    /// <summary>
    /// We are locating the feet position via a Raycast and then Solving
    /// </summary>
    /// <param name="skyPos"></param>
    /// <param name="feetIkPos"></param>
    /// <param name="feetIkRot"></param>
    private void FeetPositionSolver(Vector3 skyPos, ref Vector3 feetIkPos, ref Quaternion feetIkRot)
    {
        //Raycast handling section
        RaycastHit feetOutHit;

        if (showSolverDebug) { Debug.DrawLine(skyPos, skyPos + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.red); }

        if (Physics.Raycast(skyPos, Vector3.down, out feetOutHit, raycastDownDistance + heightFromGroundRaycast, groundMask))
        {
            //Finding our feet Ik positions from the sky position
            feetIkPos = skyPos;
            feetIkPos.y = feetOutHit.point.y + pelvisOffset;
            feetIkRot = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;

            return;
        }

        feetIkPos = Vector3.zero; //It didn't work :(

    }


    /// <summary>
    /// Adjust feet target
    /// </summary>
    /// <param name="feetPos"></param>
    /// <param name="foot"></param>
    private void AdjustFeetTarget(ref Vector3 feetPos, HumanBodyBones foot)
    {
        feetPos = anim.GetBoneTransform(foot).position;
        feetPos.y = transform.position.y + heightFromGroundRaycast;
    }

    #endregion
}
