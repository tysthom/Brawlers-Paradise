using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ParticleManager : MonoBehaviour
{
    [Header("Refrences")]
    GameObject gameManager;
    IdManagear idManagerInstance;

    public GameObject brawler1;
    public GameObject brawler2;

    public ParticleSystem karateOffensiveParticles;
    public ParticleSystem karateDefensiveParticles;
    public ParticleSystem karatePassiceParticles;
    public ParticleSystem armourParticles;


    // Start is called before the first frame update
    void Start()
    {


        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KarateOffensiveParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(karateOffensiveParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        
    }

    public void KarateDefensiveParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(karateDefensiveParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void KaratePassiveParticles(GameObject brawler)
    {
        Instantiate(karatePassiceParticles, brawler.transform.position, Quaternion.Euler(-90,0,0));
    }

    public void ArmourParticles(GameObject brawler)
    {
        Instantiate(armourParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
    }
}
