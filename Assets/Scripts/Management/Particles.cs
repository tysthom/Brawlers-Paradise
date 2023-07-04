using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
        else if (name == "Armor Lasting Particles(Clone)")
        {
            yield return new WaitUntil(() => brawler.GetComponent<Souvenirs>().hasArmour == false);
            GetComponent<ParticleSystem>().Stop();
        }

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
