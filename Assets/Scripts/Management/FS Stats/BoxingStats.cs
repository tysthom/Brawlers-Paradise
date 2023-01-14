using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStats : MonoBehaviour
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

    [Header("Ai Guardbreaker Usage")]
    [Range(0, 10)] public int aiGuardbreakerFrequency;

    [Header("Fight Type Stats")]
    public int[] boxingFightTypeStats = new int[5];

    #region Animation
    /* 0 - Combat Idle
     * 2 - Run
     * 3 - Boxing Basic Attack 1
     * 4 - Boxing Basic Attack 2V1
     * 5 - Boxing Basic Attack 2V2
     * 6V1 - Boxing Basic Attack 3V1
     * 6V2 - Boxing Basic Attack 3V2
     * 7V1 - Boxing Basic Attack 3V3
     * 7V2 - Boxing Basic Attack 3V4
     * 
     * 15 - Boxing Counter Attack
     * 
     * 20 - Boxing Parry
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
     * 105 - Boxing Stunned
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
