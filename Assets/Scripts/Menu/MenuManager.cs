using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public bool canMoveToNextMenu;
    public string currentMenu = "";
    public GameObject eventSystem;
    public GameObject blackOut;


    [Header("Cameras")]
    public GameObject titleCamera;
    public GameObject mainMenuCamera;

    [Header("Title Menu References")]
    public GameObject mainLogo;
    public GameObject startText;

    [Header("Main Menu References")]
    public GameObject mainMenu;
    public GameObject fightButton;

    [Header("Fight Menu")]
    public GameObject fightMenu;
    public bool b1Side;
    public bool b2Side;
    public GameObject modeDropDown;
    public string[] fightStyles = { "" };
    bool canSwtichFightStyle;
    public TextMeshProUGUI b1FightStyleText;
    public TextMeshProUGUI b2FightStyleText;
    int b1FightStyle = 0;
    int b2FightStyle = 0;

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
                currentMenu = "Main Menu";
            }

            canMoveToNextMenu = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        if(currentMenu == "Fight Menu")
        {
            if (canSwtichFightStyle)
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(FightStyle("right"));
                } 
                else if (horizontal < -.75f)
                {
                    StartCoroutine(FightStyle("left"));
                }
            } 
        }

        b1FightStyleText.text = fightStyles[b1FightStyle];
        b2FightStyleText.text = fightStyles[b2FightStyle];
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
        MainMenu();
        //canMoveToNextMenu = true;
    }

    IEnumerator SwitchCameras(float waitTime, GameObject currentCamera, GameObject newCamera)
    {
        yield return new WaitForSeconds(waitTime);
        newCamera.SetActive(true);
        currentCamera.SetActive(false);
        canSwtichFightStyle = true;
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = fightButton;
    }

    //FIGHT MENU

    public void FightMenu() //Used when Fight button is pressed
    {
        StartCoroutine(FightMenuCoroutine());
    }

    public IEnumerator FightMenuCoroutine() //
    {
        currentMenu = "Fight Menu";
        mainMenuCamera.GetComponent<SmoothDampCamera>().smoothDamp = true;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        fightMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(modeDropDown);
        b1Side = true;
        b2Side = false;
    }

    IEnumerator FightStyle(string dir)
    {
        canSwtichFightStyle = false;

        if (b1Side)
        {
            if(dir == "right")
            {
                if (b1FightStyle == fightStyles.Length - 1)
                {
                    b1FightStyle = 0;
                }
                else
                {
                    b1FightStyle++;
                }
            }
            else
            {
                if (b1FightStyle == 0)
                {
                    b1FightStyle = fightStyles.Length - 1;
                }
                else
                {
                    b1FightStyle--;
                }
            }
        }
        else
        {

        }

        yield return new WaitForSeconds(.5f);
        canSwtichFightStyle = true;
    }
}
