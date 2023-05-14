using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalFight : MonoBehaviour
{
    public bool fight;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fight = true;
        }
    }
}
