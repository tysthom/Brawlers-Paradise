using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultStats : MonoBehaviour
{
    [Header("Refrences")]
    GameObject gameManager;
    IdManagear idManagerInstance;

    [Header("Brawler1")]
    public float b1TotalBasicAttacks;
    public float b1BasicAttacksLanded;
    public float b1AttacksMissed;
    public float b1DamageInflicted;
    public float b1HealthRecoverd;
    public float b1StaminaUsed;
    public float b1ThrowablesUsed;

    public float b1DodgesPerformed;
    public float b1techniqueUsage;
    public float b1souvenirUsage;

    //public float match time;

    //wins, losses

    [Header("Brawler2")]
    public float b2TotalBasicAttacks;
    public float b2BasicAttacksLanded;
    public float b2AttacksMissed;
    public float b2DamageInflicted;
    public float b2HealthRecoverd;
    public float b2StaminaUsed;
    public float b2ThrowablesUsed;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
    }

    public void BasicTotalAttacks(GameObject brawler)
    {
        if(brawler == idManagerInstance.brawler1)
        {
            b1TotalBasicAttacks++;
        } else if(brawler == idManagerInstance.brawler2)
        {
            b2TotalBasicAttacks++;
        }
    }

    public void BasicAttacksLanded(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1BasicAttacksLanded++;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2BasicAttacksLanded++;
        }
    }

    public void DamageInflicted(GameObject brawler, float amount)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b2DamageInflicted += (int)amount; //Flipped
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b1DamageInflicted += (int)amount; //Flipped
        }
    }

    public void HealthRecovered(GameObject brawler, float amount)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1HealthRecoverd += (int)amount;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2HealthRecoverd += (int)amount;
        }
    }

    public void StaminaUsed(GameObject brawler, float amount)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1StaminaUsed += (int)amount;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2StaminaUsed += (int)amount;
        }
    }

    public void ThrowablesUsed(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1ThrowablesUsed++;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2ThrowablesUsed++;
        }
    }
}
