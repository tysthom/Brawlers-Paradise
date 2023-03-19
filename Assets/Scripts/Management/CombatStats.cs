using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats : MonoBehaviour
{
    [Header("Refrences")]
    public GameObject brawler1;
    public GameObject brawler2;
    public GameObject idManager;
    GameObject brawlerStats;
    BrawlerStats brawlerStatsInstance;
    public KarateStats karateStatsRefrence;
    public BoxingStats boxingStatsRefrence;
    public MMAStats mmaStatsRefrence;
    public TkdStats tkdStatsRefrence;
    public kungFuStats kungFuStatsRefrence;
    public ProWrestlingStats proWrestlingStatsRefrence;


    [Header("Brawler 1 Combat")]
    public int brawler1AttackDamage = 50;
    public float brawler1FirstAttackTime = 1.12f;
    public float brawler1FirstAttackDistance = 9;
    public float brawler1SecondAAttackTime = 1.12f;
    public float brawler1SecondAAttackDistance = 7;
    public float brawler1SecondBAttackTime = 1.12f;
    public float brawler1SecondBAttackDistance = 7;
    public float brawler1ThirdAttackTime = 1.12f;
    public float brawler1ThirdAttackV1Distance = 7;
    public float brawler1ThirdAttackV2Distance = 7;
    public float brawler1ThirdAttackV3Distance = 7;
    public float brawler1ThirdAttackV4Distance = 7;
    public float brawler1ForthAttackTime = 1.12f;
    public float brawler1ForthAttackV1Distance = 5;
    public float brawler1ForthAttackV2Distance = 2;

    [Header("Brawler 2 Combat")]
    public int brawler2AttackDamage = 50;
    public float brawler2FirstAttackTime = .5f;
    public float brawler2FirstAttackDistance = 5;
    public float brawler2SecondAAttackTime = .9f;
    public float brawler2SecondAAttackDistance = 5;
    public float brawler2SecondBAttackTime = .5f;
    public float brawler2SecondBAttackDistance = 5;
    public float brawler2ThirdAttackTime = 1.12f;
    public float brawler2ThirdAttackV1Distance = 7;
    public float brawler2ThirdAttackV2Distance = 7;
    public float brawler2ThirdAttackV3Distance = 7;
    public float brawler2ThirdAttackV4Distance = 7;
    public float parryAttackTime = .75f;

    [Header("Block")]
    public float blockMinAmount = 200;
    public float staminaBlockCost = 300;
    public float blockMultiplier = .4f;

    [Header("Dodge")]
    public float playerDodgeDistance = 5;
    public float playerDodgeTime = .25f;
    public float staminaDodgeCost = 200;
    public float playerDodgeMinAmount = 300;

    [Header("Flinch")]
    public float flinchDistance = 8;
    public float flinchTime = .2f;
    public float stunDuration = 5;

    [Header("Knockback")]
    public float knockbackDistance = 1;
    public float knockbackTime = 1;

    [Header("Gurad Breaker")]
    public float guardBreakerDistance = 12;
    public float guardBreakerTime = 1.75f;
    public float guardBreakerBufferTime = .5f;

    [Header("Parry")]
    public float parryTime = .25f;
    public float parryBufferTime = .5f;

    [Header("Counter Attack")]
    public float counterAttackTime = .35f;

    [Header("Throwables")]
    public float throwableDamage = 55;

    [Header("Finisher")]
    public float finisherDistance = 2;

    [Header("Death")]
    public int bounceBackForceAmount = 250;

    [Header("Ai Variation")]
    public bool randomFightStyle;
    [Range(0, 10)] public int aiDefendFrequency;
    [Range(0, 10)] public int aiParryDodgeFrequency;
    [Range(0, 10)] public int aiContinuedAttackFrequency;
    [Range(0, 10)] public int aiThrowableFrequency;

    private void Awake()
    {
        brawlerStats = GameObject.Find("Combat Manager");
        brawlerStatsInstance = brawlerStats.GetComponent<BrawlerStats>();
    }

    private void Start()
    {
        #region Brawler1 Fight Stats

        if (brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            brawler1FirstAttackTime = karateStatsRefrence.firstAttackTime;
            brawler1FirstAttackDistance = karateStatsRefrence.firstAttackDistance;
            brawler1SecondAAttackTime = karateStatsRefrence.secondAAttackTime;
            brawler1SecondAAttackDistance = karateStatsRefrence.secondAAttackDistance;
            brawler1SecondBAttackTime = karateStatsRefrence.secondBAttackTime;
            brawler1SecondBAttackDistance = karateStatsRefrence.secondBAttackDistance;
            brawler1ThirdAttackTime = karateStatsRefrence.thirdAttackTime;
            brawler1ThirdAttackV1Distance = karateStatsRefrence.thirdAttackV1Distance;
            brawler1ThirdAttackV2Distance = karateStatsRefrence.thirdAttackV2Distance;
            brawler1ThirdAttackV3Distance = karateStatsRefrence.thirdAttackV3Distance;
            brawler1ThirdAttackV4Distance = karateStatsRefrence.thirdAttackV4Distance;
            brawler1ForthAttackTime = karateStatsRefrence.forthAttackTime;
            brawler1ForthAttackV1Distance = karateStatsRefrence.forthAttackV1Distance;
            brawler1ForthAttackV2Distance = karateStatsRefrence.forthAttackV2Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b1AttackSpeed = karateStatsRefrence.karateFightTypeStats[0];
                brawlerStatsInstance.b1Strength = karateStatsRefrence.karateFightTypeStats[1];
                brawlerStatsInstance.b1Range = karateStatsRefrence.karateFightTypeStats[2];
                brawlerStatsInstance.b1Health = karateStatsRefrence.karateFightTypeStats[3];
                brawlerStatsInstance.b1Stamina = karateStatsRefrence.karateFightTypeStats[4];
            }
        } else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
        {
            brawler1FirstAttackTime = boxingStatsRefrence.firstAttackTime;
            brawler1FirstAttackDistance = boxingStatsRefrence.firstAttackDistance;
            brawler1SecondAAttackTime = boxingStatsRefrence.secondAAttackTime;
            brawler1SecondAAttackDistance = boxingStatsRefrence.secondAAttackDistance;
            brawler1SecondBAttackTime = boxingStatsRefrence.secondBAttackTime;
            brawler1SecondBAttackDistance = boxingStatsRefrence.secondBAttackDistance;
            brawler1ThirdAttackTime = boxingStatsRefrence.thirdAttackTime;
            brawler1ThirdAttackV1Distance = boxingStatsRefrence.thirdAttackV1Distance;
            brawler1ThirdAttackV2Distance = boxingStatsRefrence.thirdAttackV2Distance;
            brawler1ThirdAttackV3Distance = boxingStatsRefrence.thirdAttackV3Distance;
            brawler1ThirdAttackV4Distance = boxingStatsRefrence.thirdAttackV4Distance;
            brawler1ForthAttackTime = boxingStatsRefrence.forthAttackTime;
            brawler1ForthAttackV1Distance = boxingStatsRefrence.forthAttackV1Distance;
            brawler1ForthAttackV2Distance = boxingStatsRefrence.forthAttackV2Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b1AttackSpeed = boxingStatsRefrence.boxingFightTypeStats[0];
                brawlerStatsInstance.b1Strength = boxingStatsRefrence.boxingFightTypeStats[1];
                brawlerStatsInstance.b1Range = boxingStatsRefrence.boxingFightTypeStats[2];
                brawlerStatsInstance.b1Health = boxingStatsRefrence.boxingFightTypeStats[3];
                brawlerStatsInstance.b1Stamina = boxingStatsRefrence.boxingFightTypeStats[4];
            }
        }else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
        {
            brawler1FirstAttackTime = mmaStatsRefrence.firstAttackTime;
            brawler1FirstAttackDistance = mmaStatsRefrence.firstAttackDistance;
            brawler1SecondAAttackTime = mmaStatsRefrence.secondAAttackTime;
            brawler1SecondAAttackDistance = mmaStatsRefrence.secondAAttackDistance;
            brawler1SecondBAttackTime = mmaStatsRefrence.secondBAttackTime;
            brawler1SecondBAttackDistance = mmaStatsRefrence.secondBAttackDistance;
            brawler1ThirdAttackTime = mmaStatsRefrence.thirdAttackTime;
            brawler1ThirdAttackV1Distance = mmaStatsRefrence.thirdAttackV1Distance;
            brawler1ThirdAttackV2Distance = mmaStatsRefrence.thirdAttackV2Distance;
            brawler1ThirdAttackV3Distance = mmaStatsRefrence.thirdAttackV3Distance;
            brawler1ThirdAttackV4Distance = mmaStatsRefrence.thirdAttackV4Distance;
            brawler1ForthAttackTime = mmaStatsRefrence.forthAttackTime;
            brawler1ForthAttackV1Distance = mmaStatsRefrence.forthAttackV1Distance;
            brawler1ForthAttackV2Distance = mmaStatsRefrence.forthAttackV2Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b1AttackSpeed = mmaStatsRefrence.mmaFightTypeStats[0];
                brawlerStatsInstance.b1Strength = mmaStatsRefrence.mmaFightTypeStats[1];
                brawlerStatsInstance.b1Range = mmaStatsRefrence.mmaFightTypeStats[2];
                brawlerStatsInstance.b1Health = mmaStatsRefrence.mmaFightTypeStats[3];
                brawlerStatsInstance.b1Stamina = mmaStatsRefrence.mmaFightTypeStats[4];
            }
        } else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
        {
            brawler1FirstAttackTime = tkdStatsRefrence.firstAttackTime;
            brawler1FirstAttackDistance = tkdStatsRefrence.firstAttackDistance;
            brawler1SecondAAttackTime = tkdStatsRefrence.secondAAttackTime;
            brawler1SecondAAttackDistance = tkdStatsRefrence.secondAAttackDistance;
            brawler1SecondBAttackTime = tkdStatsRefrence.secondBAttackTime;
            brawler1SecondBAttackDistance = tkdStatsRefrence.secondBAttackDistance;
            brawler1ThirdAttackTime = tkdStatsRefrence.thirdAttackTime;
            brawler1ThirdAttackV1Distance = tkdStatsRefrence.thirdAttackV1Distance;
            brawler1ThirdAttackV2Distance = tkdStatsRefrence.thirdAttackV2Distance;
            brawler1ThirdAttackV3Distance = tkdStatsRefrence.thirdAttackV3Distance;
            brawler1ThirdAttackV4Distance = tkdStatsRefrence.thirdAttackV4Distance;
            brawler1ForthAttackTime = tkdStatsRefrence.forthAttackTime;
            brawler1ForthAttackV1Distance = tkdStatsRefrence.forthAttackV1Distance;
            brawler1ForthAttackV2Distance = tkdStatsRefrence.forthAttackV2Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b1AttackSpeed = tkdStatsRefrence.tkdFightTypeStats[0];
                brawlerStatsInstance.b1Strength = tkdStatsRefrence.tkdFightTypeStats[1];
                brawlerStatsInstance.b1Range = tkdStatsRefrence.tkdFightTypeStats[2];
                brawlerStatsInstance.b1Health = tkdStatsRefrence.tkdFightTypeStats[3];
                brawlerStatsInstance.b1Stamina = tkdStatsRefrence.tkdFightTypeStats[4];
            }
        }else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
        {
            brawler1FirstAttackTime = kungFuStatsRefrence.firstAttackTime;
            brawler1FirstAttackDistance = kungFuStatsRefrence.firstAttackDistance;
            brawler1SecondAAttackTime = kungFuStatsRefrence.secondAAttackTime;
            brawler1SecondAAttackDistance = kungFuStatsRefrence.secondAAttackDistance;
            brawler1SecondBAttackTime = kungFuStatsRefrence.secondBAttackTime;
            brawler1SecondBAttackDistance = kungFuStatsRefrence.secondBAttackDistance;
            brawler1ThirdAttackTime = kungFuStatsRefrence.thirdAttackTime;
            brawler1ThirdAttackV1Distance = kungFuStatsRefrence.thirdAttackV1Distance;
            brawler1ThirdAttackV2Distance = kungFuStatsRefrence.thirdAttackV2Distance;
            brawler1ThirdAttackV3Distance = kungFuStatsRefrence.thirdAttackV3Distance;
            brawler1ThirdAttackV4Distance = kungFuStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b1AttackSpeed = kungFuStatsRefrence.kungFuFightTypeStats[0];
                brawlerStatsInstance.b1Strength = kungFuStatsRefrence.kungFuFightTypeStats[1];
                brawlerStatsInstance.b1Range = kungFuStatsRefrence.kungFuFightTypeStats[2];
                brawlerStatsInstance.b1Health = kungFuStatsRefrence.kungFuFightTypeStats[3];
                brawlerStatsInstance.b1Stamina = kungFuStatsRefrence.kungFuFightTypeStats[4];
            }
        } else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
        {
            brawler1FirstAttackTime = proWrestlingStatsRefrence.firstAttackTime;
            brawler1FirstAttackDistance = proWrestlingStatsRefrence.firstAttackDistance;
            brawler1SecondAAttackTime = proWrestlingStatsRefrence.secondAAttackTime;
            brawler1SecondAAttackDistance = proWrestlingStatsRefrence.secondAAttackDistance;
            brawler1SecondBAttackTime = proWrestlingStatsRefrence.secondBAttackTime;
            brawler1SecondBAttackDistance = proWrestlingStatsRefrence.secondBAttackDistance;
            brawler1ThirdAttackTime = proWrestlingStatsRefrence.thirdAttackTime;
            brawler1ThirdAttackV1Distance = proWrestlingStatsRefrence.thirdAttackV1Distance;
            brawler1ThirdAttackV2Distance = proWrestlingStatsRefrence.thirdAttackV2Distance;
            brawler1ThirdAttackV3Distance = proWrestlingStatsRefrence.thirdAttackV3Distance;
            brawler1ThirdAttackV4Distance = proWrestlingStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b1AttackSpeed = proWrestlingStatsRefrence.proWrestlingFightTypeStats[0];
                brawlerStatsInstance.b1Strength = proWrestlingStatsRefrence.proWrestlingFightTypeStats[1];
                brawlerStatsInstance.b1Range = proWrestlingStatsRefrence.proWrestlingFightTypeStats[2];
                brawlerStatsInstance.b1Health = proWrestlingStatsRefrence.proWrestlingFightTypeStats[3];
                brawlerStatsInstance.b1Stamina = proWrestlingStatsRefrence.proWrestlingFightTypeStats[4];
            }
        }

        #endregion Fight Stats

        #region Brawler2 Fight Stats

        if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            brawler2FirstAttackTime = karateStatsRefrence.firstAttackTime;
            brawler2FirstAttackDistance = karateStatsRefrence.firstAttackDistance;
            brawler2SecondAAttackTime = karateStatsRefrence.secondAAttackTime;
            brawler2SecondAAttackDistance = karateStatsRefrence.secondAAttackDistance;
            brawler2SecondBAttackTime = karateStatsRefrence.secondBAttackTime;
            brawler2SecondBAttackDistance = karateStatsRefrence.secondBAttackDistance;
            brawler2ThirdAttackTime = karateStatsRefrence.thirdAttackTime;
            brawler2ThirdAttackV1Distance = karateStatsRefrence.thirdAttackV1Distance;
            brawler2ThirdAttackV2Distance = karateStatsRefrence.thirdAttackV2Distance;
            brawler2ThirdAttackV3Distance = karateStatsRefrence.thirdAttackV3Distance;
            brawler2ThirdAttackV4Distance = karateStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b2AttackSpeed = karateStatsRefrence.karateFightTypeStats[0];
                brawlerStatsInstance.b2Strength = karateStatsRefrence.karateFightTypeStats[1];
                brawlerStatsInstance.b2Range = karateStatsRefrence.karateFightTypeStats[2];
                brawlerStatsInstance.b2Health = karateStatsRefrence.karateFightTypeStats[3];
                brawlerStatsInstance.b2Stamina = karateStatsRefrence.karateFightTypeStats[4];
            }
        }
        else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
        {
            brawler2FirstAttackTime = boxingStatsRefrence.firstAttackTime;
            brawler2FirstAttackDistance = boxingStatsRefrence.firstAttackDistance;
            brawler2SecondAAttackTime = boxingStatsRefrence.secondAAttackTime;
            brawler2SecondAAttackDistance = boxingStatsRefrence.secondAAttackDistance;
            brawler2SecondBAttackTime = boxingStatsRefrence.secondBAttackTime;
            brawler2SecondBAttackDistance = boxingStatsRefrence.secondBAttackDistance;
            brawler2ThirdAttackTime = boxingStatsRefrence.thirdAttackTime;
            brawler2ThirdAttackV1Distance = boxingStatsRefrence.thirdAttackV1Distance;
            brawler2ThirdAttackV2Distance = boxingStatsRefrence.thirdAttackV2Distance;
            brawler2ThirdAttackV3Distance = boxingStatsRefrence.thirdAttackV3Distance;
            brawler2ThirdAttackV4Distance = boxingStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b2AttackSpeed = boxingStatsRefrence.boxingFightTypeStats[0];
                brawlerStatsInstance.b2Strength = boxingStatsRefrence.boxingFightTypeStats[1];
                brawlerStatsInstance.b2Range = boxingStatsRefrence.boxingFightTypeStats[2];
                brawlerStatsInstance.b2Health = boxingStatsRefrence.boxingFightTypeStats[3];
                brawlerStatsInstance.b2Stamina = boxingStatsRefrence.boxingFightTypeStats[4];
            }
        }
        else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
        {
            brawler2FirstAttackTime = mmaStatsRefrence.firstAttackTime;
            brawler2FirstAttackDistance = mmaStatsRefrence.firstAttackDistance;
            brawler2SecondAAttackTime = mmaStatsRefrence.secondAAttackTime;
            brawler2SecondAAttackDistance = mmaStatsRefrence.secondAAttackDistance;
            brawler2SecondBAttackTime = mmaStatsRefrence.secondBAttackTime;
            brawler2SecondBAttackDistance = mmaStatsRefrence.secondBAttackDistance;
            brawler2ThirdAttackTime = mmaStatsRefrence.thirdAttackTime;
            brawler2ThirdAttackV1Distance = mmaStatsRefrence.thirdAttackV1Distance;
            brawler2ThirdAttackV2Distance = boxingStatsRefrence.thirdAttackV2Distance;
            brawler2ThirdAttackV3Distance = mmaStatsRefrence.thirdAttackV3Distance;
            brawler2ThirdAttackV4Distance = mmaStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b2AttackSpeed = mmaStatsRefrence.mmaFightTypeStats[0];
                brawlerStatsInstance.b2Strength = mmaStatsRefrence.mmaFightTypeStats[1];
                brawlerStatsInstance.b2Range = mmaStatsRefrence.mmaFightTypeStats[2];
                brawlerStatsInstance.b2Health = mmaStatsRefrence.mmaFightTypeStats[3];
                brawlerStatsInstance.b2Stamina = mmaStatsRefrence.mmaFightTypeStats[4];
            }
        }
        else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
        {
            brawler2FirstAttackTime = tkdStatsRefrence.firstAttackTime;
            brawler2FirstAttackDistance = tkdStatsRefrence.firstAttackDistance;
            brawler2SecondAAttackTime = tkdStatsRefrence.secondAAttackTime;
            brawler2SecondAAttackDistance = tkdStatsRefrence.secondAAttackDistance;
            brawler2SecondBAttackTime = tkdStatsRefrence.secondBAttackTime;
            brawler2SecondBAttackDistance = tkdStatsRefrence.secondBAttackDistance;
            brawler2ThirdAttackTime = tkdStatsRefrence.thirdAttackTime;
            brawler2ThirdAttackV1Distance = tkdStatsRefrence.thirdAttackV1Distance;
            brawler2ThirdAttackV2Distance = tkdStatsRefrence.thirdAttackV2Distance;
            brawler2ThirdAttackV3Distance = tkdStatsRefrence.thirdAttackV3Distance;
            brawler2ThirdAttackV4Distance = tkdStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b2AttackSpeed = tkdStatsRefrence.tkdFightTypeStats[0];
                brawlerStatsInstance.b2Strength = tkdStatsRefrence.tkdFightTypeStats[1];
                brawlerStatsInstance.b2Range = tkdStatsRefrence.tkdFightTypeStats[2];
                brawlerStatsInstance.b2Health = tkdStatsRefrence.tkdFightTypeStats[3];
                brawlerStatsInstance.b2Stamina = tkdStatsRefrence.tkdFightTypeStats[4];
            }

        } else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
        {
            brawler2FirstAttackTime = kungFuStatsRefrence.firstAttackTime;
            brawler2FirstAttackDistance = kungFuStatsRefrence.firstAttackDistance;
            brawler2SecondAAttackTime = kungFuStatsRefrence.secondAAttackTime;
            brawler2SecondAAttackDistance = kungFuStatsRefrence.secondAAttackDistance;
            brawler2SecondBAttackTime = kungFuStatsRefrence.secondBAttackTime;
            brawler2SecondBAttackDistance = kungFuStatsRefrence.secondBAttackDistance;
            brawler2ThirdAttackTime = kungFuStatsRefrence.thirdAttackTime;
            brawler2ThirdAttackV1Distance = kungFuStatsRefrence.thirdAttackV1Distance;
            brawler2ThirdAttackV2Distance = kungFuStatsRefrence.thirdAttackV2Distance;
            brawler2ThirdAttackV3Distance = kungFuStatsRefrence.thirdAttackV3Distance;
            brawler2ThirdAttackV4Distance = kungFuStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b2AttackSpeed = kungFuStatsRefrence.kungFuFightTypeStats[0];
                brawlerStatsInstance.b2Strength = kungFuStatsRefrence.kungFuFightTypeStats[1];
                brawlerStatsInstance.b2Range = kungFuStatsRefrence.kungFuFightTypeStats[2];
                brawlerStatsInstance.b2Health = kungFuStatsRefrence.kungFuFightTypeStats[3];
                brawlerStatsInstance.b2Stamina = kungFuStatsRefrence.kungFuFightTypeStats[4];
            }

        } else if(brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
        {
            brawler2FirstAttackTime = proWrestlingStatsRefrence.firstAttackTime;
            brawler2FirstAttackDistance = proWrestlingStatsRefrence.firstAttackDistance;
            brawler2SecondAAttackTime = proWrestlingStatsRefrence.secondAAttackTime;
            brawler2SecondAAttackDistance = proWrestlingStatsRefrence.secondAAttackDistance;
            brawler2SecondBAttackTime = proWrestlingStatsRefrence.secondBAttackTime;
            brawler2SecondBAttackDistance = proWrestlingStatsRefrence.secondBAttackDistance;
            brawler2ThirdAttackTime = proWrestlingStatsRefrence.thirdAttackTime;
            brawler2ThirdAttackV1Distance = proWrestlingStatsRefrence.thirdAttackV1Distance;
            brawler2ThirdAttackV2Distance = proWrestlingStatsRefrence.thirdAttackV2Distance;
            brawler2ThirdAttackV3Distance = proWrestlingStatsRefrence.thirdAttackV3Distance;
            brawler2ThirdAttackV4Distance = proWrestlingStatsRefrence.thirdAttackV4Distance;

            if (brawlerStatsInstance.useAssignedFightTypeStats)
            {
                brawlerStatsInstance.b2AttackSpeed = proWrestlingStatsRefrence.proWrestlingFightTypeStats[0];
                brawlerStatsInstance.b2Strength = proWrestlingStatsRefrence.proWrestlingFightTypeStats[1];
                brawlerStatsInstance.b2Range = proWrestlingStatsRefrence.proWrestlingFightTypeStats[2];
                brawlerStatsInstance.b2Health = proWrestlingStatsRefrence.proWrestlingFightTypeStats[3];
                brawlerStatsInstance.b2Stamina = proWrestlingStatsRefrence.proWrestlingFightTypeStats[4];
            }
        }
            #endregion Fight Stats
        }
}
