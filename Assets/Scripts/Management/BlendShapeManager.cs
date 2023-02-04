using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeManager : MonoBehaviour
{
    public int[] outfits;
    public float[] blendShapes = new float[7];
    /* Neck
     * Shoulders
     * UpperArms
     * LowerArms
     * Chest
     * Back
     * Stomach
     * Waist
     * Butt
     * Thighes
     * Calves
     * Feet
     */

    public void AssignBlendShape(SkinnedMeshRenderer skin, int outfitSelection, GameObject brawler)
    {
        if(outfitSelection == 0)
        {
            Outfit1(skin, brawler);
        } else if(outfitSelection == 1)
        {
            Outfit2(skin, brawler);
        } else if(outfitSelection == 2)
        {
            Outfit3(skin, brawler);
        }
        else if (outfitSelection == 3)
        {
            Outfit4(skin, brawler);
        }
        else if (outfitSelection == 4)
        {
            Outfit5(skin, brawler);
        }
    }

    void Outfit1(SkinnedMeshRenderer skin, GameObject brawler) //Karate Outfit 1
    {
        skin.SetBlendShapeWeight(0, 25); //Neck
        skin.SetBlendShapeWeight(1, 15); //Shoulders
        skin.SetBlendShapeWeight(2, 0); //UpperArms
        skin.SetBlendShapeWeight(3, 0); //LowerArms
        skin.SetBlendShapeWeight(4, 0); //Chest
        skin.SetBlendShapeWeight(5, 0); //Back
        skin.SetBlendShapeWeight(6, 100); //Stomach
        skin.SetBlendShapeWeight(7, 70); //Waist
        skin.SetBlendShapeWeight(8, 50); //Butt
        skin.SetBlendShapeWeight(9, 100); //Thighes
        skin.SetBlendShapeWeight(10, 0); //Calves
        skin.SetBlendShapeWeight(11, 20); //Feet
    }
    void Outfit2(SkinnedMeshRenderer skin, GameObject brawler) //Karate Outfit 2 
    {
        skin.SetBlendShapeWeight(0, 25); //Neck
        skin.SetBlendShapeWeight(1, 20); //Shoulders
        skin.SetBlendShapeWeight(2, 0); //UpperArms
        skin.SetBlendShapeWeight(3, 0); //LowerArms
        skin.SetBlendShapeWeight(4, 60); //Chest
        skin.SetBlendShapeWeight(5, 0); //Back
        skin.SetBlendShapeWeight(6, 100); //Stomach
        skin.SetBlendShapeWeight(7, 0); //Waist
        skin.SetBlendShapeWeight(8, 20); //Butt
        skin.SetBlendShapeWeight(9, 50); //Thighes
        skin.SetBlendShapeWeight(10, 0); //Calves
        skin.SetBlendShapeWeight(11, 0); //Feet
    }
    void Outfit3(SkinnedMeshRenderer skin, GameObject brawler) //Boxing Outfit 1
    {
        skin.SetBlendShapeWeight(0, 25); //Neck
        skin.SetBlendShapeWeight(1, 0); //Shoulders
        skin.SetBlendShapeWeight(2, 0); //UpperArms
        skin.SetBlendShapeWeight(3, 0); //LowerArms
        skin.SetBlendShapeWeight(4, 0); //Chest
        skin.SetBlendShapeWeight(5, 0); //Back
        skin.SetBlendShapeWeight(6, 100); //Stomach
        skin.SetBlendShapeWeight(7, 50); //Waist
        skin.SetBlendShapeWeight(8, 0); //Butt
        skin.SetBlendShapeWeight(9, 25); //Thighes
        skin.SetBlendShapeWeight(10, 0); //Calves
        skin.SetBlendShapeWeight(11, 0); //Feet
    }

    void Outfit4(SkinnedMeshRenderer skin, GameObject brawler) //Boxing Outfit 2
    {
        skin.SetBlendShapeWeight(0, 25); //Neck
        skin.SetBlendShapeWeight(1, 0); //Shoulders
        skin.SetBlendShapeWeight(2, 0); //UpperArms
        skin.SetBlendShapeWeight(3, 0); //LowerArms
        skin.SetBlendShapeWeight(4, 0); //Chest
        skin.SetBlendShapeWeight(5, 0); //Back
        skin.SetBlendShapeWeight(6, 100); //Stomach
        skin.SetBlendShapeWeight(7, 50); //Waist
        skin.SetBlendShapeWeight(8, 0); //Butt
        skin.SetBlendShapeWeight(9, 25); //Thighes
        skin.SetBlendShapeWeight(10, 0); //Calves
        skin.SetBlendShapeWeight(11, 0); //Feet
    }

    void Outfit5(SkinnedMeshRenderer skin, GameObject brawler) //MMA Outfit 1
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Shoulders
        skin.SetBlendShapeWeight(2, 0); //UpperArms
        skin.SetBlendShapeWeight(3, 0); //LowerArms
        skin.SetBlendShapeWeight(4, 0); //Chest
        skin.SetBlendShapeWeight(5, 0); //Back
        skin.SetBlendShapeWeight(6, 0); //Stomach
        skin.SetBlendShapeWeight(7, 100); //Waist
        skin.SetBlendShapeWeight(8, 15); //Butt
        skin.SetBlendShapeWeight(9, 20); //Thighes
        skin.SetBlendShapeWeight(10, 0); //Calves
        skin.SetBlendShapeWeight(11, 0); //Feet
    }
}
