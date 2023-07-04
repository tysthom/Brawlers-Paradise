using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStyleManager : MonoBehaviour
{
    [Header("Refrences")]
    GameObject gameManager;
    GameObject brawler1, brawler2;

    public RuntimeAnimatorController[] fightingTypeAnimators;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");

        brawler1 = gameManager.GetComponent<IdManagear>().brawler1;
        brawler2 = gameManager.GetComponent<IdManagear>().brawler2;

        //Assigns animator based on brawler's selected fighting type
        if (brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[0];
        } else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[1];
        } else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[2];
        }else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[3];
        }else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[4];
        }else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[5];
        }

        if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[0];
        } else if(brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
        {
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[1];
        } else if(brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
        {
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[2];
        } else if(brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
        {
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[3];
        } else if(brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
        {
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[4];
        } else if(brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
        {
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[5];
        }

        brawler1.GetComponent<Combat>().currentStyle = brawler1.GetComponent<FightStyle>().fightStyle;
        brawler2.GetComponent<Combat>().currentStyle = brawler2.GetComponent<FightStyle>().fightStyle;
    }
}
