using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearhugBreakOutUI : MonoBehaviour
{
    public GameObject bearhugBreakOutBar;
    public Image bearHugBreakOutFill;
    public GameObject bearHugBreakOutPrompt;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        bearhugBreakOutBar.SetActive(false);
        bearHugBreakOutPrompt.SetActive(false);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GetComponent<Flinch>().isBearhugged)
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
