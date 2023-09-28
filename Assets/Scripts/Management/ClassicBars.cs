using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBars : MonoBehaviour
{
    GameObject gameManager;
    GameObject hudManager;
    HUDManager hudManagerInstance;

    Vector3 scale;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
    }

    void Start()
    {
        if(hudManagerInstance.hudType != HUDManager.hud.classic)
        {
            gameObject.SetActive(false);
        }
        scale = GetComponent<RectTransform>().localScale;
    }

    private void Update()
    {
        if (!UniversalFight.fight) 
        {
            GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
        } 
        else
        {
            GetComponent<RectTransform>().localScale = scale;
        }
    }
}
