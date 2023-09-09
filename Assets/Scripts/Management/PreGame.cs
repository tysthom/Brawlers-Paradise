using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreGame : MonoBehaviour
{
    public TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
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
