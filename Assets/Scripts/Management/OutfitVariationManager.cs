using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitVariationManager : MonoBehaviour
{
    public GameObject[] karateOutfit1Variations;
    public GameObject[] karateOutfit2Variations;

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
            } else
            {
                return karateOutfit2Variations[Random.Range(0, karateOutfit2Variations.Length)];
            }
    }
}
