using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlerStats : MonoBehaviour
{
    GameObject gameManager;
    IdManagear idManagerInstance;
    public bool useAssignedFightTypeStats;

    [Header("Brawler 1")]
    [Range(1, 5)] public int b1AttackSpeed;
    [Range(1, 5)] public int b1Strength;
    [Range(1, 5)] public int b1Range;
    [Range(1, 5)] public int b1Health;
    [Range(1, 5)] public int b1Stamina;

    [Header("Brawler 2")]
    [Range(1, 5)] public int b2AttackSpeed;
    [Range(1, 5)] public int b2Strength;
    [Range(1, 5)] public int b2Range;
    [Range(1, 5)] public int b2Health;
    [Range(1, 5)] public int b2Stamina;


    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
    }

    public float AttackSpeed(GameObject brawler)
    {
        float amount;
        if(brawler == idManagerInstance.brawler1)
        {
            amount = .58f + (.02f * (3-b1AttackSpeed)); //.02f is what determines the amount multiplyer. .58f is the base speed
            return amount;
        } else if(brawler == idManagerInstance.brawler2)
        {
            amount = .58f + (.02f * (3 - b2AttackSpeed));
            return amount;
        }
        return 0;
    }

    public float Strength(GameObject brawler)
    {
        float amount;
        if (brawler == idManagerInstance.brawler1)
        {
            amount = 1 + (.15f * (b1AttackSpeed - 3)); //.15f is what determines the amount multiplyer
            return amount;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            amount = 1 + (.15f * (b2AttackSpeed - 3));
            return amount;
        }
        return 0;
    }

    public float Range(GameObject brawler)
    {
        float amount;
        if (brawler == idManagerInstance.brawler1)
        {
            amount = 1 + (.1f * (b1Range - 3)); //.1f is what determines the amount multiplyer
            return amount;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            amount = 1 + (.1f * (b2Range - 3));
            return amount;
        }
        return 0;

    }

    public float Health(GameObject brawler)
    {
        float amount;
        if (brawler == idManagerInstance.brawler1)
        {
            amount = (1 + (.12f * (b1Health - 3))); //.15f is what determines the amount multiplyer
            return amount;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            amount = (1 + (.12f * (b2Health - 3)));
            return amount;
        }
        return 0;
    }

    public float Stamina(GameObject brawler)
    {
        float amount;
        if (brawler == idManagerInstance.brawler1)
        {
            amount = (1 + (.12f * (b1Stamina - 3))); //.12f is what determines the amount multiplyer
            return amount;
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            amount = (1 + (.12f * (b2Stamina - 3)));
            return amount;
        }
        return 0;
    }

}
