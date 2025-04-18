using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Souvenirs : MonoBehaviour
{
    [Header("Refrences")]
    public Image souvenirDisplay;
    public Image souvenirFill;
    public Image souvenirIcon;
    GameObject gameManager;
    ResultStats resultStatsInstance;
    GameObject souvenirsManager;
    SouvenirsManager souvenirsManagerInstance;
    GameObject hudManager;
    HUDManager hudManagerInstance;
    IdManagear idManagerInstance;
    GameObject particleManager;
    ParticleManager particleManagerInstance;

    public enum souvenirs {medicine, sunscreen, coffee, briefcase, ratPoison, lifeJacket, tequila, vipCard, floaty, none};

    [Header("Status")]
    public bool canUseSouvenir;
    public bool onCooldown;
    public bool hasArmour;
    public bool hasSpeedBoost;
    public bool hasPoison;
    public bool hasDamageBoost;
    public bool hasLifeSteal;
    public bool hasDamageReduction;

    [Header("Souvenirs")]
    public souvenirs souvenir;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        resultStatsInstance = gameManager.GetComponent<ResultStats>();
        souvenirsManager = GameObject.Find("Souvenir Manager");
        souvenirsManagerInstance = souvenirsManager.GetComponent<SouvenirsManager>();
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        particleManager = GameObject.Find("Particle Manager");
        particleManagerInstance = particleManager.GetComponent<ParticleManager>();

        canUseSouvenir = true;
    }

    private void Start()
    {
        if(hudManagerInstance.hudType == HUDManager.hud.classic)
        {
            GameObject mainCanvas = GameObject.Find("Main Canvas");

            Image[] mainCanvasChildren = mainCanvas.GetComponentsInChildren<Image>();
            foreach (Image child in mainCanvasChildren)
            {
                if (idManagerInstance.brawler1 == gameObject)
                {
                    if (child.gameObject.name == "Brawler1 Souvenir Display")
                    {
                        souvenirDisplay = child;
                    }
                    if (child.gameObject.name == "Brawler1 Souvenir Icon")
                    {
                        souvenirIcon = child;
                    }
                }
                else if (idManagerInstance.brawler2 == gameObject)
                {
                    if (child.gameObject.name == "Brawler2 Souvenir Display")
                    {
                        souvenirDisplay = child;
                    }
                    if (child.gameObject.name == "Brawler2 Souvenir Icon")
                    {
                        souvenirIcon = child;
                    }
                }
            }

                Image[] souveDisplay = souvenirDisplay.GetComponentsInChildren<Image>();
                foreach(Image subchild in souveDisplay)
                {
                    if(subchild.name == "Souvenir Fill")
                    {
                        souvenirFill = subchild;
                    }
                }
        } 
        else if (hudManagerInstance.hudType == HUDManager.hud.minimalist)
        {
            Image[] children = gameObject.GetComponentsInChildren<Image>();
            foreach (Image child in children)
            {
                if (child.gameObject.name == "Souvenir Fill")
                {
                    souvenirFill = child;
                }
                if (idManagerInstance.brawler1 == gameObject)
                {
                    if (child.gameObject.name == "Souvenir Icon")
                    {
                        souvenirIcon = child;
                    }
                } else if (idManagerInstance.brawler2 == gameObject)
                {
                    if (child.gameObject.name == "Souvenir Icon")
                    {
                        souvenirIcon = child;
                    }
                }
            }
        }

        if (souvenirFill != null)
            souvenirFill.fillAmount = 1;
    }

    private void Update()
    {
        if(souvenir == souvenirs.briefcase && GetComponent<Throw>().hasThrowable || GetComponent<Flinch>().isReacting || onCooldown) //Makes sure user doesn't already have a throwable
        {
            canUseSouvenir = false;
        }
        else
        {
            canUseSouvenir = true;
        }
    }

    public void ActivateSouvenir()
    {
        if (souvenirsManager.GetComponent<SouvenirsManager>().souvenirsAllowed)
        {
            GetComponent<FightingSoundEffects>().Souvenir();
            canUseSouvenir = false;
            if (souvenirFill != null)
                souvenirFill.fillAmount = 0;
            if (souvenir == souvenirs.medicine) //Health Boost
            {
                HealthBoost();
            }
            if (souvenir == souvenirs.sunscreen) //Flinch Armour
            {
                Armour();
            }
            if (souvenir == souvenirs.coffee) //Movement Speed Boost
            {
                StartCoroutine(Coffee());
            }
            if (souvenir == souvenirs.briefcase && StateNameController.throwableSelection == 0) //Throwable
            {
                Briefcase();
            }
            if (souvenir == souvenirs.ratPoison) //Poison
            {
                hasPoison = true;
                particleManagerInstance.PoisonParticles(gameObject);
                StartCoroutine(CooldownTime(souvenirsManagerInstance.ratPoisonCooldownTime));
            }
            if (souvenir == souvenirs.lifeJacket) //Unkillable
            {
                StartCoroutine(LifeJacket());
            }
            if (souvenir == souvenirs.tequila) //Damage Boost for ALL damage (Throwables)
            {
                StartCoroutine(Tequila());
            }
            if (souvenir == souvenirs.vipCard) //Life Steal
            {
                StartCoroutine(VIPCard());
            }
            if (souvenir == souvenirs.floaty) //Damage Reduction
            {
                StartCoroutine(Floaty());
            }

            resultStatsInstance.SouvenirUsage(gameObject);
        }
    }

    void HealthBoost()
    {
        GetComponent<Health>().AddHealth(souvenirsManagerInstance.healthBoostAmount);
        StartCoroutine(CooldownTime(souvenirsManagerInstance.medicineCooldownTime));
        particleManagerInstance.HealthBoostParticles(gameObject);
    }

    void Armour()
    {
        GetComponent<Health>().armour = souvenirsManagerInstance.armourAmount;
        GetComponent<Health>().GetComponent<Souvenirs>().hasArmour = true;
        GetComponent<Combat>().invulnerable = true;
        StartCoroutine(CooldownTime(souvenirsManager.GetComponent<SouvenirsManager>().sunscreenCooldownTime));
        particleManagerInstance.ArmorParticles(gameObject);
    }

    IEnumerator Coffee()
    {
        hasSpeedBoost = true;
        StartCoroutine(CooldownTime(souvenirsManagerInstance.coffeeCooldownTime));
        particleManagerInstance.SpeedIncreaseParticles(gameObject);
        yield return new WaitForSeconds(souvenirsManagerInstance.speedBoostDuration);
        hasSpeedBoost = false;
    }

    void Briefcase()
    {
        GameObject briefcase = Instantiate(souvenirsManagerInstance.briefcasePrefab, transform.position, transform.rotation);
        briefcase.name = "Briefcase";
        GetComponent<Throw>().closestThrowable = briefcase;
        GetComponent<Throw>().PickUp();
        StartCoroutine(CooldownTime(souvenirsManagerInstance.briefcaseCooldownTime));
    }

    public IEnumerator RatPoison()
    {
        hasPoison = false;
        GetComponent<Combat>().enemy.GetComponent<Flinch>().isPoisoned = true;
        for (int i = 0; i < souvenirsManagerInstance.rpDuration; i++)
        {
            GetComponent<Combat>().enemy.GetComponent<Health>().SubtractHealth(souvenirsManagerInstance.rpDamageAmount);

            yield return new WaitForSeconds(1);
        }
        GetComponent<Combat>().enemy.GetComponent<Flinch>().isPoisoned = false;
    }

    IEnumerator LifeJacket()
    {
        GetComponent<Combat>().unkillable = true;
        GetComponent<Health>().SubtractHealth(0); //Used to update health bar UI
        StartCoroutine(CooldownTime(souvenirsManagerInstance.lifeJacketCooldownTime));
        particleManagerInstance.UnkillableParticles(gameObject);
        yield return new WaitForSeconds(souvenirsManagerInstance.ljDuration);
        GetComponent<Combat>().unkillable = false;
    }

    IEnumerator Tequila()
    {
        hasDamageBoost = true;
        StartCoroutine(CooldownTime(souvenirsManagerInstance.tequilaCooldownTime));
        particleManagerInstance.DamageBoostParticles(gameObject);
        yield return new WaitForSeconds(souvenirsManagerInstance.tDuration);
        hasDamageBoost = false;
    }

    IEnumerator VIPCard()
    {
        hasLifeSteal = true;
        StartCoroutine(CooldownTime(souvenirsManagerInstance.vipcCooldownTime));
        particleManagerInstance.LifeStealParticles(gameObject);
        yield return new WaitForSeconds(souvenirsManagerInstance.vipcDuration);
        hasLifeSteal = false;
    }

    IEnumerator Floaty()
    {
        hasDamageReduction = true;
        StartCoroutine(CooldownTime(souvenirsManagerInstance.fCooldownTime));
        particleManagerInstance.DamageReductionParticles(gameObject);
        yield return new WaitForSeconds(souvenirsManagerInstance.fDuration);
        hasDamageReduction = false;
    }

    public IEnumerator CooldownTime(float time)
    {
        float amount = 1 / time;
        for (int i = 0; i < time; i++) {
            onCooldown = true;
            yield return new WaitForSeconds(1);
            if (souvenirFill != null)
                souvenirFill.fillAmount += amount;
        }
        onCooldown = false;
        canUseSouvenir = true;
    }
}
