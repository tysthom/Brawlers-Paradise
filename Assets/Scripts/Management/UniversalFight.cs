using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalFight : MonoBehaviour
{
    public static bool usingMenuData = true;
    public static bool fight = false;
    public static int matchTime = 0;

    public IEnumerator Timing()
    {
        while (fight)
        {
            yield return new WaitForSeconds(1);
            matchTime++;
        }
    }
}
