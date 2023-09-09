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
    public float b1BasicAttackPercentage;
    public float b1DamageInflicted;
    public float b1HealthRecoverd;
    public float b1StaminaUsed;
    public float b1DodgesPerformed;
    public float b1ThrowablesUsed;
    public float b1TechniqueUsage;
    public float b1SouvenirUsage;

    //public float match time;

    //wins, losses

    [Header("Brawler2")]
    public float b2TotalBasicAttacks;
    public float b2BasicAttacksLanded;
    public float b2BasicAttackPercentage;
    public float b2DamageInflicted;
    public float b2HealthRecoverd;
    public float b2StaminaUsed;
    public float b2DodgesPerformed;
    public float b2ThrowablesUsed;
    public float b2TechniqueUsage;
    public float b2SouvenirUsage;

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

    public void DodgesPerformed(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1DodgesPerformed++;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2DodgesPerformed++;
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

    public void TechniqueUsage(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1TechniqueUsage++;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2TechniqueUsage++;
        }
    }

    public void SouvenirUsage(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1SouvenirUsage++;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2SouvenirUsage++;
        }
    }
}
