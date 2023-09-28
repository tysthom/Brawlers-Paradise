using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitVariationManager : MonoBehaviour
{
    public bool usingDevTool;
    public bool useOutfitColorVariations;
    [Range(0, 10)] public int oufitColorVariationUsage;

    public GameObject[] karateOutfit1Variations;
    public GameObject[] karateOutfit2Variations;
    public GameObject[] boxingOutfit1Variations;
    public GameObject[] boxingOutfit2Variations;
    public GameObject[] mmaOutfit1Variations;
    public GameObject[] mmaOutfit2Variations;
    public GameObject[] taekwondoOutfit1Variations;
    public GameObject[] taekwondoOutfit2Variations;
    public GameObject[] kungFuOutfit1Variations;
    public GameObject[] kungFuOutfit2Variations;
    public GameObject[] wrestlingOutfit1Variations;
    public GameObject[] wrestlingOutfit2Variations;

    public GameObject OutfitVariations(FightStyle.fightStyles f, int outfitSelection, int outfitVariation)
    {
        int i = 100;
        if (f == FightStyle.fightStyles.karate)
        {
            if (usingDevTool && (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return karateOutfit1Variations[0];
            }
            else
            {
                if (!UniversalFight.usingMenuData)
                {
                    return karateOutfit1Variations[Random.Range(0, karateOutfit1Variations.Length)];
                }
                else
                {
                    if (outfitSelection == 1)
                    {
                        return karateOutfit1Variations[outfitVariation - 1];
                    }
                    else
                    {
                        return karateOutfit2Variations[outfitVariation - 1];
                    }
                }
            }
        }
        else if (f == FightStyle.fightStyles.boxing)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return boxingOutfit1Variations[0];
            }
            else
            {
                if (!UniversalFight.usingMenuData)
                {
                    return boxingOutfit2Variations[Random.Range(0, boxingOutfit2Variations.Length)];
                }
                else
                {
                    if (outfitSelection == 1)
                    {
                        return boxingOutfit1Variations[outfitVariation - 1];
                    }
                    else
                    {
                        return boxingOutfit2Variations[outfitVariation - 1];
                    }
                }
            }
        }
        else if (f == FightStyle.fightStyles.MMA)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return mmaOutfit1Variations[0];
            }
            else
            {
                if (!UniversalFight.usingMenuData)
                {
                    return mmaOutfit1Variations[Random.Range(0, mmaOutfit1Variations.Length)];
                }
                else
                {
                    if (outfitSelection == 1)
                    {
                        return mmaOutfit1Variations[outfitVariation - 1];
                    }
                    else
                    {
                        return mmaOutfit2Variations[outfitVariation - 1];
                    }
                }
            }
        }
        else if (f == FightStyle.fightStyles.taekwondo)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return taekwondoOutfit1Variations[0];
            }
            else
            {
                if (!UniversalFight.usingMenuData)
                {
                    return taekwondoOutfit1Variations[Random.Range(0, taekwondoOutfit1Variations.Length)];
                }
                else
                {
                    if (outfitSelection == 1)
                    {
                        return taekwondoOutfit1Variations[outfitVariation - 1];
                    }
                    else
                    {
                        return taekwondoOutfit2Variations[outfitVariation - 1];
                    }
                }
            }
        }
        else if (f == FightStyle.fightStyles.kungFu)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return kungFuOutfit1Variations[0];
            }
            else
            {
                if (!UniversalFight.usingMenuData)
                {
                    return kungFuOutfit1Variations[Random.Range(0, kungFuOutfit1Variations.Length)];
                }
                else
                {
                    if (outfitSelection == 1)
                    {
                        return kungFuOutfit1Variations[outfitVariation - 1];
                    }
                    else
                    {
                        return kungFuOutfit2Variations[outfitVariation - 1];
                    }
                }
            }
        }
        else if (f == FightStyle.fightStyles.proWrestling)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return wrestlingOutfit1Variations[0];
            }
            else
            {
                if (!UniversalFight.usingMenuData)
                {
                    return wrestlingOutfit1Variations[Random.Range(0, wrestlingOutfit1Variations.Length)];
                }
                else
                {
                    if (outfitSelection == 1)
                    {
                        return wrestlingOutfit1Variations[outfitVariation - 1];
                    }
                    else
                    {
                        return wrestlingOutfit2Variations[outfitVariation - 1];
                    }
                }
            }
        }
        return null;
    }
}
