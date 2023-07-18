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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject OutfitVariations(FightStyle.fightStyles f, int outfitSelection)
    {
        int i = Random.Range(0, 11);
        if(outfitSelection == 0)
        {
            if (usingDevTool && (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return karateOutfit1Variations[0];
            }
            else
            {
                return karateOutfit1Variations[Random.Range(0, karateOutfit1Variations.Length)];
            }

        } else if (outfitSelection == 1)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return karateOutfit2Variations[0];
            }
            else
            {
                return karateOutfit2Variations[Random.Range(0, karateOutfit2Variations.Length)];
            }
        }
        else if (outfitSelection == 2)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return boxingOutfit1Variations[0];
            }
            else
            {
                return boxingOutfit1Variations[Random.Range(0, boxingOutfit1Variations.Length)];
            }
        }
        else if (outfitSelection == 3)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return boxingOutfit2Variations[0];
            }
            else
            {
                return boxingOutfit2Variations[Random.Range(0, boxingOutfit2Variations.Length)];
            }
        }
        else if (outfitSelection == 4)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return mmaOutfit1Variations[0];
            }
            else
            {
                return mmaOutfit1Variations[Random.Range(0, mmaOutfit1Variations.Length)];
            }
        }
        else if (outfitSelection == 5)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return mmaOutfit2Variations[0];
            }
            else
            {
                return mmaOutfit2Variations[Random.Range(0, mmaOutfit2Variations.Length)];
            }
        }
        else if (outfitSelection == 6)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return taekwondoOutfit1Variations[0];
            }
            else
            {
                return taekwondoOutfit1Variations[Random.Range(0, taekwondoOutfit1Variations.Length)];
            }
        }
        else if (outfitSelection == 7)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return taekwondoOutfit2Variations[0];
            }
            else
            {
                return taekwondoOutfit2Variations[Random.Range(0, taekwondoOutfit2Variations.Length)];
            }
        }
        else if (outfitSelection == 8)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return kungFuOutfit1Variations[0];
            }
            else
            {
                return kungFuOutfit1Variations[Random.Range(0, kungFuOutfit1Variations.Length)];
            }
        }
        else if (outfitSelection == 9)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return kungFuOutfit2Variations[0];
            }
            else
            {
                return kungFuOutfit2Variations[Random.Range(0, kungFuOutfit2Variations.Length)];
            }
        }
        else if (outfitSelection == 10)
        {
            if (usingDevTool && (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return wrestlingOutfit1Variations[0];
            }
            else
            {
                return wrestlingOutfit1Variations[Random.Range(0, wrestlingOutfit1Variations.Length)];
            }
        }
        else if (outfitSelection == 11)
        {
            if (usingDevTool || (!useOutfitColorVariations || i < oufitColorVariationUsage))
            {
                return wrestlingOutfit2Variations[0];
            }
            else
            {
                return wrestlingOutfit2Variations[Random.Range(0, wrestlingOutfit2Variations.Length)];
            }
        }

        return null;
    }
}
