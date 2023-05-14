using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearhugBreakOutUI : MonoBehaviour
{
    GameObject hudManager;
    HUDManager hudManagerInstance;
    public GameObject bearhugBreakOutBar;
    public Image bearHugBreakOutFill;
    public GameObject bearHugBreakOutPrompt;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hudManager = GameObject.Find("HUD Manager");
        hudManagerInstance = hudManager.GetComponent<HUDManager>();
        bearhugBreakOutBar.SetActive(false);
        bearHugBreakOutPrompt.SetActive(false);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GetComponent<Flinch>().isBearhugged && hudManagerInstance.hudType != HUDManager.hud.none)
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
