using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStyle : MonoBehaviour
{
    public enum fightStyles {karate, boxing, MMA, taekwondo, kungFu, proWrestling}

    public fightStyles fightStyle; //Assign animator based on selected fight style

    GameObject combatManager;

    private void Start()
    {
        if (!UniversalFight.usingMenuData)
        {
            combatManager = GameObject.Find("Combat Manager");

            if (tag == "Enemy" && combatManager.GetComponent<CombatStats>().randomFightStyle)
            {
                fightStyle = (fightStyles)Random.Range(0, System.Enum.GetNames(typeof(fightStyles)).Length);
            }
        }
        else
        {
            if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler1)
            {
                if (StateNameController.b1MainFightStyleSelection == 0)
                {
                    fightStyle = fightStyles.karate;
                }
                else if (StateNameController.b1MainFightStyleSelection == 1)
                {
                    fightStyle = fightStyles.boxing;
                }
                else if (StateNameController.b1MainFightStyleSelection == 2)
                {
                    fightStyle = fightStyles.MMA;
                }
                else if (StateNameController.b1MainFightStyleSelection == 3)
                {
                    fightStyle = fightStyles.taekwondo;
                }
                else if (StateNameController.b1MainFightStyleSelection == 4)
                {
                    fightStyle = fightStyles.kungFu;
                }
                else if (StateNameController.b1MainFightStyleSelection == 5)
                {
                    fightStyle = fightStyles.proWrestling;
                }
            }

            if (GetComponent<BrawlerId>().brawlerId == BrawlerId.Id.brawler2)
            {
                if (StateNameController.b2MainFightStyleSelection == 0)
                {
                    fightStyle = fightStyles.karate;
                }
                else if (StateNameController.b2MainFightStyleSelection == 1)
                {
                    fightStyle = fightStyles.boxing;
                }
                else if (StateNameController.b2MainFightStyleSelection == 2)
                {
                    fightStyle = fightStyles.MMA;
                }
                else if (StateNameController.b2MainFightStyleSelection == 3)
                {
                    fightStyle = fightStyles.taekwondo;
                }
                else if (StateNameController.b2MainFightStyleSelection == 4)
                {
                    fightStyle = fightStyles.kungFu;
                }
                else if (StateNameController.b2MainFightStyleSelection == 5)
                {
                    fightStyle = fightStyles.proWrestling;
                }
            }
        }
    }
}
