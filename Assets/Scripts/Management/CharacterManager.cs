using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool assignCustomization;

    [Header("Refrences")]
    public GameObject brawler1;
    public GameObject brawler1HairHolder;
    public GameObject brawler1Skin;
    public GameObject brawler2;
    public GameObject brawler2HairHolder;
    public GameObject brawler2Skin;
    public GameObject tourist;
    public GameObject touristHairHolder;
    public GameObject touristSkin;

    [Header("Hair")]
    public int brawler1HairSelection;
    public int brawler2HairSelection;
    public GameObject[] hairTypes;
    GameObject currentbrawler1HairType;
    GameObject currentbrawler2HairType;
    GameObject currentTouristHairType;

    public int brawler1HairColorSelction;
    public int brawler2HairColorSelction;
    public Material[] hairColor;

    [Header("Skin")]
    public int brawler1SkinSelction;
    public int brawler2SkinSelction;
    public Material[] skinColor;

    [Header("Outfits")]
    public bool useAppropriateOutfits;
    public int brawler1OutfitSelection;
    public int brawler2OutfitSelection;
    public GameObject brawler1Outfit;
    public GameObject brawler2Outfit;
    public SkinnedMeshRenderer brawler1TargetMesh;
    public SkinnedMeshRenderer brawler2TargetMesh;
    public SkinnedMeshRenderer touristTargetMesh;
    public enum outfitParts { };
    public GameObject[] outfits;

    private void Start()
    {
        if (assignCustomization)
        {
           /* if (GetComponent<IdManagear>().gameMode == IdManagear.mode.playerVsAi || GetComponent<IdManagear>().gameMode == IdManagear.mode.training)
            {
                brawler1 = GetComponent<IdManagear>().brawler1;
                brawler2 = GetComponent<IdManagear>().brawler2;
            }
            else if (GetComponent<IdManagear>().gameMode == IdManagear.mode.AiVsAi)
            {
                brawler1 = GetComponent<IdManagear>().brawler1;
                brawler2 = GetComponent<IdManagear>().brawler2;
            } */

            Transform[] brawler1Children = brawler1.GetComponentsInChildren<Transform>();
            foreach (Transform child in brawler1Children)
            {
                if (child.gameObject.name == "Hair Holder")
                {
                    brawler1HairHolder = child.gameObject;
                }
            }

            Transform[] brawler2Children = brawler2.GetComponentsInChildren<Transform>();
            foreach (Transform child in brawler2Children)
            {
                if (child.gameObject.name == "Hair Holder")
                {
                    brawler2HairHolder = child.gameObject;
                }
            }



            for (int i = 0; i < brawler1.transform.childCount; i++)
            {
                if (brawler1.transform.GetChild(i).gameObject.name == "Skin")
                {
                    brawler1Skin = brawler1.transform.GetChild(i).gameObject;
                    i = brawler1.transform.childCount;
                }
            }

            for (int i = 0; i < brawler2.transform.childCount; i++)
            {
                if (brawler2.transform.GetChild(i).gameObject.name == "Skin")
                {
                    brawler2Skin = brawler2.transform.GetChild(i).gameObject;
                    i = brawler2.transform.childCount;
                }
            }

            //Assigns skinned mesh renders from instantiated prefabs
            brawler1TargetMesh = brawler1Skin.GetComponent<SkinnedMeshRenderer>();
            brawler2TargetMesh = brawler2Skin.GetComponent<SkinnedMeshRenderer>();

            StartCoroutine(Assign());
            //brawler1.GetComponent<Animator>().enabled = true;
        }
    }

    IEnumerator Assign()
    {
        yield return new WaitForSeconds(.01f);
        AssignHairType();
        AssignHairColor();
        AssignSkinColor();
        AssignOutfit();
    }

    void AssignHairType()
    {
        if (hairTypes[brawler1HairSelection] != null)
        {
            currentbrawler1HairType = Instantiate(hairTypes[brawler1HairSelection], brawler1HairHolder.transform);
        }
        if (hairTypes[brawler2HairSelection] != null)
        {
            currentbrawler2HairType = Instantiate(hairTypes[brawler2HairSelection], brawler2HairHolder.transform);
        }
    }

    void AssignHairColor()
    {
        if(currentbrawler1HairType != null)
        {
            currentbrawler1HairType.GetComponent<MeshRenderer>().material = hairColor[brawler1HairColorSelction];
        }
        if (currentbrawler2HairType != null)
        {
            currentbrawler2HairType.GetComponent<MeshRenderer>().material = hairColor[brawler2HairColorSelction];
        }
    }

    void AssignSkinColor()
    {
        brawler1Skin.GetComponent<SkinnedMeshRenderer>().material = skinColor[brawler1SkinSelction];
        brawler2Skin.GetComponent<SkinnedMeshRenderer>().material = skinColor[brawler2SkinSelction];
    }

    void AssignOutfit()
    {
        SkinnedMeshRenderer[] setBrawler1Outfit = new SkinnedMeshRenderer[50];
        SkinnedMeshRenderer[] holdBrawler1Outfit = null;
        if (useAppropriateOutfits)
        {    
            if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
            {
                brawler1OutfitSelection = Random.Range(1, 2);
                brawler1Outfit = GetComponent<OutfitVariationManager>().OutfitVariations(FightStyle.fightStyles.karate,
                    brawler1OutfitSelection);
                holdBrawler1Outfit = new SkinnedMeshRenderer[brawler1Outfit.transform.childCount];

            } else if(brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
            {
                brawler1OutfitSelection = Random.Range(2, 3);
                brawler1Outfit = GetComponent<OutfitVariationManager>().OutfitVariations(FightStyle.fightStyles.boxing,
                    brawler1OutfitSelection);
                holdBrawler1Outfit = new SkinnedMeshRenderer[outfits[brawler1OutfitSelection].transform.childCount];
            } else if (brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
            {
                brawler1OutfitSelection = Random.Range(4, 6);
                holdBrawler1Outfit = new SkinnedMeshRenderer[outfits[brawler1OutfitSelection].transform.childCount];
            }
            else if (brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
            {
                brawler1OutfitSelection = Random.Range(6, 8);
                holdBrawler1Outfit = new SkinnedMeshRenderer[outfits[brawler1OutfitSelection].transform.childCount];
            }
            else if (brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
            {
                brawler1OutfitSelection = Random.Range(8, 10);
                holdBrawler1Outfit = new SkinnedMeshRenderer[outfits[brawler1OutfitSelection].transform.childCount];
            }
            else if (brawler1.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
            {
                brawler1OutfitSelection = Random.Range(10, 12);
                holdBrawler1Outfit = new SkinnedMeshRenderer[outfits[brawler1OutfitSelection].transform.childCount];
            } 
        }
        else
        {
            holdBrawler1Outfit = new SkinnedMeshRenderer[outfits[brawler1OutfitSelection].transform.childCount]; //New array to hold insantiated versions of prefabs without changing original prefab
        }
        
        for (int i = 0; i< outfits[brawler1OutfitSelection].transform.childCount; i++)
        {
            setBrawler1Outfit[i] = brawler1Outfit.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
            holdBrawler1Outfit[i] = Instantiate<SkinnedMeshRenderer>(setBrawler1Outfit[i]); //Assigns instaniaited versions to new array to make changes only to this version

            holdBrawler1Outfit[i].bones = brawler1TargetMesh.bones;
            holdBrawler1Outfit[i].rootBone = brawler1TargetMesh.rootBone;

            if(holdBrawler1Outfit[i].GetComponent<Cloth>() != null)
            {
                holdBrawler1Outfit[i].GetComponent<Cloth>().enabled = true;
            }
            
        }

        GetComponent<BlendShapeManager>().AssignBlendShape(brawler1TargetMesh, brawler1OutfitSelection, brawler1);


        SkinnedMeshRenderer[] setBrawler2Outfit = new SkinnedMeshRenderer[50];
        SkinnedMeshRenderer[] holdBrawler2Outfit;

        if (useAppropriateOutfits)
        {
            if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
            {
                brawler2OutfitSelection = Random.Range(0, 2);
            }
            else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
            {
                brawler2OutfitSelection = Random.Range(2, 4);
            }
            else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.MMA)
            {
                brawler2OutfitSelection = Random.Range(4, 6);
            }
            else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.taekwondo)
            {
                brawler2OutfitSelection = Random.Range(6, 8);
            }
            else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
            {
                brawler2OutfitSelection = Random.Range(8, 10);
            }
            else if (brawler2.GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.proWrestling)
            {
                brawler2OutfitSelection = Random.Range(10, 12);
            }
        }

        holdBrawler2Outfit = new SkinnedMeshRenderer[outfits[brawler2OutfitSelection].transform.childCount];


        for (int i = 0; i < outfits[brawler2OutfitSelection].transform.childCount; i++)
        {
            setBrawler2Outfit[i] = outfits[brawler2OutfitSelection].transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
            holdBrawler2Outfit[i] = Instantiate<SkinnedMeshRenderer>(outfits[brawler2OutfitSelection].transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()); //Assigns instaniaited versions to new array to make changes only to this version

            holdBrawler2Outfit[i].bones = brawler2TargetMesh.bones;
            holdBrawler2Outfit[i].rootBone = brawler2TargetMesh.rootBone;

            if (holdBrawler2Outfit[i].GetComponent<Cloth>() != null)
            {
                holdBrawler2Outfit[i].GetComponent<Cloth>().enabled = true;
            }
        }

        GetComponent<BlendShapeManager>().AssignBlendShape(brawler2TargetMesh, brawler2OutfitSelection, brawler2);
    }

    public void AssignTourist()
    {
        for (int i = 0; i < tourist.transform.childCount; i++)
        {
            if (tourist.transform.GetChild(i).gameObject.name == "Skin")
            {
                touristSkin = tourist.transform.GetChild(i).gameObject;
                i = tourist.transform.childCount;
            }
        }
        touristTargetMesh = touristSkin.GetComponent<SkinnedMeshRenderer>();

        Transform[] touristChildren = tourist.GetComponentsInChildren<Transform>();
        foreach (Transform child in touristChildren)
        {
            if (child.gameObject.name == "Hair Holder")
            {
                touristHairHolder = child.gameObject;
            }
        }

        int a = Random.Range(0, hairTypes.Length); //Assigns hair type
        if(a != 0)
        {
            currentTouristHairType = Instantiate(hairTypes[a], touristHairHolder.transform);
            currentTouristHairType.GetComponent<MeshRenderer>().material = hairColor[Random.Range(0,hairColor.Length)]; //Assigns hair color
        }

        touristSkin.GetComponent<SkinnedMeshRenderer>().material = skinColor[Random.Range(0, skinColor.Length)]; //Assigns skin color

        SkinnedMeshRenderer setTouristOutfit = Instantiate<SkinnedMeshRenderer>(outfits[Random.Range(0, outfits.Length-1)].GetComponent<SkinnedMeshRenderer>());
        setTouristOutfit.transform.parent = touristTargetMesh.transform;

        setTouristOutfit.bones = touristTargetMesh.bones;
        setTouristOutfit.rootBone = touristTargetMesh.rootBone;
    }

    
}
