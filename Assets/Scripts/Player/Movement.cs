using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    [Header("Refences")]
    public CharacterController controller;
    public Animator anim;
    public Transform cam;
    GameObject gameManager;
    GameObject souvenirManager;
    IdManagear idManagerInstance;
    GameObject fightStyleManager;

    [Header("Movement")]
    public Vector3 velocity;
    float turnSmoothVelocity;
    public Vector3 direction;

    [Header("Stats")]
    public float turnSmoothTime = .1f;
    public float jogSpeed = 9;
    public float runSpeed = 14;
    public float attackRange = 5;
    public float pickUpRange = 5;
    float isRunning;

    [Header("Ground")]
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask; 

    [Header("Feet Grounder")]

    #region LegVariables
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
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        souvenirManager = GameObject.Find("Souvenir Manager");
        fightStyleManager = GameObject.Find("Fight Style Manager");
    }

    void Update()
    {
        if (Time.frameCount < 100)
        {
            GetComponent<CapsuleCollider>().enabled = true;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Checks if grounded
        if (isGrounded && velocity.y < 0) //Resets Velocity and keeps brawler "glued" to the floor
        {
            velocity.y = -2f;
        }

        GetComponent<BoxCollider>().enabled = true;

        if(!UniversalFight.fight || PauseMenu.gamePaused) { return; }

        if (GetComponent<Combat>().inCombat) { return; }
        if (GetComponent<Combat>().isAttacking) { return; }
        if (GetComponent<Flinch>().isParried) { return; }
        if (GetComponent<Combat>().isParrying) { return; }
        if(GetComponent<Combat>().isBlocking) { return; }
        if (GetComponent<Combat>().isGuardBreaking) { return; }
        if (GetComponent<Throw>().isAiming) { return; }
        if (GetComponent<Flinch>().isFlinching == true) { return; }
        if (GetComponent<Flinch>().isKnockedBack == true) { return; }
        if (GetComponent<Flinch>().isKnockedDown == true) { return; }
        if (GetComponent<Flinch>().isStunned == true) { return; }
        if (GetComponent<Flinch>().isDove == true) { return; }
        if (GetComponent<Flinch>().isReacting == true) { return; }
        if (GetComponent<Dodge>().isDodging == true) { return; }
        if(GetComponent<Combat>().isDiving) { return; }
        if (GetComponent<Combat>().isGroundAttacking) { return; }



        if (!controller.enabled) { return; }

        if(idManagerInstance.gameMode == IdManagear.mode.AiVsAi)
        {
            return;
        }

        

        velocity.y += -9.8f * Time.deltaTime; //Gravity
        if (GetComponent<Flinch>().isFlinching == false && GetComponent<Flinch>().isStunned == false)
        {
            controller.Move(velocity * Time.deltaTime); //Controls vertical movement, like falling
        }

        PlayerMovement();
    }

    public void PlayerMovement()
    {
        anim.speed = 1;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            if (Input.GetAxis("Secondary") == 0)
            {
                isRunning = Input.GetAxis("Primary"); //Causes player to run when secondary isn't down and primary is down
            }
            else
            {
                isRunning = 0;
            }
            anim.speed = 1.1f;
            if (GetComponent<Souvenirs>().hasSpeedBoost)
            {
                anim.speed *= souvenirManager.GetComponent<SouvenirsManager>().speedMultiplier;
                controller.Move(moveDir.normalized * runSpeed * souvenirManager.GetComponent<SouvenirsManager>().speedMultiplier * Time.deltaTime);
            }
            else
            {
                controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
            }
            anim.SetInteger("State", 2);
           /* if (isRunning == 0)
            {
                controller.Move(moveDir.normalized * jogSpeed * GetComponent<Combat>().movementSpeedMultiplier * Time.deltaTime);
                anim.speed = GetComponent<Combat>().movementSpeedMultiplier;
                anim.SetInteger("State", 1);
            }
            else
            {
                controller.Move(moveDir.normalized * runSpeed * GetComponent<Combat>().movementSpeedMultiplier * Time.deltaTime);
                anim.speed = GetComponent<Combat>().movementSpeedMultiplier;
                anim.SetInteger("State", 2);
            } */
        }
        else
        {
            if (!GetComponent<Throw>().isEquipped)
            {
                anim.SetInteger("State", 0);
            }
            else
            {
                anim.SetInteger("State", 40);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #region FeetGrounding

    /// <summary>
    /// We are updating the AdjustFeetTarget method and also finding the position of each foot in our Solver Position.
    /// </summary>
    private void FixedUpdate()
    {
        if (enableFeetIk == false) { return; }
        if(anim == null) { return; }
        AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        //Find and raycast to the ground
        FeetPositionSolver(rightFootPosition, ref rightFootIkPosition, ref rightFootIkRotation); //Handles the solver for the right foot
        FeetPositionSolver(leftFootPosition, ref leftFootIkPosition, ref leftFootIkRotation); //Handles the solver for the left foot

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (enableFeetIk == false) { return; }
        if(anim == null) { return; }

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
    void MoveFeetToIk (AvatarIKGoal foot, Vector3 posIkHolder, Quaternion rotIkHolder, ref float lastFootPosY)
    {
        Vector3 targetIkPos = anim.GetIKPosition(foot);

        if(posIkHolder != Vector3.zero)
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
        if(rightFootIkPosition == Vector3.zero || leftFootIkPosition == Vector3.zero || lastLeftFootPosY == 0)
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
    private void AdjustFeetTarget(ref  Vector3 feetPos, HumanBodyBones foot)
    {
        feetPos = anim.GetBoneTransform(foot).position;
        feetPos.y = transform.position.y + heightFromGroundRaycast;
    }

    #endregion
}
