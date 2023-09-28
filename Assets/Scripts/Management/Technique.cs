using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technique : MonoBehaviour
{
    GameObject gameManager;
    ResultStats resultStatsInstance;
    GameObject particleManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        resultStatsInstance = gameManager.GetComponent<ResultStats>();
        particleManager = GameObject.Find("Particle Manager");

        StartCoroutine(InitialParticles());
    }

    IEnumerator InitialParticles()
    {
        yield return new WaitUntil(() => UniversalFight.fight);

        if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            GetComponent<Animator>().SetBool("isOffensiveStance", true);
            GetComponent<Animator>().SetBool("isDefensiveStance", false);
            GetComponent<Animator>().SetBool("isPassiveStance", false);

            particleManager.GetComponent<ParticleManager>().KarateOffensiveParticles(gameObject); //Switches stance from defensive to passive & activates particles
        }
    }

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

            particleManager.GetComponent<ParticleManager>().KarateDefensiveParticles(gameObject); //Switches stance from defensive to passive & activates particles
        }
        else if(b.GetComponent<Animator>().GetBool("isDefensiveStance"))
        {
            b.GetComponent<Animator>().SetBool("isOffensiveStance", false);
            b.GetComponent<Animator>().SetBool("isDefensiveStance", false);
            b.GetComponent<Animator>().SetBool("isPassiveStance", true);

            particleManager.GetComponent<ParticleManager>().KaratePassiveParticles(gameObject); //Switches stance from defensive to passive & activates particles
        }
        else if(b.GetComponent<Animator>().GetBool("isPassiveStance"))
        {
            b.GetComponent<Animator>().SetBool("isOffensiveStance", true);
            b.GetComponent<Animator>().SetBool("isDefensiveStance", false);
            b.GetComponent<Animator>().SetBool("isPassiveStance", false);

            particleManager.GetComponent<ParticleManager>().KarateOffensiveParticles(gameObject); //Switches stance from defensive to passive & activates particles
        }

        resultStatsInstance.TechniqueUsage(gameObject);

        b.GetComponent<Combat>().stanceCooldown = true;
        yield return new WaitForSeconds(.5f);
        b.GetComponent<Combat>().stanceCooldown = false;
    }
}
