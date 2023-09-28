using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    GameObject gameManager;
    IdManagear idManagerInstance;
    public Transform cam;

    private void Start()
    {
        if (gameObject.name != "Finisher Prompt")
        {
            gameManager = GameObject.Find("Game Manager");
            idManagerInstance = gameManager.GetComponent<IdManagear>();

            if (idManagerInstance.gameMode == IdManagear.mode.playerVsAi || idManagerInstance.gameMode == IdManagear.mode.training)
            {
                cam = GameObject.Find("Player Camera").transform;
            }
            else if (idManagerInstance.gameMode == IdManagear.mode.AiVsAi)
            {
                cam = idManagerInstance.spectatorCamera.transform;
            }
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
