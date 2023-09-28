using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FightOptionsMenu : MonoBehaviour
{
    GameObject menuManager;
    MenuManager menuManagerInstance;
    public GameObject eventSystem;
    public GameObject brawler1, brawler2;

    [Header("Souvenir")]
    public GameObject b1SouvenirDescription;
    public GameObject b2SouvenirDescription;
    public TextMeshProUGUI b1SouvenirDescriptionText;
    public TextMeshProUGUI b2SouvenirDescriptionText;
    public int b1SouvenirSelection;
    public int b2SouvenirSelection;
    public string[] souvniers = { "Medicine", "Sunscreen", "Coffee", "Briefcase", "Life Jacket", "Rat Poison", "Tequila",
        "VIP Card", "Floaty", "None"};
    public GameObject b1CurrentParticle;
    public GameObject b2CurrentParticle;
    public GameObject b1CurrentSuitCase;
    public GameObject b2CurrentSuitCase;

    Coroutine stopB1Particles;
    Coroutine stopB2Particles;

    [Header("Particles")]
    public GameObject medicineParticles;
    public GameObject sunscreenParticles;
    public GameObject suitCaseThrowable;
    public GameObject coffeeParticles;
    public GameObject ratPoisonParticles;
    public GameObject lifeJacketParticles;
    public GameObject tequilaParticles;
    public GameObject vipCardParticles;
    public GameObject floatyParticles;


    [Header("Throwable")]
    public bool throwableAllowed;

    void Start()
    {
        menuManager = GameObject.Find("Menu Manager");
        menuManagerInstance = menuManager.GetComponent<MenuManager>();
    }

    void Update()
    {
        if(eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name == "B1 SouvenirSelection" ||
            eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name == "B2 SouvenirSelection")
        {
            b1SouvenirDescription.SetActive(true);
            b2SouvenirDescription.SetActive(true);
        }
        else
        {
            b1SouvenirDescription.SetActive(false);
            b2SouvenirDescription.SetActive(false);
        }

        if (b1SouvenirSelection != menuManagerInstance.b1SouvenirSelection)
        {
            if(b1CurrentParticle != null)
            {
                Destroy(b1CurrentParticle);
            } else if(b1CurrentSuitCase != null)
            {
                Destroy(b1CurrentSuitCase);
            }

            if(menuManagerInstance.b1SouvenirSelection != 3 && menuManagerInstance.b1SouvenirSelection != 9)
            {
                if(menuManagerInstance.b1SouvenirSelection == 0)
                {
                    b1CurrentParticle = Instantiate(medicineParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Restore 250 health at once. (35 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 1)
                {
                    b1CurrentParticle = Instantiate(sunscreenParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76,11,80)));
                    b1SouvenirDescriptionText.text = "Ignore flinches & knockdowns from basic attacks while armor lasts. (30 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 2)
                {
                    b1CurrentParticle = Instantiate(coffeeParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Move at 1.2x speed for 10 sec. (25 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 4)
                {
                    b1CurrentParticle = Instantiate(lifeJacketParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Become unkillable for 5 sec. (40 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 5)
                {
                    b1CurrentParticle = Instantiate(ratPoisonParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Deal 75 damage over 5 sec. (30 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 6)
                {
                    b1CurrentParticle = Instantiate(tequilaParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Deal 1.4x damage with basic attacks for 10 sec. (30 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 7)
                {
                    b1CurrentParticle = Instantiate(vipCardParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Landing basic attacks grants health back for 10 sec. (35 sec cooldown)";
                }
                else if (menuManagerInstance.b1SouvenirSelection == 8)
                {
                    b1CurrentParticle = Instantiate(floatyParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b1SouvenirDescriptionText.text = "Damage recieved deal .5x normal amount for 10 sec. (25 sec cooldown)";
                }
            }
            else if(menuManagerInstance.b1SouvenirSelection == 3)
            {
                b1CurrentSuitCase = Instantiate(suitCaseThrowable, new Vector3(14.944f, -0.902f, -2.107f), Quaternion.Euler(new Vector3(-90, 0, 55.2f)));
                b1SouvenirDescriptionText.text = "Summon a thrwoable that deals 2x damage. (15 sec cooldown)";
            }
            else if(menuManagerInstance.b1SouvenirSelection == 9)
            {
                b1SouvenirDescriptionText.text = "No Souvenir Selected.";
            }

            if (stopB1Particles != null)
                StopCoroutine(stopB1Particles);

            stopB1Particles = StartCoroutine(StopB1Particles());
        }

        if (b2SouvenirSelection != menuManagerInstance.b2SouvenirSelection)
        {
            if (b2CurrentParticle != null)
            {
                Destroy(b2CurrentParticle);
            }
            else if (b2CurrentSuitCase != null)
            {
                Destroy(b2CurrentSuitCase);
            }

            if (menuManagerInstance.b2SouvenirSelection != 3 && menuManagerInstance.b2SouvenirSelection != 9)
            {
                if (menuManagerInstance.b2SouvenirSelection == 0)
                {
                    b2CurrentParticle = Instantiate(medicineParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Restore 250 health at once. (35 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 1)
                {
                    b2CurrentParticle = Instantiate(sunscreenParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Ignore flinches & knockdowns from basic attacks while armor lasts. (30 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 2)
                {
                    b2CurrentParticle = Instantiate(coffeeParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Move at 1.2x speed for 10 sec. (25 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 4)
                {
                    b2CurrentParticle = Instantiate(lifeJacketParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Become unkillable for 5 sec. (40 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 5)
                {
                    b2CurrentParticle = Instantiate(ratPoisonParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Deal 75 damage over 5 sec. (30 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 6)
                {
                    b2CurrentParticle = Instantiate(tequilaParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Deal 1.4x damage with basic attacks for 10 sec. (30 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 7)
                {
                    b2CurrentParticle = Instantiate(vipCardParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Landing basic attacks grants health back for 10 sec. (35 sec cooldown)";
                }
                else if (menuManagerInstance.b2SouvenirSelection == 8)
                {
                    b2CurrentParticle = Instantiate(floatyParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                    b2SouvenirDescriptionText.text = "Damage recieved deal .5x normal amount for 10 sec. (25 sec cooldown)";
                }
            }
            else if (menuManagerInstance.b2SouvenirSelection == 3)
            {
                b2CurrentSuitCase = Instantiate(suitCaseThrowable, new Vector3(19.07f, -0.912f, -2.107f), Quaternion.Euler(new Vector3(-90, 0, -55.2f)));
                b2SouvenirDescriptionText.text = "Summon a thrwoable that deals 2x damage. (15 sec cooldown)";
            } 
            else if (menuManagerInstance.b2SouvenirSelection == 9)
            {
                b2SouvenirDescriptionText.text = "No Souvenir Selected.";
            }

            if (stopB2Particles != null)
                StopCoroutine(stopB2Particles);

            stopB2Particles = StartCoroutine(StopB2Particles());
        }

        b1SouvenirSelection = menuManagerInstance.b1SouvenirSelection;
        b2SouvenirSelection = menuManagerInstance.b2SouvenirSelection;

        throwableAllowed = menuManagerInstance.throwableAllowed;
    }

    IEnumerator StopB1Particles()
    {
        yield return new WaitForSeconds(2);

       if (b1CurrentParticle != null)
       {
           b1CurrentParticle.GetComponent<ParticleSystem>().Stop(true);
       }

        yield return new WaitForSeconds(1);

        Destroy(b1CurrentParticle);
    }

    IEnumerator StopB2Particles()
    {
        yield return new WaitForSeconds(2);

        if (b2CurrentParticle != null)
        {
            b2CurrentParticle.GetComponent<ParticleSystem>().Stop(true);
        }

        yield return new WaitForSeconds(1);

        Destroy(b2CurrentParticle);
    }
}
