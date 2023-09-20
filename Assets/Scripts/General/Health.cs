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
    public Image armourBar;

    public float health, regenHealth, maxHealth = 100, armour;
    public float previousHealth;
    Coroutine regen;

    bool canDeteriorate = true; //Derterioration of the armour

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
        yield return new WaitForSeconds(.25f);

        if (idManagerInstance.brawler1 == gameObject)
        {
            healthBar = hudManagerInstance.brawler1HealthFill;
            healthRegenBar = hudManagerInstance.brawler1HealthRegen;
            armourBar = hudManagerInstance.brawler1ArmourFill;
        }
        else if (idManagerInstance.brawler2 == gameObject)
        {
            
            healthBar = hudManagerInstance.brawler2HealthFill;
            healthRegenBar = hudManagerInstance.brawler2HealthRegen;
            armourBar = hudManagerInstance.brawler2ArmourFill;
        }

        maxHealth = (int)(maxHealth * combatManager.GetComponent<BrawlerStats>().Health(gameObject));
        health = maxHealth;
        previousHealth = maxHealth;
        armour = 0;

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

        if (UniversalFight.fight)
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

                if (GetComponent<Souvenirs>().hasArmour && canDeteriorate)
                {
                    StartCoroutine(DeteriorateArmour());
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
    }

    void BarFiller()
    {
        healthBar.fillAmount = health / maxHealth;
        healthRegenBar.fillAmount = regenHealth / maxHealth;
        if(GetComponent<Souvenirs>().hasArmour)
        armourBar.fillAmount = armour / souvenirsManager.GetComponent<SouvenirsManager>().armourAmount;
    }

    IEnumerator DeteriorateArmour()
    {
        while (armour > 0)
        {

            canDeteriorate = false;
            yield return new WaitForSeconds(.5f);
            armour -= souvenirsManager.GetComponent<SouvenirsManager>().armourAmount/10;
            canDeteriorate = true;
        }
        regen = null;
        GetComponent<Souvenirs>().hasArmour = false;
        GetComponent<Combat>().invulnerable = false;
        armourBar.fillAmount = 0;
    }

    public void SubtractHealth(float damageAmount)
    {
        if (armour <= 0)
        {
            previousHealth = health;
            if (health > 0)
            {
                health -= damageAmount;
            }
            gameManager.GetComponent<ResultStats>().DamageInflicted(gameObject, damageAmount);

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
            SubtractArmor(damageAmount);
        }
    }

    public void SubtractArmor(float damageAmount)
    {
        armour -= damageAmount;
        if(armour < 0)
        {
            armour = 0;
            GetComponent<Souvenirs>().hasArmour = false;
        }
    }

    public IEnumerator DamageOverTime(float damageAmount, int repeatAmount)
    {
        if (regen != null)
            StopCoroutine(regen);

        for (int i = 0; i < repeatAmount; i++)
        {
            if (armour <= 0)
            {
                if (health > 0)
                {
                    health -= damageAmount;
                    regenHealth = health;
                    gameManager.GetComponent<ResultStats>().DamageInflicted(gameObject, damageAmount);
                    if (health <= 0)
                    {
                        health = 5;
                    }
                }
            }
            else
            {
                armour -= damageAmount;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void AddHealth(float amount)
    {
        regenHealth += amount;
        health += amount;
        gameManager.GetComponent<ResultStats>().HealthRecovered(gameObject, amount);
        if (health > maxHealth)
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
            gameManager.GetComponent<ResultStats>().HealthRecovered(gameObject, (maxHealth / 100));
            yield return new WaitForSeconds(.5f);
        }
        regen = null;
    }

    IEnumerator KararteRegenHealth()
    {
        yield return new WaitForSeconds(1);

        while (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.karate ) 
        {
            if (GetComponent<Combat>().anim.GetBool("isPassiveStance") && !GetComponent<Flinch>().isSurrendering &&
            !GetComponent<Flinch>().isBeingFinished && !GetComponent<Death>().dead)
            {
                regenHealth += fightStyleManager.GetComponent<KarateStats>().karateHealthRecoveryAmount;
                health += fightStyleManager.GetComponent<KarateStats>().karateHealthRecoveryAmount;
                gameManager.GetComponent<ResultStats>().HealthRecovered(gameObject, fightStyleManager.GetComponent<KarateStats>().karateHealthRecoveryAmount);
                yield return new WaitForSeconds(fightStyleManager.GetComponent<KarateStats>().karateRegenHealthWaitTime);
            }

            yield return new WaitForSeconds(.1f);
        }
        regen = null;
    }
}
