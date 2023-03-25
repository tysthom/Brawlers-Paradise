using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kungFuStats : MonoBehaviour
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
    public float forthAttackTime = 1.12f;
    public float forthAttackV1Distance = 5;
    public float forthAttackV2Distance = 3;

    [Header("Fight Type Stats")]
    public int[] kungFuFightTypeStats = new int[5];

    [Header("Eye Poke Stats")]
    public float eyePokeTime = 1.5f;
    public int eyePokeDamage = 10;

    [Header("Ai Combo Usage")]
    [Range(0, 10)] public int aiEyePokeFrequency;

    #region Animation
    /* 0 - Combat Idle
     * 2 - Run
     * 3 - Kung Fu Basic Attack 1
     * 4 - Kung Fu Basic Attack 2V1
     * 5 - Kung Fu Basic Attack 2V2
     * 6V1 - Kung Fu Basic Attack 3V1
     * 6V2 - Kung Fu Basic Attack 3V2
     * 7V1 - Kung Fu Basic Attack 3V3
     * 7V2 - Kung Fu Basic Attack 3V4
     * 
     * 15 - Kung Fu Counter Attack
     * 
     * 20 - Kung Fu Parry
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
     * 105 - Kung Fu Stunned
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
