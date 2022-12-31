using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultStats : MonoBehaviour
{
    [Header("Refrences")]
    GameObject gameManager;
    IdManagear idManagerInstance;

    [Header("Brawler1")]
    public float b1TotalAttacks;
    public float b1AttacksLanded;
    public float b1AttacksMissed;
    public float b1DamageInflicted;
    public float b1HealthRecoverd;
    public float b1StaminaUsed;

    [Header("Brawler2")]
    public float b2TotalAttacks;
    public float b2AttacksLanded;
    public float b2AttacksMissed;
    public float b2DamageInflicted;
    public float b2HealthRecoverd;
    public float b2StaminaUsed;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
    }

    public void TotalAttacks(GameObject brawler)
    {
        if(brawler == idManagerInstance.brawler1)
        {
            b1TotalAttacks++;
        } else if(brawler == idManagerInstance.brawler2)
        {
            b2TotalAttacks++;
        }
    }

    public void AttacksLanded(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttacksLanded++;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttacksLanded++;
        }
    }
}
