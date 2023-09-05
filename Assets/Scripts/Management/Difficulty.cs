using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public enum difficulty { easy, medium, hard }

    public difficulty difficultyMode;

    public void Awake()
    {
        if (UniversalFight.usingMenuData)
        {
            if(StateNameController.difficultySelection == 0)
            {
                difficultyMode = difficulty.easy;
            }
            else if (StateNameController.difficultySelection == 1)
            {
                difficultyMode = difficulty.medium;
            }
            else
            {
                difficultyMode = difficulty.hard;
            }
        }
    }
}
