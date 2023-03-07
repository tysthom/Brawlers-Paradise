using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public GameObject combatManagear;
    Animator anim;
    Combat combatInstance;
    Flinch flinchInstance;
    CombatStats combatStatsInstance;
    public bool canDodge;
    public bool isDodging;
    public bool isColliding;

    private void Awake()
    {
        combatManagear = GameObject.Find("Combat Manager");
        combatStatsInstance = combatManagear.GetComponent<CombatStats>();
        anim = GetComponent<Animator>();
        combatInstance = GetComponent<Combat>();
        flinchInstance = GetComponent<Flinch>();
    }

    // Update is called once per frame
    void Update()
    {
        if(combatInstance.inCombat || flinchInstance.isReacting || combatInstance.isParrying || GetComponent<Throw>().isEquipped ||
            isDodging || GetComponent<Stamina>().stamina < combatStatsInstance.playerDodgeMinAmount ||
            GetComponent<Combat>().enemy.GetComponent<Flinch>().isParried)
        {
            canDodge = false;
        }
        else
        {
            canDodge = true;
        }

        if (isDodging) //Moves brawler backwards
        {
            if (tag == "Player")
            {
                if (anim.GetInteger("State") == 26)
                {
                    transform.position += transform.forward * combatStatsInstance.playerDodgeDistance * Time.deltaTime;
                }
                else
                {
                    transform.position -= transform.forward * combatStatsInstance.playerDodgeDistance * Time.deltaTime;
                }
            }
            else
            {
                transform.position -= transform.forward * combatStatsInstance.playerDodgeDistance * Time.deltaTime;
            }
        } 
    }

    public IEnumerator PerformDodge()
    {
        isDodging = true;
        combatInstance.canAttack = false;
        combatInstance.canBlock = false;
        combatInstance.invinsible = true;
        if (tag == "Player")
        {
            if (GetComponent<Movement>().direction.magnitude >= .1)
            {
                combatInstance.faceEnemy = false;
                anim.SetInteger("State", 26);
            }
            else
            {
                combatInstance.faceEnemy = true;
                anim.SetInteger("State", 25);
            }
        }
        else
        {
            if (GetComponent<AiBehavior>().waitToAttack != null)
            {
                StopCoroutine(GetComponent<AiBehavior>().waitToAttack);
                GetComponent<AiBehavior>().wTACalled = false;
            }
            anim.SetInteger("State", 25);
            combatInstance.faceEnemy = true;
        }

        #region Coroutines
        if (tag == "Enemy")
        {
            if (GetComponent<AiBehavior>().waitToAttack != null)
            {
                StopCoroutine(GetComponent<AiBehavior>().waitToAttack);
                GetComponent<AiBehavior>().isWaitingToAttack = false;
            }
        }
        if (GetComponent<Combat>().parry != null)
        {
            StopCoroutine(GetComponent<Combat>().parry);
        }
        if (GetComponent<Combat>().guardBreaker != null)
        {
            StopCoroutine(GetComponent<Combat>().guardBreaker);
            GetComponent<Combat>().guardBreakerComplete = true;
        }
        if (GetComponent<Combat>().diveAttack != null)
        {
            StopCoroutine(GetComponent<Combat>().diveAttack);
        }
        if (GetComponent<Combat>().groundAttack != null)
        {
            StopCoroutine(GetComponent<Combat>().groundAttack);
        }
        if (GetComponent<Combat>().counterAttack != null)
        {
            StopCoroutine(GetComponent<Combat>().counterAttack);
        }
        if (GetComponent<Combat>().basicAttack != null)
        {
            StopCoroutine(GetComponent<Combat>().basicAttack);
        }
        if (GetComponent<Combat>().afterAttack != null)
        {
            StopCoroutine(GetComponent<Combat>().afterAttack);
        }
        if (GetComponent<Flinch>().reactionTime != null)
        {
            StopCoroutine(GetComponent<Flinch>().reactionTime);
        }
        if (GetComponent<Flinch>().recovery != null)
        {
            StopCoroutine(GetComponent<Flinch>().recovery);
        }
        if (GetComponent<Flinch>().stun != null)
        {
            StopCoroutine(GetComponent<Flinch>().stun);
        }
        if (GetComponent<Flinch>().dove != null)
        {
            StopCoroutine(GetComponent<Flinch>().dove);
        }
        if (GetComponent<Flinch>().groundFlinch != null)
        {
            StopCoroutine(GetComponent<Flinch>().groundFlinch);
        }
        if (GetComponent<Flinch>().parried != null)
        {
            StopCoroutine(GetComponent<Flinch>().parried);
        }
        #endregion

        combatInstance.invulnerable = true;
        GetComponent<Stamina>().SubtractStamina(combatStatsInstance.staminaDodgeCost);
        if (tag == "Player") { GetComponent<CharacterController>().enabled = false; }
        
        
        yield return new WaitForSeconds(combatStatsInstance.playerDodgeTime);
        isDodging = false;
        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = true;
            combatInstance.faceEnemy = false;
            GetComponent<Movement>().PlayerMovement();
        }
        else
        {
            anim.SetInteger("State", 0);
        } 
        GetComponent<Combat>().invulnerable = false;
        combatInstance.canAttack = true;
        combatInstance.canBlock = true;
        combatInstance.canUseTechnique = true;
        if (tag == "Enemy") { GetComponent<AiBehavior>().canGlideToEnemy = false; GetComponent<AiBehavior>().isChasingEnemy = true; }
        yield return new WaitForSeconds(.25f);
        combatInstance.invinsible = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            isColliding = true;
            if (isDodging)
            {
                isDodging = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            isColliding = false;
        }
    }
}
