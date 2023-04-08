using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TkdStats : MonoBehaviour
{
    public float firstAttackTime = 1;
    public float firstAttackDistance = 5;
    public float secondAAttackTime = 1.12f;
    public float secondAAttackDistance = 7;
    public float secondBAttackTime = 1.12f;
    public float secondBAttackDistance = 7;
    public float thirdAttackTime = 1.12f;
    public float thirdAttackV1Distance = 7;
    public float thirdAttackV2Distance = 7;
    public float thirdAttackV3Distance = 7;
    public float thirdAttackV4Distance = 7;
    public float forthAttackTime = 1f;
    public float forthAttackV1Distance = 5;
    public float forthAttackV2Distance = 3;

    [Header("Fight Type Stats")]
    public int[] tkdFightTypeStats = new int[5];

    [Header("Stretch")]
    public float stretchParryWindow = .15f;
    public float stretchSuccessTime = .25f;
    public float stretchMissTime = 2.5f;

    [Header("Ai Stretch Usage")]
    [Range(0, 10)] public int aiStretchFrequency;


    #region Animation
    /* 0 - Combat Idle
     * 2 - Run
     * 3 - TKD Basic Attack 1
     * 4 - TKD Basic Attack 2V1
     * 5 - TKD Basic Attack 2V2
     * 6V1 - TKD Basic Attack 3V1
     * 6V2 - TKD Basic Attack 3V2
     * 7V1 - TKD Basic Attack 3V3
     * 7V2 - TKD Basic Attack 3V4
     * 
     * 15 - TKD Counter Attack
     * 
     * 20 - TKD Parry
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
     * 105 - TKD Stunned
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
