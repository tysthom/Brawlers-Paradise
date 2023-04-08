using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technique : MonoBehaviour
{
    public void UseTechnique(GameObject brawler)
    {
        if (brawler.GetComponent<Combat>().canUseTechnique)
        {
            if (brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
            {
                StartCoroutine(KarateTechnique(brawler));
            } else if(brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
            {
                GetComponent<Combat>().guardBreaker = StartCoroutine(GetComponent<Combat>().GuardBreaker());
            } else if(brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
            {
                GetComponent<Combat>().diveAttack = StartCoroutine(GetComponent<Combat>().DiveAttack());
            } else if(brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
            {
                GetComponent<Combat>().stretch = StartCoroutine(GetComponent<Combat>().Stretch());
            }  else if(brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
            {
                Debug.Log("Eye Poke");
                GetComponent<Combat>().eyePoke = StartCoroutine(GetComponent<Combat>().EyePoke());
            }  else if(brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
            {
                GetComponent<Combat>().bearhugGrab = StartCoroutine(GetComponent<Combat>().BearhugGrab());
            }
        }
    }

    IEnumerator KarateTechnique(GameObject b)
    {
        if (b.GetComponent<Animator>().GetBool("isOffensiveStance"))
        {
            b.GetComponent<Animator>().SetBool("isOffensiveStance", false);
            b.GetComponent<Animator>().SetBool("isDefensiveStance", true);
            b.GetComponent<Animator>().SetBool("isPassiveStance", false);
        }
        else if(b.GetComponent<Animator>().GetBool("isDefensiveStance"))
        {
            b.GetComponent<Animator>().SetBool("isOffensiveStance", false);
            b.GetComponent<Animator>().SetBool("isDefensiveStance", false);
            b.GetComponent<Animator>().SetBool("isPassiveStance", true);
        }
        else if(b.GetComponent<Animator>().GetBool("isPassiveStance"))
        {
            b.GetComponent<Animator>().SetBool("isOffensiveStance", true);
            b.GetComponent<Animator>().SetBool("isDefensiveStance", false);
            b.GetComponent<Animator>().SetBool("isPassiveStance", false);
        }
        else
        {
            Debug.Log("Stance Issue!!");
        }

        b.GetComponent<Combat>().stanceCooldown = true;
        yield return new WaitForSeconds(.5f);
        b.GetComponent<Combat>().stanceCooldown = false;
    }
}
