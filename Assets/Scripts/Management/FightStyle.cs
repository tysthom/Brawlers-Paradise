using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStyle : MonoBehaviour
{
    public enum fightStyles {karate, boxing, MMA, taekwondo, kungFu, proWrestling}

    public fightStyles fightStyle; //Assign animator based on selected fight style

    private void Start()
    {
        if(tag == "Enemy")
        {
            fightStyle = (fightStyles)Random.Range(0, System.Enum.GetNames(typeof(fightStyles)).Length);
        }
    }
}
