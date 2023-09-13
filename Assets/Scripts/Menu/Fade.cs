using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;

    public bool fadeIn, fadeOut;

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeIn = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if(myUIGroup != null && myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime;
                if(myUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (myUIGroup != null && myUIGroup.alpha >= 0)
            {
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            } else if(myUIGroup == null)
            {
                GetComponent<TextMeshProUGUI>().alpha -= Time.deltaTime * 2;
                if (GetComponent<TextMeshProUGUI>().alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
