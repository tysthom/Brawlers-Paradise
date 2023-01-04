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
        combatManager = GameObject.Find("Combat Manager");

        if(tag == "Enemy" && combatManager.GetComponent<CombatStats>().randomFightStyle)
        {
            fightStyle = (fightStyles)Random.Range(0, System.Enum.GetNames(typeof(fightStyles)).Length);
        }
    }
}
