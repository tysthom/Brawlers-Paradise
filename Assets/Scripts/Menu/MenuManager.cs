using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool canMoveToNextMenu;
    public string currentMenu = "";
    public GameObject blackOut;

    [Header("Cameras")]
    public GameObject titleCamera;
    public GameObject mainMenuCamera;

    [Header("Title Menu References")]
    public GameObject mainLogo;
    public GameObject startText;

    private void Awake()
    {
        mainLogo.GetComponent<CanvasGroup>().alpha = 0;
        startText.GetComponent<CanvasGroup>().alpha = 0;
        currentMenu = "Title Menu";
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainLogoTime(2));

    }

    private void Update()
    {
        bool nextMenu = Input.GetButton("Pick Up");
        if (canMoveToNextMenu && nextMenu)
        {
            if (currentMenu == "Title Menu")
            {
                mainLogo.GetComponent<Fade>().fadeOut = true;
                startText.GetComponent<Fade>().fadeOut = true;
                StartCoroutine(BlackOut(1, 3));
                StartCoroutine(SwitchCameras(2, titleCamera, mainMenuCamera));
            }

            canMoveToNextMenu = false;
        }
    }


    IEnumerator MainLogoTime(float t)
    {
        yield return new WaitForSeconds(t);
        mainLogo.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(t);
        startText.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(1.5f);
        canMoveToNextMenu = true;
    }

    IEnumerator BlackOut(float waitTime, float blackOutTime)
    {
        yield return new WaitForSeconds(waitTime);
        blackOut.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(blackOutTime);
        blackOut.GetComponent<Fade>().fadeOut = true;
    }

    IEnumerator SwitchCameras(float waitTime, GameObject currentCamera, GameObject newCamera)
    {
        yield return new WaitForSeconds(waitTime);
        newCamera.SetActive(true);
        currentCamera.SetActive(false);
    }

}
