using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorHUD : MonoBehaviour
{
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.GetComponent<IdManagear>().gameMode == IdManagear.mode.AiVsAi)
        {
            gameObject.SetActive(false);
        }
    }
}
