using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimalist : MonoBehaviour
{
    GameObject hudManager;
    HUDManager hudManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();

        if(hudManagerInstance.hudType != HUDManager.hud.minimalist)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
