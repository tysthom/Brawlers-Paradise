using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlerUpdates : MonoBehaviour
{
    GameObject menuManager;
    MenuManager menuManagerInstance;
    public GameObject brawler1, brawler2;

    [Header("Fight Style Selection")]
    public RuntimeAnimatorController[] fightingTypeAnimators;
    int b1FightStyleSelection = 0;
    int b2FightStyleSelection = 0;

    [Header("Outfit Selection")]
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

    int b1OutfitSelection = 0;
    int b2OutfitSelection = 0;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("Menu Manager");
        menuManagerInstance = menuManager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuManagerInstance.b1FightStyle != b1FightStyleSelection || menuManagerInstance.b2FightStyle != b2FightStyleSelection)
        {
            brawler1.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[menuManagerInstance.b1FightStyle];
        }

        if (menuManagerInstance.b1OutfitSelection != b1OutfitSelection || menuManagerInstance.b2OutfitSelection != b2OutfitSelection)
        {
            //Update Outfits
        }

        b1OutfitSelection = menuManagerInstance.b1FightStyle;
        b2OutfitSelection = menuManagerInstance.b2FightStyle;
    }
}
