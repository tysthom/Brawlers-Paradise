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
    int b1FightStyleSelection = -1;
    int b2FightStyleSelection = -1;

    [Header("Outfit Selection")]
    List<SkinnedMeshRenderer> setBrawler1Outfit = new List<SkinnedMeshRenderer>();
    List<SkinnedMeshRenderer> holdBrawler1Outfit = new List<SkinnedMeshRenderer>();
    List<SkinnedMeshRenderer> setBrawler2Outfit = new List<SkinnedMeshRenderer>();
    List<SkinnedMeshRenderer> holdBrawler2Outfit = new List<SkinnedMeshRenderer>();
    int lenb1 = 0; //Used for getting and creating children of outfits
    int lenb2 = 0;
    public GameObject b1Outfit, b2Outfit;
    public GameObject[] outfits;
    public SkinnedMeshRenderer brawler1TargetMesh;
    public SkinnedMeshRenderer brawler2TargetMesh;
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

    [Header("Names")]
    public string[] names;

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
            brawler2.GetComponent<Animator>().runtimeAnimatorController = fightingTypeAnimators[menuManagerInstance.b2FightStyle];
        }

        if (menuManagerInstance.b1OutfitSelection != b1OutfitSelection || menuManagerInstance.b2OutfitSelection != b2OutfitSelection ||
            menuManagerInstance.b1FightStyle != b1FightStyleSelection || menuManagerInstance.b2FightStyle != b2FightStyleSelection ||
            menuManagerInstance.b1OutfitVariation != b1OutfitVariation || menuManagerInstance.b2OutfitVariation != b2OutfitVariation)
        {



            #region Brawler1 Outfit Selection
            for (int i = 0; i < holdBrawler1Outfit.Count; i++)
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
                    lenb1 = 5;
                    Outfit1(brawler1TargetMesh);
                }
                else
                {
                    b1Outfit = karateOutfit2Variations[menuManagerInstance.b1OutfitVariation-1];
                    lenb1 = 4;
                    Outfit2(brawler1TargetMesh);
                }
            }
               else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[1]) //Boxing
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = boxingOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 5;
                    Outfit3(brawler1TargetMesh, brawler1);
                }
                else
                {
                    b1Outfit = boxingOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 4;
                    Outfit4(brawler1TargetMesh, brawler1);
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[2]) //MMA
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = mmaOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 3;
                    Outfit5(brawler1TargetMesh);
                }
                else
                {
                    b1Outfit = mmaOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 2;
                    Outfit6(brawler1TargetMesh);
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[3]) //TKD
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = taekwondoOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 4;
                    Outfit7(brawler1TargetMesh);
                }
                else
                {
                    b1Outfit = taekwondoOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 4;
                    Outfit8(brawler1TargetMesh);
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[4]) //Kung Fu
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = kungFuOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 2;
                    Outfit9(brawler1TargetMesh);
                }
                else
                {
                    b1Outfit = kungFuOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 3;
                    Outfit10(brawler1TargetMesh);
                }
            }
            else if (brawler1.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[5]) //Wrestling
            {
                if (menuManagerInstance.b1OutfitSelection == 1)
                {
                    b1Outfit = wrestlingOutfit1Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 3;
                    Outfit11(brawler1TargetMesh);
                }
                else
                {
                    b1Outfit = wrestlingOutfit2Variations[menuManagerInstance.b1OutfitVariation - 1];
                    lenb1 = 2;
                    Outfit12(brawler1TargetMesh);
                }
            }

            for (int i = 0; i <= lenb1; i++)
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
            #endregion

            #region Brawler2 Outfit Selection
            for (int i = 0; i < holdBrawler2Outfit.Count; i++)
            {
                //Destroy(setBrawler1Outfit[i].gameObject);
                Destroy(holdBrawler2Outfit[i].gameObject);
            }
            setBrawler2Outfit.Clear();
            holdBrawler2Outfit.Clear();

            if (brawler2.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[0]) //Karate
            {
                if (menuManagerInstance.b2OutfitSelection == 1)
                {
                    b2Outfit = karateOutfit1Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 5;
                    Outfit1(brawler2TargetMesh);
                }
                else
                {
                    b2Outfit = karateOutfit2Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 4;
                    Outfit2(brawler2TargetMesh);
                }
            }
            else if (brawler2.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[1]) //Boxing
            {
                if (menuManagerInstance.b2OutfitSelection == 1)
                {
                    b2Outfit = boxingOutfit1Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 5;
                    Outfit3(brawler2TargetMesh, brawler2);
                }
                else
                {
                    b2Outfit = boxingOutfit2Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 4;
                    Outfit4(brawler2TargetMesh, brawler2);
                }
            }
            else if (brawler2.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[2]) //MMA
            {
                if (menuManagerInstance.b2OutfitSelection == 1)
                {
                    b2Outfit = mmaOutfit1Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 3;
                    Outfit5(brawler2TargetMesh);
                }
                else
                {
                    b2Outfit = mmaOutfit2Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 2;
                    Outfit6(brawler2TargetMesh);
                }
            }
            else if (brawler2.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[3]) //TKD
            {
                if (menuManagerInstance.b2OutfitSelection == 1)
                {
                    b2Outfit = taekwondoOutfit1Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 4;
                    Outfit7(brawler2TargetMesh);
                }
                else
                {
                    b2Outfit = taekwondoOutfit2Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 4;
                    Outfit8(brawler2TargetMesh);
                }
            }
            else if (brawler2.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[4]) //Kung Fu
            {
                if (menuManagerInstance.b2OutfitSelection == 1)
                {
                    b2Outfit = kungFuOutfit1Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 2;
                    Outfit9(brawler2TargetMesh);
                }
                else
                {
                    b2Outfit = kungFuOutfit2Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 3;
                    Outfit10(brawler2TargetMesh);
                }
            }
            else if (brawler2.GetComponent<Animator>().runtimeAnimatorController == fightingTypeAnimators[5]) //Wrestling
            {
                if (menuManagerInstance.b2OutfitSelection == 1)
                {
                    b2Outfit = wrestlingOutfit1Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 3;
                    Outfit11(brawler2TargetMesh);
                }
                else
                {
                    b2Outfit = wrestlingOutfit2Variations[menuManagerInstance.b2OutfitVariation - 1];
                    lenb2 = 2;
                    Outfit12(brawler2TargetMesh);
                }
            }

            for (int i = 0; i <= lenb2; i++)
            {
                setBrawler2Outfit.Add(b2Outfit.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>());
                holdBrawler2Outfit.Add(Instantiate(setBrawler2Outfit[i])); //Assigns instaniaited versions to new array to make changes only to this version
                holdBrawler2Outfit[i].bones = brawler2TargetMesh.bones;
                holdBrawler2Outfit[i].rootBone = brawler2TargetMesh.rootBone;

                if (holdBrawler2Outfit[i].GetComponent<Cloth>() != null)
                {
                    holdBrawler2Outfit[i].GetComponent<Cloth>().enabled = true;
                }
            }
            #endregion
        }

        if(b1HairType != menuManagerInstance.b1HairType || b2HairType != menuManagerInstance.b2HairType)
        {
            if (b1CurrentHair != null)
                Destroy(b1CurrentHair);
            
            if(menuManagerInstance.b1HairType != 1)
            {
                b1CurrentHair = Instantiate(hairStyles[menuManagerInstance.b1HairType-1], b1HairHolder.transform);
            }

            if (b2CurrentHair != null)
                Destroy(b2CurrentHair);

            if (menuManagerInstance.b2HairType != 1)
            {
                b2CurrentHair = Instantiate(hairStyles[menuManagerInstance.b2HairType - 1], b2HairHolder.transform);
            }
        }

        if(b1HairColor != menuManagerInstance.b1HairColor || b2HairColor != menuManagerInstance.b2HairColor ||
            b1HairType != menuManagerInstance.b1HairType || b2HairType != menuManagerInstance.b2HairType)
        {
            if(menuManagerInstance.b1HairType != 1)
            {
                b1CurrentHair.GetComponent<MeshRenderer>().material = hairColors[menuManagerInstance.b1HairColor - 1];
            }

            if (menuManagerInstance.b2HairType != 1)
            {
                b2CurrentHair.GetComponent<MeshRenderer>().material = hairColors[menuManagerInstance.b2HairColor - 1];
            }
        }

        if(b1SkinColor != menuManagerInstance.b1SkinColor || b2SkinColor != menuManagerInstance.b2SkinColor)
        {
            brawler1TargetMesh.GetComponent<SkinnedMeshRenderer>().material = skinColors[menuManagerInstance.b1SkinColor - 1];

            brawler2TargetMesh.GetComponent<SkinnedMeshRenderer>().material = skinColors[menuManagerInstance.b2SkinColor - 1];
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
        brawler.GetComponent<Animator>().SetLayerWeight(1, 1);

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
        brawler.GetComponent<Animator>().SetLayerWeight(1, 1);

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
