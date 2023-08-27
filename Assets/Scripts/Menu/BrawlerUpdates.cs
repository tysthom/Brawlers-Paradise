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
    List<SkinnedMeshRenderer> setBrawler1Outfit = new List<SkinnedMeshRenderer>();
    List<SkinnedMeshRenderer> holdBrawler1Outfit = new List<SkinnedMeshRenderer>();
    int len = 0; //Used for getting and creating children of outfits
    public GameObject b1Outfit, b2Outfit;
    public GameObject[] outfits;
    public SkinnedMeshRenderer brawler1TargetMesh;
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

    public int b1OutfitSelection = 0;
    public int b2OutfitSelection = 0;

    [Header("Outfit Variation")]
    public int b1OutfitVariation = 1;
    public int b2OutfitVariation = 1;

    [Header("Hair Type")]
    public GameObject b1HairHolder;
    public GameObject b2HairHolder;
    GameObject b1CurrentHair;
    GameObject b2CurrentHair;
    public GameObject[] hairStyles;
    public int b1HairType = 1;
    public int b2HairType = 1;

    [Header("Hair Color")]
    public Material[] hairColors;
    public int b1HairColor = 1;
    public int b2HairColor = 1;

    [Header("Skin Color")]
    public Material[] skinColors;
    public int b1SkinColor;
    public int b2SkinColor;

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

        if (menuManagerInstance.b1OutfitSelection != b1OutfitSelection || menuManagerInstance.b2OutfitSelection != b2OutfitSelection ||
            menuManagerInstance.b1FightStyle != b1FightStyleSelection || menuManagerInstance.b2FightStyle != b2FightStyleSelection ||
            menuManagerInstance.b1OutfitVariation != b1OutfitVariation || menuManagerInstance.b2OutfitVariation != b2OutfitVariation)
        {

            for(int i = 0; i < holdBrawler1Outfit.Count; i++)
            {
                //Destroy(setBrawler1Outfit[i].gameObject);
                Destroy(holdBrawler1Outfit[i].gameObject);
            }
            setBrawler1Outfit.Clear();
            holdBrawler1Outfit.Clear();

            if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[0]) //Karate
            {
                if(menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = karateOutfit1Variations[menuManagerInstance.b1OutfitVariation-1];
                    len = 5;
                }
                else
                {
                    b1Outfit = karateOutfit2Variations[menuManagerInstance.b1OutfitVariation-1];
                    len = 4;
                }
            }
               else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[1]) //Boxing
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = boxingOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 5;
                }
                else
                {
                    b1Outfit = boxingOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 4;
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[2]) //MMA
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = mmaOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 3;
                }
                else
                {
                    b1Outfit = mmaOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 2;
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[3]) //TKD
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = taekwondoOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 4;
                }
                else
                {
                    b1Outfit = taekwondoOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 4;
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[4]) //Kung Fu
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = kungFuOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 2;
                }
                else
                {
                    b1Outfit = kungFuOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 3;
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[5]) //Wrestling
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = wrestlingOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 3;
                }
                else
                {
                    b1Outfit = wrestlingOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    len = 2;
                }
            }


            for (int i = 0; i <= len; i++) 
            {
                  setBrawler1Outfit.Add(b1Outfit.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>());
                    holdBrawler1Outfit.Add(Instantiate(setBrawler1Outfit[i])); //Assigns instaniaited versions to new array to make changes only to this version
                    holdBrawler1Outfit[i].bones = brawler1TargetMesh.bones;
                    holdBrawler1Outfit[i].rootBone = brawler1TargetMesh.rootBone;



                    if (holdBrawler1Outfit[i].GetComponent<Cloth>() != null)
                    {
                        holdBrawler1Outfit[i].GetComponent<Cloth>().enabled = true;
                    }
            }

        }

        if(b1HairType != menuManagerInstance.b1HairType || b2HairType != menuManagerInstance.b2HairType)
        {
            if (b1CurrentHair != null)
                Destroy(b1CurrentHair);
            
            if(menuManagerInstance.b1HairType != 1)
            {
                b1CurrentHair = Instantiate(hairStyles[menuManagerInstance.b1HairType-1], b1HairHolder.transform);
            }
        }

        if(b1HairColor != menuManagerInstance.b1HairColor || b2HairColor != menuManagerInstance.b2HairColor ||
            b1HairType != menuManagerInstance.b1HairType || b2HairType != menuManagerInstance.b2HairType)
        {
            if(menuManagerInstance.b1HairType != 1)
            {
                b1CurrentHair.GetComponent<MeshRenderer>().material = hairColors[menuManagerInstance.b1HairColor - 1];
            }
        }

        if(b1SkinColor != menuManagerInstance.b1SkinColor || b2SkinColor != menuManagerInstance.b2SkinColor)
        {
            brawler1TargetMesh.GetComponent<SkinnedMeshRenderer>().material = skinColors[menuManagerInstance.b1SkinColor - 1];
        }

        b1FightStyleSelection = menuManagerInstance.b1FightStyle;
        b2FightStyleSelection = menuManagerInstance.b2FightStyle;

        b1OutfitSelection = menuManagerInstance.b1OutfitSelection;
        b2OutfitSelection = menuManagerInstance.b2OutfitSelection;

        b1OutfitVariation = menuManagerInstance.b1OutfitVariation;
        b2OutfitVariation = menuManagerInstance.b2OutfitVariation;

        b1HairType = menuManagerInstance.b1HairType;
        b2HairType = menuManagerInstance.b2HairType;

        b1HairColor = menuManagerInstance.b1HairColor;
        b2HairColor = menuManagerInstance.b2HairColor;
    }
}
