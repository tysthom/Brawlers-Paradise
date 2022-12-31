using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tourist : MonoBehaviour
{
    [Header("Refrences")]
    public Animator anim;
    public LayerMask groundMask;
    GameObject gameManager;
    GameObject combatManagear;
    public GameObject companion;
    public GameObject enemy;
    public NavMeshAgent agent;
    SouvenirsManager souvenirsManagerInstance;

    [Header("Stats")]
    float baseSpeed;
    public float attackRange = 1.5f; //Range where Ai will run when charging and attacking
    public float attackRadius = 3; //Range where Ai must be in order for attack to count as a hit

    [Header("Status")]
    public bool isPunchingBag; //Idle
    public bool isIdle; //True when Ai is Idle/Neutral
    public bool canAttack; //True when Ai is allowed to attack
    public bool isWaitingToAttack; //True when Ai not on cooldown to attack. False when Ai can't attack to stop animation cancelling
    public bool isChargingEnemy;// Trye when Ai is running to attack palyer
    public bool canGlideToEnemy; //True when Ai is gliding to player when attacking


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
        combatManagear = GameObject.Find("Combat Manager");
        gameManager = GameObject.Find("Game Manager");
        agent = GetComponent<NavMeshAgent>();
        baseSpeed = agent.speed;
        canAttack = true;
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        if (isPunchingBag == false)
        {
            agent.stoppingDistance = attackRange;
            //MoveToAttack(attackRange);
        }
    }

    public IEnumerator CompanionAssigned(GameObject compan)
    {
        companion = compan;
        enemy = companion.GetComponent<Combat>().enemy;
        GetComponent<Combat>().enemy = enemy;
        FaceEnemy();
        yield return new WaitForSeconds(2);
        isChargingEnemy = true;
    }

    void Update()
    {
        if(companion == null)
        {
            return;
        }

            GetComponent<CapsuleCollider>().enabled = true;

        if (isPunchingBag)
        {
            GetComponent<Combat>().faceEnemy = true;
            FaceEnemy();
            return;
        }

        if (!isChargingEnemy && !GetComponent<Combat>().isAttacking)
        {
            if (!GetComponent<Combat>().isAttacking && !GetComponent<Combat>().isParrying && !GetComponent<Flinch>().isParried &&
                !GetComponent<Flinch>().isFlinching && !GetComponent<Flinch>().isStunned && !GetComponent<Combat>().isGuardBreaking)
            {
                isIdle = true;
                FaceEnemy();
            }
            else { isIdle = false; }
        }
        else { isIdle = false; }

        if (!GetComponent<Flinch>().isFlinching && GetComponent<Combat>().faceEnemy) //Makes Ai face player before they get hit
        {
            FaceEnemy();
        }

        if (GetComponent<Flinch>().isReacting) //Moves Ai backwards when they get hit
        {
            agent.isStopped = true;
        }

        if (!GetComponent<Combat>().isAttacking) //Makes Ai preform main sequence
        {
                agent.speed = baseSpeed;
                anim.speed = 1;

            if (isChargingEnemy == true)
            {
                MoveToAttack(attackRange);
            }

            if (isWaitingToAttack)
            {
                StartCoroutine(WaitToAttack());
            }
        }
        else
        {
            isChargingEnemy = false;
        }
    }

    void FaceEnemy()
    {
        Quaternion rotTarget = Quaternion.LookRotation(enemy.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, 800 * Time.deltaTime);
    }

    public void AssignIdle()
    {
            anim.SetInteger("State", 0);
    }

    IEnumerator StartTimer()
    {
        if (canAttack)
        {
            canAttack = false;
            isWaitingToAttack = false;
            yield return new WaitForSeconds(Random.Range(2.5f, 3.75f));
            canAttack = true;
        }
    }

    public IEnumerator WaitToAttack() //The time it takes the Ai to wait from a distance & charge towards player/throable
    { 

            if (canAttack)
            {
                StartCoroutine(StartTimer());

                if (!GetComponent<Flinch>().isFlinching && !GetComponent<Flinch>().isStunned)
                {
                        MoveToAttack(attackRange);
            }
            else
            {
                yield return null;
            }
            }
    }

    public void MoveToAttack(float dis) //Ai charges towards player and assigns attack variation
    {
            agent.isStopped = false;
            if ((Vector3.Distance(transform.position, enemy.transform.position) > attackRange + 1))
            {
            
                isChargingEnemy = true;
                anim.SetInteger("State", 2);
                agent.SetDestination(enemy.transform.position);
            }
            else
            {
                agent.isStopped = true;
                isChargingEnemy = false;
                    // MAKES AI ATTACK
                    int a = Random.Range(30, 32);
            if (a == 30)
                    {
                        GetComponent<Combat>().basicAttack = StartCoroutine(GetComponent<Combat>().Attack(a, combatManagear.GetComponent<CombatStats>().brawler2FirstAttackTime));
                    }
                    else if (a == 31)
                    {
                        GetComponent<Combat>().basicAttack = StartCoroutine(GetComponent<Combat>().Attack(a, combatManagear.GetComponent<CombatStats>().brawler2SecondAAttackTime));
                    }
                    else if (a == 32)
                    {
                        GetComponent<Combat>().basicAttack = StartCoroutine(GetComponent<Combat>().Attack(a, combatManagear.GetComponent<CombatStats>().brawler2SecondBAttackTime));
                    }
                    else if (a == 33)
                    {
                        GetComponent<Combat>().basicAttack = StartCoroutine(GetComponent<Combat>().Attack(a, combatManagear.GetComponent<CombatStats>().brawler2SecondBAttackTime));
                    }         
            }
    }

    private void OnDrawGizmosSelected()
    {
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
