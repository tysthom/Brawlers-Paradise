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
    public List<SkinnedMeshRenderer> setBrawler1Outfit = new List<SkinnedMeshRenderer>();
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
                    b1Outfit = karateOutfit1Variations[0];
                    len = 5;
                }
                else
                {
                    b1Outfit = karateOutfit2Variations[0];
                    len = 4;
                }
            }


            for (int i = 0; i <= len; i++) 
            {
                Debug.Log(i + " is " + b1Outfit.transform.GetChild(i).name);    

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

        b1FightStyleSelection = menuManagerInstance.b1FightStyle;
        b2FightStyleSelection = menuManagerInstance.b2FightStyle;

        b1OutfitSelection = menuManagerInstance.b1OutfitSelection;
        b2OutfitSelection = menuManagerInstance.b2OutfitSelection;

    }
}
