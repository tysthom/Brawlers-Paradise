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
        
        if (name == "Defense Increase Particles(Clone)")
        {
            Debug.Log("Hey");
            yield return new WaitUntil(() => brawler.GetComponent<Animator>().GetBool("isDefensiveStance") == false);
            GetComponent<ParticleSystem>().Stop();

            //Destroy(gameObject);
        }
    }
}
