using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveAttackUI : MonoBehaviour
{
    GameObject hudManager;
    HUDManager hudManagerInstance;
    public GameObject playerDiveAttackPrompt;
    GameObject player;

    private void Start()
    {
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player != null && player.GetComponent<Combat>().isGroundIdle || player.GetComponent<Combat>().isGroundAttacking 
                && hudManagerInstance.hudType != HUDManager.hud.none)
        {
            playerDiveAttackPrompt.SetActive(true);
        }
        else
        {
            playerDiveAttackPrompt.SetActive(false);
        }
    }
}
