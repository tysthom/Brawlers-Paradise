using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOptionsMenu : MonoBehaviour
{
    GameObject menuManager;
    MenuManager menuManagerInstance;
    public GameObject brawler1, brawler2;

    [Header("Souvenir")]
    public int b1SouvenirSelection;
    public int b2SouvenirSelection;
    public string[] souvniers = { "Medicine", "Sunscreen", "Coffee", "Briefcase", "Rat Poison", "Life Jacket", "Tequila",
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

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("Menu Manager");
        menuManagerInstance = menuManager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(b1SouvenirSelection != menuManagerInstance.b1SouvenirSelection)
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
                }
                else if (menuManagerInstance.b1SouvenirSelection == 1)
                {
                    b1CurrentParticle = Instantiate(sunscreenParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76,11,80)));
                }
                else if (menuManagerInstance.b1SouvenirSelection == 2)
                {
                    b1CurrentParticle = Instantiate(coffeeParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b1SouvenirSelection == 4)
                {
                    b1CurrentParticle = Instantiate(ratPoisonParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b1SouvenirSelection == 5)
                {
                    b1CurrentParticle = Instantiate(lifeJacketParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b1SouvenirSelection == 6)
                {
                    b1CurrentParticle = Instantiate(tequilaParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b1SouvenirSelection == 7)
                {
                    b1CurrentParticle = Instantiate(vipCardParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b1SouvenirSelection == 8)
                {
                    b1CurrentParticle = Instantiate(floatyParticles, brawler1.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
            }
            else if(menuManagerInstance.b1SouvenirSelection == 3)
            {
                b1CurrentSuitCase = Instantiate(suitCaseThrowable, new Vector3(14.944f, -0.815f, -2.107f), Quaternion.Euler(new Vector3(-90, 0, 55.2f)));
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
                }
                else if (menuManagerInstance.b2SouvenirSelection == 1)
                {
                    b2CurrentParticle = Instantiate(sunscreenParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b2SouvenirSelection == 2)
                {
                    b2CurrentParticle = Instantiate(coffeeParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b2SouvenirSelection == 4)
                {
                    b2CurrentParticle = Instantiate(ratPoisonParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b2SouvenirSelection == 5)
                {
                    b2CurrentParticle = Instantiate(lifeJacketParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b2SouvenirSelection == 6)
                {
                    b2CurrentParticle = Instantiate(tequilaParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b2SouvenirSelection == 7)
                {
                    b2CurrentParticle = Instantiate(vipCardParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
                else if (menuManagerInstance.b2SouvenirSelection == 8)
                {
                    b2CurrentParticle = Instantiate(floatyParticles, brawler2.transform.position, Quaternion.Euler(new Vector3(-76, 11, 80)));
                }
            }
            else if (menuManagerInstance.b2SouvenirSelection == 3)
            {
                b2CurrentSuitCase = Instantiate(suitCaseThrowable, new Vector3(19.07f, -0.815f, -2.107f), Quaternion.Euler(new Vector3(-90, 0, -55.2f)));
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
