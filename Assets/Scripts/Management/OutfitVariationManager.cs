using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitVariationManager : MonoBehaviour
{
    public GameObject[] karateOutfit1Variations;
    public GameObject[] karateOutfit2Variations;
    public GameObject[] boxingOutfit1Variations;
    public GameObject[] boxingOutfit2Variations;
    public GameObject[] mmaOutfit1Variations;
    public GameObject[] mmaOutfit2Variations;
    public GameObject[] taekwondoOutfit1Variations;
    public GameObject[] taekwondoOutfit2Variations;

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
        if(outfitSelection == 0)
        {
            return karateOutfit1Variations[Random.Range(0, karateOutfit1Variations.Length)];
        } else if (outfitSelection == 1)
        {
            return karateOutfit2Variations[Random.Range(0, karateOutfit2Variations.Length)];
        }
        else if (outfitSelection == 2)
        {
            return boxingOutfit1Variations[Random.Range(0, boxingOutfit1Variations.Length)];
        }
        else if (outfitSelection == 3)
        {
            return boxingOutfit2Variations[Random.Range(0, boxingOutfit2Variations.Length)];
        }
        else if (outfitSelection == 4)
        {
            return mmaOutfit1Variations[Random.Range(0, mmaOutfit1Variations.Length)];
        }
        else if (outfitSelection == 5)
        {
            return mmaOutfit2Variations[Random.Range(0, mmaOutfit2Variations.Length)];
        }
        else if (outfitSelection == 6)
        {
            return taekwondoOutfit1Variations[Random.Range(0, taekwondoOutfit1Variations.Length)];
        }
        else if (outfitSelection == 7)
        {
            return taekwondoOutfit2Variations[Random.Range(0, taekwondoOutfit2Variations.Length)];
        }

        return null;
    }
}
