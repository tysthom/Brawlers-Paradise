using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public void CancelCoroutines(Coroutine ignore)
    {
        if (tag == "Enemy")
        {
            if (GetComponent<AiBehavior>().waitToAttack != null)
            {
                StopCoroutine(GetComponent<AiBehavior>().waitToAttack);
                GetComponent<AiBehavior>().isWaitingToAttack = false;
                GetComponent<AiBehavior>().wTACalled = false;
            }
        }
        if (GetComponent<Combat>().block != null && ignore != GetComponent<Combat>().block)
        {
            StopCoroutine(GetComponent<Combat>().block);
            GetComponent<Combat>().isBlocking = false;
            GetComponent<Combat>().isBlockBuffering = false;
        }
        if (GetComponent<Combat>().parry != null && ignore != GetComponent<Combat>().parry)
        {
            StopCoroutine(GetComponent<Combat>().parry);
            GetComponent<Combat>().isParrying = false;
            GetComponent<Combat>().isParryBuffering = false;
        }
        if (GetComponent<Combat>().guardBreaker != null && ignore != GetComponent<Combat>().guardBreaker)
        {
            StopCoroutine(GetComponent<Combat>().guardBreaker);
            GetComponent<Combat>().isGuardBreaking = false;
            GetComponent<Combat>().guardBreakerComplete = true;
        }
        if (GetComponent<Combat>().diveAttack != null && ignore != GetComponent<Combat>().diveAttack)
        {
            StopCoroutine(GetComponent<Combat>().diveAttack);
            GetComponent<Combat>().isDiving = false;
            GetComponent<Combat>().invinsible = false;
        }
        if (GetComponent<Combat>().groundAttack != null && ignore != (GetComponent<Combat>().groundAttack))
        {
            StopCoroutine(GetComponent<Combat>().groundAttack);
            GetComponent<Combat>().isGroundAttacking = false;
            GetComponent<Combat>().isGroundIdle = false;
            GetComponent<Combat>().invinsible = false;
        }
        if (GetComponent<Combat>().stretch != null && ignore != (GetComponent<Combat>().stretch)){
            StopCoroutine(GetComponent<Combat>().stretch);
            GetComponent<Combat>().isStretching = false;
            GetComponent<Combat>().isStretchBuffering = false;
        }
        if (GetComponent<Combat>().eyePoke != null && ignore != (GetComponent<Combat>().eyePoke))
        {
            StopCoroutine(GetComponent<Combat>().eyePoke);
            GetComponent<Combat>().isEyePoking = false;
        }
        if (GetComponent<Combat>().bearhugGrab != null && ignore != (GetComponent<Combat>().bearhugGrab))
        {
            StopCoroutine(GetComponent<Combat>().bearhugGrab);
            GetComponent<Combat>().isBearhugGrabbing = false;
            GetComponent<Combat>().invulnerable = false;
        }
        if (GetComponent<Combat>().bearhugging != null && ignore != (GetComponent<Combat>().bearhugging))
        {
            StopCoroutine(GetComponent<Combat>().bearhugging);
            GetComponent<Combat>().isBearhugging = false;
        }
        if (GetComponent<Combat>().bearhugBreakOut != null && ignore != (GetComponent<Combat>().bearhugBreakOut))
        {
            StopCoroutine(GetComponent<Combat>().bearhugBreakOut);
            GetComponent<Combat>().isBearhugging = false;
        }
        if (GetComponent<Combat>().counterAttack != null && ignore != GetComponent<Combat>().counterAttack)
        {
            StopCoroutine(GetComponent<Combat>().counterAttack);
            GetComponent<Combat>().isCounterAttacking = false;
            GetComponent<Combat>().isCounterAttackBuffering = false;
        }
        if (GetComponent<Combat>().basicAttack != null && ignore != GetComponent<Combat>().basicAttack)
        {
            StopCoroutine(GetComponent<Combat>().basicAttack);
            GetComponent<Combat>().isAttacking = false;
        }
        if (GetComponent<Combat>().finisher != null && ignore != GetComponent<Combat>().finisher)
        {
            StopCoroutine(GetComponent<Combat>().finisher);
            GetComponent<Combat>().isFinishing = false;
        }
        if (GetComponent<Combat>().afterAttack != null && ignore != GetComponent<Combat>().afterAttack)
        {
            StopCoroutine(GetComponent<Combat>().afterAttack);
        }
        if (GetComponent<Flinch>().reactionTime != null && ignore != GetComponent<Flinch>().reactionTime)
        {
            StopCoroutine(GetComponent<Flinch>().reactionTime);
            GetComponent<Flinch>().isFlinching = false;
            GetComponent<Flinch>().isFlinchBuffering = false;
            GetComponent<Flinch>().isKnockedBack = false;
        }
        if (GetComponent<Flinch>().blockedBack != null && ignore != GetComponent<Flinch>().blockedBack)
        {
            StopCoroutine(GetComponent<Flinch>().blockedBack);
            GetComponent<Flinch>().isBlockedBack = false;
        }
        if (GetComponent<Flinch>().recovery != null && ignore != GetComponent<Flinch>().recovery)
        {
            StopCoroutine(GetComponent<Flinch>().recovery);
            GetComponent<Flinch>().isReacting = false;
        }
        if (GetComponent<Flinch>().stun != null && ignore != GetComponent<Flinch>().stun)
        {
            StopCoroutine(GetComponent<Flinch>().stun);
            GetComponent<Flinch>().isStunned = false;
        }
        if (GetComponent<Flinch>().dove != null && ignore != GetComponent<Flinch>().dove)
        {
            StopCoroutine(GetComponent<Flinch>().dove);
            GetComponent<Flinch>().isDove = false;
        }
        if (GetComponent<Flinch>().groundFlinch != null && ignore != GetComponent<Flinch>().groundFlinch)
        {
            StopCoroutine(GetComponent<Flinch>().groundFlinch);
        }
        if(GetComponent<Flinch>().bearHugged != null && ignore != GetComponent<Flinch>().bearHugged)
        {
            StopCoroutine(GetComponent<Flinch>().bearHugged);
            GetComponent<Flinch>().isBearhugged = false;
        }
        if (GetComponent<Flinch>().parried != null && ignore != GetComponent<Flinch>().parried)
        {
            StopCoroutine(GetComponent<Flinch>().parried);
            GetComponent<Flinch>().isParried = false;
            GetComponent<Flinch>().isParryBuffering = false;
        }
        if (GetComponent<Flinch>().surrender != null && ignore != GetComponent<Flinch>().surrender)
        {
            StopCoroutine(GetComponent<Flinch>().surrender);
            GetComponent<Flinch>().surrender = null;
            GetComponent<Flinch>().isSurrendering = false;
        }
    }
}
