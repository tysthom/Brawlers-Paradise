using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    GameObject gameManager;
    GameObject hudManager;
    HUDManager hudManagerInstance;
    BarColors barColorInstance;
    GameObject souvenirsManager;
    IdManagear idManagerInstance;
    GameObject combatManager;
    GameObject fightStyleManager;

    public Image healthBar;
    public Image healthRegenBar;
    public Image shieldBar;

    public float health, regenHealth, maxHealth = 100, shield;
    public float previousHealth;
    Coroutine regen;

    bool canDeteriorate = true; //Derterioration of the shield

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        souvenirsManager = GameObject.Find("Souvenir Manager");
        combatManager = GameObject.Find("Combat Manager");
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
        barColorInstance = hudManager.GetComponent<BarColors>();
        fightStyleManager = GameObject.Find("Fight Style Manager");
    }

    void Start()
    {
        StartCoroutine(CallFunctions());
    }

    IEnumerator CallFunctions()
    {
        yield return new WaitForSeconds(1);

        if (idManagerInstance.brawler1 == gameObject)
        {
            healthBar = hudManagerInstance.brawler1HealthFill;
            healthRegenBar = hudManagerInstance.brawler1HealthRegen;
            shieldBar = hudManagerInstance.brawler1ShieldFill;
        }
        else if (idManagerInstance.brawler2 == gameObject)
        {
            healthBar = hudManagerInstance.brawler2HealthFill;
            healthRegenBar = hudManagerInstance.brawler2HealthRegen;
            shieldBar = hudManagerInstance.brawler2ShieldFill;
        }
        maxHealth = (int)(maxHealth * combatManager.GetComponent<BrawlerStats>().Health(gameObject));
        health = maxHealth;
        previousHealth = maxHealth;
        shield = 0;

        StartCoroutine(KararteRegenHealth());

        if (hudManagerInstance.hudType == HUDManager.hud.none || tag == "Tourist")
        {
            //return;
        }
        else
        {
            healthBar.GetComponent<Image>().color = barColorInstance.healthColor;
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (health > maxHealth)
        {
            health = maxHealth;
        } 
        if (health <= 0)
        {
            health = 0;
            regenHealth = 0;
        }

        if (hudManagerInstance.hudType != HUDManager.hud.none && tag != "Tourist")
        {
            BarFiller();

            if (GetComponent<Souvenirs>().hasShield && canDeteriorate)
            {
                StartCoroutine(DeteriorateShield());
            }

            if (GetComponent<Flinch>().isPoisoned)
            {
                healthBar.GetComponent<Image>().color = barColorInstance.poisonColor;
            }
            else if (GetComponent<Combat>().unkillable)
            {
                healthBar.GetComponent<Image>().color = barColorInstance.unkillableColor;
            }
            else
            {
                healthBar.GetComponent<Image>().color = barColorInstance.healthColor;
            }
        }
    }

    void BarFiller()
    {
        healthBar.fillAmount = health / maxHealth;
        healthRegenBar.fillAmount = regenHealth / maxHealth;
        if(GetComponent<Souvenirs>().hasShield)
        shieldBar.fillAmount = shield / souvenirsManager.GetComponent<SouvenirsManager>().shieldAmount;
    }

    IEnumerator DeteriorateShield()
    {
        while (shield > 0)
        {

            canDeteriorate = false;
            yield return new WaitForSeconds(.5f);
            shield -= souvenirsManager.GetComponent<SouvenirsManager>().shieldAmount/10;
            canDeteriorate = true;
        }
        regen = null;
        GetComponent<Souvenirs>().hasShield = false;
        GetComponent<Combat>().invulnerable = false;
        shieldBar.fillAmount = 0;
        StartCoroutine(GetComponent<Souvenirs>().CooldownTime(souvenirsManager.GetComponent<SouvenirsManager>().sunscreenCooldownTime));
    }

    public void SubtractHealth(float damageAmount)
    {
        if (shield <= 0)
        {
            previousHealth = health;
            if (health > 0)
            {
                health -= damageAmount;
            }
            regenHealth = health + ((maxHealth - health) * .2f);

            if (regenHealth > previousHealth)
            {
                regenHealth = previousHealth;
            }

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenHealth());
            if (GetComponent<Combat>().unkillable && health < maxHealth * souvenirsManager.GetComponent<SouvenirsManager>().ljMinHealthAmount)
            {

                health = maxHealth * souvenirsManager.GetComponent<SouvenirsManager>().ljMinHealthAmount;
            }
        }
        else
        {
            SubtractShield(damageAmount);
        }
    }

    public void SubtractShield(float damageAmount)
    {
        shield -= damageAmount;
        if(shield < 0)
        {
            shield = 0;
            GetComponent<Souvenirs>().hasShield = false;
        }
    }

    public IEnumerator DamageOverTime(float damageAmount)
    {
        if (regen != null)
            StopCoroutine(regen);

        for (int i = 0; i < 5; i++)
        {
            if (shield <= 0)
            {
                if (health > 0)
                {
                    health -= damageAmount;
                }
            }
            else
            {
                shield -= damageAmount;
            }
            yield return new WaitForSeconds(1);
        }

        regen = StartCoroutine(RegenHealth());
    }

    public void AddHealth(float amount)
    {
        regenHealth += amount;
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health > regenHealth)
        {
            regenHealth = health;
        }
    }

    IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(4);

        while (health < regenHealth)
        {
            health += maxHealth / 100;
            yield return new WaitForSeconds(.5f);
        }
        regen = null;
    }

    IEnumerator KararteRegenHealth()
    {
        yield return new WaitForSeconds(1);

        while (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate)
        {
            if (GetComponent<Combat>().anim.GetBool("isPassiveStance"))
            {
                Debug.Log("Regenerating");
                regenHealth += fightStyleManager.GetComponent<KarateStats>().karateHealthRecoveryAmount;
                health += fightStyleManager.GetComponent<KarateStats>().karateHealthRecoveryAmount;
                yield return new WaitForSeconds(fightStyleManager.GetComponent<KarateStats>().karateRegenHealthWaitTime);
            }

            yield return new WaitForSeconds(.1f);
        }
        regen = null;
    }
}
