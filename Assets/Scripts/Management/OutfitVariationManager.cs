using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitVariationManager : MonoBehaviour
{
    public GameObject[] karateOutfit1Variations;

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
        if(f == FightStyle.fightStyles.karate)
        {
            if(outfitSelection == 0)
            {
                return karateOutfit1Variations[Random.Range(0, karateOutfit1Variations.Length)];
            } else
            {
                return null;
            }
        }

        return null;
    }
}
