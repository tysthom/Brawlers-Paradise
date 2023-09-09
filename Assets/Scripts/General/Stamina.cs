using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    GameObject gameManager;
    GameObject hudManager;
    HUDManager hudManagerInstance;
    BarColors barColorInstance;
    IdManagear idManagerInstance;
    GameObject combatManager;
    GameObject fightStyleManager;

    Image staminaBar;

    public float stamina, maxStamina = 900;
    
    Coroutine regen;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
        barColorInstance = hudManager.GetComponent<BarColors>();
        combatManager = GameObject.Find("Combat Manager");
        fightStyleManager = GameObject.Find("Fight Style Manager");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (idManagerInstance.brawler1 == gameObject)
        {
            staminaBar = hudManagerInstance.brawler1StaminaFill; //Assigns stamina bar from hudManager to staminaBar in current script
        }
        else if (idManagerInstance.brawler2 == gameObject)
        {
            staminaBar = hudManagerInstance.brawler2StaminaFill; //Assigns stamina bar from hudManager to staminaBar in current script
        }
        else
        {
            GetComponent<Stamina>().enabled = false;
        }

        StartCoroutine(Assign());
    }

    IEnumerator Assign()
    {
        yield return new WaitForSeconds(.25f);
        maxStamina = (int)(maxStamina * combatManager.GetComponent<BrawlerStats>().Stamina(gameObject));
        stamina = maxStamina;

    }

    // Update is called once per frame
    void Update()
    {
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina < 0)
        {
            stamina = 0;
        }

        if(stamina < combatManager.GetComponent<CombatStats>().staminaMinAmount)
        {
            staminaBar.GetComponent<Image>().color = barColorInstance.lowStaminaColor;
        }
        else
        {
            staminaBar.GetComponent<Image>().color = barColorInstance.staminaColor;
        }

        if (hudManagerInstance.hudType != HUDManager.hud.none)
        {
            BarFiller();
        }
    }

    void BarFiller()
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    public void SubtractStamina(float staminaAmount)
    {
        if (stamina > 0)
        {
            stamina -= staminaAmount;
            gameManager.GetComponent<ResultStats>().StaminaUsed(gameObject, staminaAmount);
        }

        if (regen != null)
            StopCoroutine(regen);

        regen = StartCoroutine(RegenStamina());
    }

    IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(.5f);

        while (stamina < maxStamina)
        {
            if(!GetComponent<Combat>().isBlocking)
                stamina += maxStamina / 100;
            yield return new WaitForSeconds(.2f);
        }
        regen = null;
    }
}
