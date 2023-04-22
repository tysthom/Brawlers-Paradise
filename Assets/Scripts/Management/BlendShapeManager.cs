using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeManager : MonoBehaviour
{

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
        if(GetComponent<CharacterManager>().outfits[outfitSelection].name == "Kata_Gi")
        {
            KataGi(skin);
        } else if(GetComponent<CharacterManager>().outfits[outfitSelection].name == "Japanese_Style") 
        {
            JapaneseSyle(skin);
        } else if(GetComponent<CharacterManager>().outfits[outfitSelection].name == "Workout")
        {
            Workout(skin);
        }
        else if (GetComponent<CharacterManager>().outfits[outfitSelection].name == "Main_Event") 
        {
            MainEvent(skin);
        }
        else if (outfitSelection == 4)
        {
            Outfit5(skin, brawler);
        }
        else if (outfitSelection == 5)
        {
            Outfit6(skin, brawler);
        }
        else if (outfitSelection == 6)
        {
            Outfit7(skin, brawler);
        }
        else if (outfitSelection == 7)
        {
            Outfit8(skin, brawler);
        }
        else if (outfitSelection == 8)
        {
            Outfit9(skin, brawler);
        }
        else if (outfitSelection == 9)
        {
            Outfit10(skin, brawler);
        }
        else if (outfitSelection == 10)
        {
            Outfit11(skin, brawler);
        }
        else if (outfitSelection == 11)
        {
            Outfit12(skin, brawler);
        }
    }

    /*
     * skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 0); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 0); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    */

    void KataGi(SkinnedMeshRenderer skin) //Karate Outfit 1
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 30); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 50); //Pelivs
        skin.SetBlendShapeWeight(12, 80); //Butt
        skin.SetBlendShapeWeight(13, 50); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 20); //Feet
    }
    void JapaneseSyle(SkinnedMeshRenderer skin) //Karate Outfit 2
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 10); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 30); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 30); //Pelivs
        skin.SetBlendShapeWeight(12, 80); //Butt
        skin.SetBlendShapeWeight(13, 70); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }
    void Workout(SkinnedMeshRenderer skin) //Boxing Outfit 1
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 50); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 75); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 50); //Feet
    }

    void MainEvent(SkinnedMeshRenderer skin) //Boxing Outfit 2
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 40); //Waist
        skin.SetBlendShapeWeight(11, 20); //Pelivs
        skin.SetBlendShapeWeight(12, 100); //Butt
        skin.SetBlendShapeWeight(13, 40); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 30); //Feet
    }

    void Outfit5(SkinnedMeshRenderer skin, GameObject brawler) //MMA Outfit 1
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 35); //Waist
        skin.SetBlendShapeWeight(11, 20); //Pelivs
        skin.SetBlendShapeWeight(12, 100); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }

    void Outfit6(SkinnedMeshRenderer skin, GameObject brawler) //MMA Outfit 2
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 25); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 25); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }

    void Outfit7(SkinnedMeshRenderer skin, GameObject brawler) //TKD Outfit 1
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 100); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 100); //Pelivs
        skin.SetBlendShapeWeight(12, 100); //Butt
        skin.SetBlendShapeWeight(13, 100); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }

    void Outfit8(SkinnedMeshRenderer skin, GameObject brawler) //TKD Outfit 2
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 100); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 100); //Pelivs
        skin.SetBlendShapeWeight(12, 100); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }

    void Outfit9(SkinnedMeshRenderer skin, GameObject brawler) //Kung Fu 
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 100); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 50); //Pelivs
        skin.SetBlendShapeWeight(12, 0); //Butt
        skin.SetBlendShapeWeight(13, 20); //Thighes
        skin.SetBlendShapeWeight(14, 100); //Calves
        skin.SetBlendShapeWeight(15, 100); //Feet
    }

    void Outfit10(SkinnedMeshRenderer skin, GameObject brawler) //Kung Fu 2 
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 100); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 0); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 15); //Feet
    }

    void Outfit11(SkinnedMeshRenderer skin, GameObject brawler) //Wrestling 1
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 0); //Waist
        skin.SetBlendShapeWeight(11, 10); //Pelivs
        skin.SetBlendShapeWeight(12, 0); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }

    void Outfit12(SkinnedMeshRenderer skin, GameObject brawler) //Wrestling 2
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 0); //Stomach
        skin.SetBlendShapeWeight(10, 0); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 0); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }
}
