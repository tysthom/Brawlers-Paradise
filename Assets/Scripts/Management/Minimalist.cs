using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimalist : MonoBehaviour
{
    GameObject gameManager;
    GameObject hudManager;
    HUDManager hudManagerInstance;

    Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();

        if(hudManagerInstance.hudType != HUDManager.hud.minimalist)
        {
            gameObject.SetActive(false);
        }

        scale = GetComponent<RectTransform>().localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!UniversalFight.fight)
        {
            GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        }
        else
        {
            GetComponent<RectTransform>().localScale = scale;
        }
    }
}
