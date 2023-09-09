using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreGame : MonoBehaviour
{
    public GameObject blackOut;
    public TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        blackOut.GetComponent<CanvasGroup>().alpha = 1;
        blackOut.GetComponent<Fade>().fadeOut = true;
        countDownText.text = "";
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1.5f);

        for(int i = 3; i > 0; i--)
        {
            countDownText.text = "" + i;
            yield return new WaitForSeconds(1);
        }

        countDownText.text = "FIGHT";
        UniversalFight.fight = true;

        yield return new WaitForSeconds(.5f);
        countDownText.GetComponent<Fade>().fadeOut = true;
    }
}
