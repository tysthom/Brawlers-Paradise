using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateStats : MonoBehaviour
{
    public float firstAttackTime = 1;
    public float firstAttackDistance = 9;
    public float secondAAttackTime = 1.12f;
    public float secondAAttackDistance = 7;
    public float secondBAttackTime = 1.12f;
    public float secondBAttackDistance = 7;
    public float thirdAttackTime = 1.12f;
    public float thirdAttackV1Distance = 7;
    public float thirdAttackV2Distance = 7;
    public float thirdAttackV3Distance = 7;
    public float thirdAttackV4Distance = 7;

    [Header("Offensive Stance")]
    public float karateDamageIncreaser = 1.25f;

    [Header("Defensive Stance")]
    public float karateDamageDecreaser = .75f;

    [Header("Passive Stance")]
    public int karateHealthRecoveryAmount = 5;
    public float karateRegenHealthWaitTime = 1;

    [Header("Ai Stance Change")]
    [Range(0, 10)] public int aiStanceChangeFrequency;

    [Header("Fight Type Stats")]
    public int[] karateFightTypeStats = new int[5];

    #region Animation
    /* 0V1 - Combat Idle - Attack
     * 0V1 - Combat Idle - Passive
     * 2 - Run
     * 3 - Karate Basic Attack 1
     * 4 - Karate Basic Attack 2V1
     * 5 - Karate Basic Attack 2V2
     * 6V1 - Karate Basic Attack 3V1
     * 6V2 - Karate Basic Attack 3V2
     * 7V1 - Karate Basic Attack 3V3
     * 7V2 - Karate Basic Attack 3V4
     * 
     * 15 - Counter Attack
     * 
     * 20 - Karate Parry
     * 
     * 25 - Backwards Dodge
     * 26 - Forward Dodge
     *
     * 40 - Throw Idle
     * 41 - Throw Aim
     * 
     * 52 - Grabbed
     * 
     * 100 - Flinch 1
     * 101 - Flinch 2
     * 
     * 105 - Karate Stunned
     * 
     * 110 - Knockback
     * 
     * 115 - Knockdown
     * 
     * 118 - Recovery
     * 
     * 120 - Parried
     */

    #endregion
}
