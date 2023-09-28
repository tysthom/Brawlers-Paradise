using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearhugBreakOutUI : MonoBehaviour
{
    GameObject gameManager;
    GameObject hudManager;
    HUDManager hudManagerInstance;
    public GameObject bearhugBreakOutBar;
    public Image bearHugBreakOutFill;
    public GameObject bearHugBreakOutPrompt;
    GameObject player;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
        bearhugBreakOutBar.SetActive(false);
        bearHugBreakOutPrompt.SetActive(false);
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (gameManager.GetComponent<IdManagear>().gameMode != IdManagear.mode.AiVsAi && player != null && player.GetComponent<Flinch>().isBearhugged && hudManagerInstance.hudType != HUDManager.hud.none)
        {
            bearhugBreakOutBar.SetActive(true);
            bearHugBreakOutPrompt.SetActive(true);
            bearHugBreakOutFill.fillAmount = (float)player.GetComponent<Combat>().bearhugBreakOutCount / 100;
        }
        else
        {
            bearHugBreakOutFill.fillAmount = 0;
            bearhugBreakOutBar.SetActive(false);
            bearHugBreakOutPrompt.SetActive(false);
        }
    }
}
