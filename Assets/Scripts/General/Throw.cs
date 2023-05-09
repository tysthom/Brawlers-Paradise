using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    GameObject throwableHolder;
    public GameObject closestThrowable;
    public GameObject currentThrowable;
    public GameObject handHold;
    public GameObject ballLaunchPosition;
    public GameObject pickupIcon;
    Collider[] throwablesInRange;
    bool throwableIsInRange;
    public bool hasThrowable;
    public bool isAiming, isThrowing;
    public bool isEquipped;
    float lastX, lastY;

    Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        throwableHolder = GameObject.Find("Throwables");
    }

    void Start()
    {

    }

    void Update()
    {
        if(GetComponent<Flinch>().isStunned && hasThrowable && isEquipped)
        {
            Equiping(false);
        }

        if (tag == "Player")
        {
            float DPady = Input.GetAxis("DPad Vertical");
            throwablesInRange = Physics.OverlapSphere(transform.position, GetComponent<Movement>().pickUpRange);
            for (int i = 0; i < throwablesInRange.Length; i++)
            {
                if (throwablesInRange[i].tag == "Throwable")
                {
                    closestThrowable = throwablesInRange[i].gameObject;
                }
            }

            if (closestThrowable != null && closestThrowable.tag == "Throwable" && currentThrowable == null 
                && Vector3.Distance(transform.position, closestThrowable.transform.position) < GetComponent<Movement>().pickUpRange
                && !GetComponent<Combat>().isFinishing)
            {
                pickupIcon.SetActive(true);
                throwableIsInRange = true;
            }
            else
            {
                pickupIcon.SetActive(false);
                throwableIsInRange = false;
            }

            bool pickUpButtonDown = Input.GetButton("Pick Up");
            if (pickUpButtonDown && throwableIsInRange)
            {
                if (!GetComponent<Combat>().isParrying && !GetComponent<Combat>().isAttacking && !GetComponent<Flinch>().isFlinching &&
                            !GetComponent<Flinch>().isStunned && !GetComponent<Combat>().isGuardBreaking)
                {
                    PickUp();
                }
            }

            if (hasThrowable)
            {
                currentThrowable.transform.position = handHold.transform.position;
                if (!GetComponent<Combat>().isParrying && !GetComponent<Combat>().isAttacking && !GetComponent<Flinch>().isFlinching &&
                    !GetComponent<Flinch>().isStunned && !GetComponent<Combat>().isGuardBreaking)
                {
                    if (lastY != DPady)
                        if (DPady == -1)
                        {
                            Equiping(!isEquipped);
                        }
                }
            }
            else
            {
                isEquipped = false;
            }

            lastY = DPady;
        }
        else
        {
            if (hasThrowable)
            {
                currentThrowable.transform.position = handHold.transform.position;
            }   
        }
    }

    public bool AISearchForThrowable()
    {
        bool foundThrowable = false;
        throwablesInRange = Physics.OverlapSphere(transform.position, 5000);

        for (int i = 0; i < throwablesInRange.Length; i++)
        {
            if (throwablesInRange[i].tag == "Throwable")
            {
                foundThrowable = true;
                if (closestThrowable == null)
                {
                    closestThrowable = throwablesInRange[i].gameObject;
                }
                else if(Vector3.Distance(transform.position, throwablesInRange[i].transform.position) < Vector3.Distance(transform.position, closestThrowable.transform.position))
                {
                    closestThrowable = throwablesInRange[i].gameObject;
                }
            }
        }
        if (!foundThrowable)
        {
            closestThrowable = null;
            return false;
        }
        else
        {
            return true;
        }
    }

     public void PickUp()
    {
        hasThrowable = true;
        isEquipped = true;
        currentThrowable = closestThrowable;
        currentThrowable.GetComponent<Rigidbody>().useGravity = false;
        currentThrowable.GetComponent<Rigidbody>().isKinematic = true;
        currentThrowable.transform.parent = transform;
        currentThrowable.tag = tag;
        currentThrowable.GetComponent<Throwable>().holder = gameObject;
        GetComponent<Combat>().isBlocking = false;

        if(currentThrowable.GetComponent<BoxCollider>() != null)
        {
            currentThrowable.GetComponent<BoxCollider>().enabled = false;
        }
        if (currentThrowable.GetComponent<SphereCollider>() != null)
        {
            currentThrowable.GetComponent<SphereCollider>().enabled = false;
        }
        if (tag == "Enemy")
        {
            GetComponent<AiBehavior>().isLookingForThrowable = false;
            GetComponent<AiBehavior>().isChasingEnemy = true;
            GetComponent<Combat>().faceEnemy = true;
            Equiping(false);
        }
        currentThrowable.GetComponent<Throwable>().previousTag = gameObject.tag;
    }

    public void Equiping(bool state)
    {
        if(tag == "Player")
        {
            isEquipped = state;
        } else if(tag == "Enemy")
        {
            isEquipped = state;
        }
        if (isEquipped)
        {
            currentThrowable.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            currentThrowable.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void AimObject()
    {
        anim.SetInteger("State", 41);
        isAiming = true;
    }
     
    public void ThrowObject()
    {
        anim.SetInteger("State", 42);
        isThrowing = true;
    }

    public void Release()
    {
        hasThrowable = false;
        isEquipped = false;
        if (currentThrowable.GetComponent<BoxCollider>() != null)
        {
            currentThrowable.GetComponent<BoxCollider>().enabled = true;
        }
        currentThrowable.GetComponent<Rigidbody>().useGravity = true;
        if (currentThrowable.GetComponent<SphereCollider>() != null)
        {
            currentThrowable.GetComponent<SphereCollider>().enabled = true;
        } 
        if(currentThrowable.GetComponent<BoxCollider>() != null)
        {
            currentThrowable.GetComponent<BoxCollider>().enabled = true;
        }
        if (currentThrowable.GetComponent<MeshCollider>() != null)
        {
            currentThrowable.GetComponent<MeshCollider>().enabled = true;
        }
        currentThrowable.GetComponent<Rigidbody>().isKinematic = false;
        currentThrowable.transform.position = ballLaunchPosition.transform.position;
        currentThrowable.transform.parent = throwableHolder.transform;
        currentThrowable.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
        currentThrowable = null;
        closestThrowable = null;

        if(GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.briefcase && !GetComponent<Souvenirs>().onCooldown)
        {
            GetComponent<Souvenirs>().canUseSouvenir = true;
        }
        
    }
}
