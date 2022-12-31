using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeManager : MonoBehaviour
{
    public int[] outfits;
    public float[] blendShapes = new float[7];
    /*Arms
     * Shoulders
     * Calves
     * Thighs
     * Waist
     * Neck
     * Feet
     * Butt
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
    }

    void Outfit1(SkinnedMeshRenderer skin, GameObject brawler)
    {
        skin.SetBlendShapeWeight(0, 25); //Neck
        skin.SetBlendShapeWeight(1, 0); //Shoulders
        skin.SetBlendShapeWeight(2, 0); //UpperArms
        skin.SetBlendShapeWeight(3, 0); //LowerArms
        skin.SetBlendShapeWeight(4, 0); //Chest
        skin.SetBlendShapeWeight(5, 0); //Back
        skin.SetBlendShapeWeight(6, 35); //Waist
        skin.SetBlendShapeWeight(7, 0); //Butt
        skin.SetBlendShapeWeight(8, 0); //Thighs
        skin.SetBlendShapeWeight(9, 0); //Calves
        skin.SetBlendShapeWeight(10, 0); //Feet
    }
    void Outfit2(SkinnedMeshRenderer skin, GameObject brawler)
    {
        skin.SetBlendShapeWeight(0, 0);
        skin.SetBlendShapeWeight(1, 0);
        skin.SetBlendShapeWeight(2, 0);
        skin.SetBlendShapeWeight(3, 0);
        skin.SetBlendShapeWeight(4, 25);
        skin.SetBlendShapeWeight(5, 0);
        skin.SetBlendShapeWeight(6, 0f);
        skin.SetBlendShapeWeight(6, 0f);
    }
    void Outfit3(SkinnedMeshRenderer skin, GameObject brawler)
    {
        skin.SetBlendShapeWeight(0, 0);
        skin.SetBlendShapeWeight(1, 0);
        skin.SetBlendShapeWeight(2, 0);
        skin.SetBlendShapeWeight(3, 30);
        skin.SetBlendShapeWeight(4, 20);
        skin.SetBlendShapeWeight(5, 0);
        skin.SetBlendShapeWeight(6, 100);
        skin.SetBlendShapeWeight(6, 65f);
    }
}
