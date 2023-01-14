using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMAStats : MonoBehaviour
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


    
    public float mmaDiveRange = 3;
    public float mmaDiveDistance = 2;
    public float mmaDiveInitialDamage = 15;
    public float mmaDiveTime = 2;
    public float mmaGroundAttackTime = .5f;
    public float mmaGroundBufferTime = .35f;
    public float mmaGroundAttackStaminaCost = 50;

    [Header("Ai Dive Frequency")]
    [Range(0, 10)] public int aiDiveFrequency;

    [Header("Fight Type Stats")]
    public int[] mmaFightTypeStats = new int[5];

    #region Animation
    /* 0 - Combat Idle
     * 2 - Run
     * 3 - MMA Basic Attack 1
     * 4 - MMA Basic Attack 2V1
     * 5 - MMA Basic Attack 2V2
     * 6V1 - MMA Basic Attack 3V1
     * 6V2 - MMA Basic Attack 3V2
     * 7V1 - MMA Basic Attack 3V3
     * 7V2 - MMA Basic Attack 3V4
     * 
     * 15 - MMA Counter Attack
     * 
     * 20 - MMA Parry
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
     * 105 - MMA Stunned
     * 
     * 109 - Bearhugged
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
