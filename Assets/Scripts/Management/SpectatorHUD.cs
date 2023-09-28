using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorHUD : MonoBehaviour
{
    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
    }

    void Update()
    {
        if(gameManager.GetComponent<IdManagear>().gameMode == IdManagear.mode.AiVsAi)
        {
            gameObject.SetActive(false);
        }
    }
}
