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
        if(brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            if(outfitSelection == 1)
            {
                Outfit1(skin);
            }
            else
            {
                Outfit2(skin);
            }
        } else if (brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
        {
            if (outfitSelection == 1)
            {
                Outfit3(skin, brawler);
            }
            else
            {
                Outfit4(skin, brawler);
            }
        }
        else if (brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
        {
            if (outfitSelection == 1)
            {
                Outfit5(skin);
            }
            else
            {
                Outfit6(skin);
            }
        }
        else if (brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
        {
            if (outfitSelection == 1)
            {
                Outfit7(skin);
            }
            else
            {
                Outfit8(skin);
            }
        }
        else if (brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
        {
            if (outfitSelection == 1)
            {
                Outfit9(skin);
            }
            else
            {
                Outfit10(skin);
            }
        }
        else if (brawler.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
        {
            if (outfitSelection == 1)
            {
                Outfit11(skin);
            }
            else
            {
                Outfit12(skin);
            }
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

    void Outfit1(SkinnedMeshRenderer skin) //Karate Outfit 1
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
    void Outfit2(SkinnedMeshRenderer skin) //Karate Outfit 2
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
    void Outfit3(SkinnedMeshRenderer skin, GameObject brawler) //Boxing Outfit 1
    {
        brawler.GetComponent<Combat>().anim.SetLayerWeight(1, 1);

        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 25); //Stomach
        skin.SetBlendShapeWeight(10, 50); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 75); //Butt
        skin.SetBlendShapeWeight(13, 30); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 50); //Feet
    }

    void Outfit4(SkinnedMeshRenderer skin, GameObject brawler) //Boxing Outfit 2
    {
        brawler.GetComponent<Combat>().anim.SetLayerWeight(1, 1);

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

    void Outfit5(SkinnedMeshRenderer skin) //MMA Outfit 1
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

    void Outfit6(SkinnedMeshRenderer skin) //MMA Outfit 2
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

    void Outfit7(SkinnedMeshRenderer skin) //TKD Outfit 1
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

    void Outfit8(SkinnedMeshRenderer skin) //TKD Outfit 2
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 20); //Chest
        skin.SetBlendShapeWeight(8, 0); //Back
        skin.SetBlendShapeWeight(9, 100); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 100); //Pelivs
        skin.SetBlendShapeWeight(12, 100); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 0); //Feet
    }

    void Outfit9(SkinnedMeshRenderer skin) //Kung Fu 
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
        skin.SetBlendShapeWeight(15, 100); //Feet
    }

    void Outfit10(SkinnedMeshRenderer skin) //Kung Fu 2 
    {
        skin.SetBlendShapeWeight(0, 0); //Neck
        skin.SetBlendShapeWeight(1, 0); //Right Shoulder
        skin.SetBlendShapeWeight(2, 0); //Left Shoulder
        skin.SetBlendShapeWeight(3, 0); //Right Upper Arm
        skin.SetBlendShapeWeight(4, 0); //Left Upper Arm
        skin.SetBlendShapeWeight(5, 0); //Right Lower Arm
        skin.SetBlendShapeWeight(6, 0); //Left Lower Arm
        skin.SetBlendShapeWeight(7, 0); //Chest
        skin.SetBlendShapeWeight(8, 75); //Back
        skin.SetBlendShapeWeight(9, 100); //Stomach
        skin.SetBlendShapeWeight(10, 100); //Waist
        skin.SetBlendShapeWeight(11, 0); //Pelivs
        skin.SetBlendShapeWeight(12, 0); //Butt
        skin.SetBlendShapeWeight(13, 0); //Thighes
        skin.SetBlendShapeWeight(14, 0); //Calves
        skin.SetBlendShapeWeight(15, 15); //Feet
    }

    void Outfit11(SkinnedMeshRenderer skin) //Wrestling 1
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

    void Outfit12(SkinnedMeshRenderer skin) //Wrestling 2
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
