using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SouvenirsManager : MonoBehaviour
{
    GameObject gameManager;
    IdManagear idManagerInstance;
    public bool souvenirsAllowed;

    [Header("Medicine")]
    public float healthBoostAmount = 50;
    public float medicineCooldownTime = 15;

    [Header("Sunscreen")]
    public float shieldAmount = 300;
    public float sunscreenCooldownTime = 30;

    [Header("Coffee")]
    public float speedMultiplier;
    public float speedBoostDuration = 5;
    public float coffeeCooldownTime = 15;

    [Header("Briefcase")]
    public GameObject briefcasePrefab;
    public float briefcaseCooldownTime = 25;

    [Header("Life Jacket")]
    public float ljMinHealthAmount = .1f; //10%
    public float ljDuration = 5;
    public float lifeJacketCooldownTime = 50;

    [Header("Rat Poison")]
    public float rpDamageAmount = 15;
    public float rpDuration = 5;
    public float ratPoisonCooldownTime = 30;

    [Header("Tequila")]
    public float tDamageMultiplier = 1.5f;
    public float tDuration = 10;
    public float tequilaCooldownTime = 20;

    [Header("VIP Card")]
    public float vipcHealthRecoveryAmount = 20;
    public float vipcDuration = 10;
    public float vipcCooldownTime = 25;

    [Header("Floaty")]
    public float fDamageReductionPercentage = .5f;
    public float fDuration = 10;
    public float fCooldownTime = 15;

    [Header("Ai Management")]
    [Range(0, 10)] public int medicineUsage;
    [Range(0, 10)] public int sunscreenUsage;
    [Range(0, 10)] public int coffeeUsage;
    [Range(0, 10)] public int briefcaseUsage;
    [Range(0, 10)] public int lifeJacketUsage;
    [Range(0, 10)] public int ratPoisonUsage;
    [Range(0, 10)] public int tequilaUsage;
    [Range(0, 10)] public int vipCadUsage;
    [Range(0, 10)] public int floatyUsage;

    [Header("Souvenir Images")]
    public Sprite medicineImage;
    public Sprite sunscreenImage;
    public Sprite coffeeImage;
    public Sprite briefcaseImage;
    public Sprite lifeJacketImage;
    public Sprite ratPoisonImage;
    public Sprite tequilaImage;
    public Sprite vipCardImage;
    public Sprite floatyImage;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
    }

    private void Start()
    {
        if (souvenirsAllowed) //Assigns random souvenir if no souvenir is assigned already
        {
            if(idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.none)
            {
                AssignRandomSouvenir(idManagerInstance.brawler1);
                AssignSouvenirIcon(idManagerInstance.brawler1);
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.none)
            {
                AssignRandomSouvenir(idManagerInstance.brawler2);
                AssignSouvenirIcon(idManagerInstance.brawler2);
            }
        }

        
    }

    void AssignRandomSouvenir(GameObject brawler)
    {
        int i = Random.Range(0, System.Enum.GetValues(typeof(Souvenirs.souvenirs)).Length - 1);
        brawler.GetComponent<Souvenirs>().souvenir = (Souvenirs.souvenirs)i;
    }

    public void AssignSouvenirIcon(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1 && idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir != Souvenirs.souvenirs.none)
        {
            
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.medicine)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = medicineImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.sunscreen)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = sunscreenImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.coffee)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = coffeeImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.briefcase)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = briefcaseImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.lifeJacket)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = lifeJacketImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.ratPoison)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = ratPoisonImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.tequila)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = tequilaImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.vipCard)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = vipCardImage;
            }
            if (idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.floaty)
            {
                idManagerInstance.brawler1.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = floatyImage;
            }
        }
        else 
        if (brawler == idManagerInstance.brawler2 && idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir != Souvenirs.souvenirs.none)
        {
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.medicine)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = medicineImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.sunscreen)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = sunscreenImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.coffee)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = coffeeImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.briefcase)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = briefcaseImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.lifeJacket)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = lifeJacketImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.ratPoison)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = ratPoisonImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.tequila)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = tequilaImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.vipCard)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = vipCardImage;
            }
            if (idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenir == Souvenirs.souvenirs.floaty)
            {
                idManagerInstance.brawler2.GetComponent<Souvenirs>().souvenirIcon.GetComponent<Image>().sprite = floatyImage;
            }
        }
    }
}
