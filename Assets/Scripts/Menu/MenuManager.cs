using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu References")]
    public GameObject mainLogo;
    public GameObject startText;

    private void Awake()
    {
        mainLogo.GetComponent<CanvasGroup>().alpha = 0;
        startText.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainLogoTime(2));

    }
    

    IEnumerator MainLogoTime(float t)
    {
        yield return new WaitForSeconds(t);
        mainLogo.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(t);
        startText.GetComponent<Fade>().fadeIn = true;

    }
}
