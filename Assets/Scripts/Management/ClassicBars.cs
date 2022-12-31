using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBars : MonoBehaviour
{
    GameObject hudManager;
    HUDManager hudManagerInstance;

    private void Awake()
    {
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
    }

    void Start()
    {
        if(hudManagerInstance.hudType != HUDManager.hud.classic)
        {
            gameObject.SetActive(false);
        }
    }
}
