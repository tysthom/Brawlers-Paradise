using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    GameObject gameManager;
    IdManagear idManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        if (UniversalFight.fight)
        {
            gameManager = GameObject.Find("Game Manager");
            idManagerInstance = gameManager.GetComponent<IdManagear>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UniversalFight.fight)
        {
            if (idManagerInstance.brawler1.GetComponent<Death>().dead || idManagerInstance.brawler2.GetComponent<Death>().dead)
            {
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    public IEnumerator ParticlesDeletion(GameObject brawler)
    {

        if (name == "Attack Increase Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Animator>().GetBool("isOffensiveStance") == false);
            GetComponent<ParticleSystem>().Stop();
        } 
        else if (name == "Defense Increase Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Animator>().GetBool("isDefensiveStance") == false);
            GetComponent<ParticleSystem>().Stop();
        } 
        else if (name == "Health Regen Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Animator>().GetBool("isPassiveStance") == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Health Boost Burst Particles(Clone)")
        {
            yield return new WaitForSeconds(3);
        }
        else if (name == "Armor Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasArmour == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Speed Increase Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasSpeedBoost == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Poison Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasPoison == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Unkillable Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Combat>().unkillable == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Damage Boost Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasDamageBoost == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Life Steal Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasLifeSteal == false);
            GetComponent<ParticleSystem>().Stop();
        }
        else if (name == "Damage Reduction Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasDamageReduction == false);
            GetComponent<ParticleSystem>().Stop();
        }

        yield return new WaitForSeconds(2);

        if(gameObject != null)
            Destroy(gameObject);
    }
}
