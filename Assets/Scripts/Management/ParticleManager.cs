using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ParticleManager : MonoBehaviour
{
    [Header("Refrences")]
    GameObject gameManager;

    public GameObject brawler1;
    public GameObject brawler2;

    public ParticleSystem karateOffensiveParticles;
    public ParticleSystem karateDefensiveParticles;
    public ParticleSystem karatePassiceParticles;
    public ParticleSystem healthBoostParticles;
    public ParticleSystem armorParticles;
    public ParticleSystem speedIncreaseParticles;
    public ParticleSystem poisonParticles;
    public ParticleSystem unkillableParticles;
    public ParticleSystem damageBoostParticles;
    public ParticleSystem lifeStealParticles;
    public ParticleSystem damageReductionParticles;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
    }

    public void KarateOffensiveParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(karateOffensiveParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void KarateDefensiveParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(karateDefensiveParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void KaratePassiveParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(karatePassiceParticles, brawler.transform.position, Quaternion.Euler(-90,0,0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void HealthBoostParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(healthBoostParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void ArmorParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(armorParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void SpeedIncreaseParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(speedIncreaseParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void PoisonParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(poisonParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void UnkillableParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(unkillableParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void DamageBoostParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(damageBoostParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void LifeStealParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(lifeStealParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }

    public void DamageReductionParticles(GameObject brawler)
    {
        ParticleSystem p = Instantiate(damageReductionParticles, brawler.transform.position, Quaternion.Euler(-90, 0, 0));
        p.transform.SetParent(brawler.transform);
        StartCoroutine(p.GetComponent<Particles>().ParticlesDeletion(brawler));
    }
}
