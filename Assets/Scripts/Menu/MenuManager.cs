using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    int firstTime = 0;

    public bool canChangeMenu;
    public string currentMenu = "";
    public GameObject eventSystem;
    public GameObject blackOut;
    bool useMainMenu;
    public AudioSource musicAudioSource;
    public AudioSource soundEffectsAudioSource;
    SoundManager soundManagerInstance;

    [Header("Cameras")]
    public GameObject titleCamera;
    public GameObject menuCamera;

    [Header("Title Menu References")]
    public GameObject mainLogo;
    public GameObject startText;

    [Header("Main Menu References")]
    public GameObject mainMenu;
    public GameObject fightButton;

    [Header("Fight Menu")]
    public GameObject fightMenu;
    GameObject currentSelected;
    public bool b1Side;
    public bool b2Side;
    public GameObject[] b1MenuSide;
    public GameObject[] b2MenuSide;
    //GAME MODE
    public GameObject modeDropDown;
    //FIGHT STYLE
    public GameObject b1FightStyleButton;
    public GameObject b1FightStyleStats;
    public GameObject b2FightStyleStats;
    public string[] fightStyles = { "" };
    public GameObject[] b1StatsBars = new GameObject[5];
    public GameObject[] b2StatsBars = new GameObject[5];
    float[] karateStats = { 3, 3, 4, 2, 2 };
    float[] boxingStats = { 5, 4, 1, 1, 3 };
    float[] mmaStats = { 4, 5, 2, 3, 3 };
    float[] tkdStats = { 2, 4, 5, 3, 2 };
    float[] kungFuStats = { 4, 2, 3, 5, 2 };
    float[] wrestlingStats = { 1, 5, 3, 5, 3 };
    bool canSwtichFightStyle;
    public TextMeshProUGUI b1FightStyleText;
    public TextMeshProUGUI b2FightStyleText;
    public int b1FightStyle = 0;
    public int b2FightStyle = 0;
    //OUTFIT SELECTION
    bool canSwitchOutfitSelection;
    public TextMeshProUGUI b1OutfitSelectionText;
    public TextMeshProUGUI b2OutfitSelectionText;
    public int b1OutfitSelection;
    public int b2OutfitSelection;
    //OUTFIT VARIATION
    bool canSwitchOutfitVariation;
    public TextMeshProUGUI b1OutfitVariationText;
    public TextMeshProUGUI b2OutfitVariationText;
    public int b1OutfitVariation;
    public int b2OutfitVariation;
    //HAIR TYPE
    bool canSwitchHairType;
    public TextMeshProUGUI b1HairTypeText;
    public TextMeshProUGUI b2HairTypeText;
    public int b1HairType;
    public int b2HairType;
    //HAIR COLOR
    bool canSwitchHairColor;
    public Image b1HairColorIndicator;
    public Image b2HairColorIndicator;
    public int b1HairColor;
    public int b2HairColor;
    //SKIN COLOR
    bool canSwitchSkinColor;
    public Image b1SkinColorIndicator;
    public Image b2SkinColorIndicator;
    public int b1SkinColor;
    public int b2SkinColor;
    //NAME
    bool canSwitchName;
    public TextMeshProUGUI b1NameText;
    public TextMeshProUGUI b2NameText;
    public int b1NameSelection;
    public int b2NameSelection;

    [Header("Fight Options Menu")]
    public GameObject fightOptionsMenu;
    //SOUVENIRS
    public GameObject b1SouvenirSouvenirButton;
    bool canSwitchSouvenir;
    public TextMeshProUGUI b1SouvenirText;
    public TextMeshProUGUI b2SouvenirText;
    public int b1SouvenirSelection;
    public int b2SouvenirSelection;
    //GAME MODE
    public TMP_Dropdown gameModeDropDown;
    public int gameModeSelection;
    //DIFFICULTY
    public TMP_Dropdown difficultyDropdown;
    public int difficultySelection;
    //THROWABLE
    public Toggle throwableToggle;
    public bool throwableAllowed;

    [Header("Stats")]
    public GameObject statsMenu;
    public TextMeshProUGUI totalFightsText;
    public TextMeshProUGUI winRateText;
    public TextMeshProUGUI fightStyleText;
    public TextMeshProUGUI basicAttackText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI dodgeText;
    public TextMeshProUGUI souvenirText;
    float resetAmount = 0;
    public GameObject resetFill;
    Coroutine resetCoroutine;

    [Header("Options Menu")]
    public GameObject optionsBackground;
    public GameObject optionsMenu;
    public GameObject howToPlayButton;
    public GameObject[] optionsMenuParts = new GameObject[4];
    //HOW TO PLAY
    public GameObject htpMenu;
    public GameObject controlsButton;
    public GameObject[] htpMenuParts = new GameObject[7];
        //Controls
    public GameObject controlsMenu;
        //Combat
    public GameObject combatMenu;
        //Fight Styles
    public GameObject fightStylesMenu;
    public GameObject karateFightStyleButton;
    public GameObject[] fightStylesMenuParts = new GameObject[4];
    public GameObject[] fightStylesIndividualMenus = new GameObject[6];
        //Souvenirs
    public GameObject souvenirsMenu;
        //HUD Colors
    public GameObject hudColorsMenu;
        //Throwables
    public GameObject throwablesMenu;
        //Finisher
    public GameObject finisherMenu;
    //SETTINGS
    public GameObject settingsMenu;
        //HUD Selection
    public TMP_Dropdown hudSelectionDropdown;
    public int hudSelection;
        //Vibration
    public Toggle vibrationToggle;
    public bool vibrationAllowed;
        //Music Volume
    public Slider musicSlider;
    public float musicSliderValue;
        //Sound Effects Volume
    public Slider soundEffectsSlider;
    public float soundEffectsSliderValue;
    //CREDITS
    public GameObject creditsMenu;

    private void Awake()
    {
        useMainMenu = StateNameController.useMainMenu;
        musicAudioSource = GameObject.Find("Music Audio").GetComponent<AudioSource>();
        soundEffectsAudioSource = GameObject.Find("Sound Effects").GetComponent<AudioSource>();
        soundManagerInstance = GetComponent<SoundManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (useMainMenu)
        {
            mainLogo.GetComponent<CanvasGroup>().alpha = 0;
            startText.GetComponent<CanvasGroup>().alpha = 0;
            currentMenu = "Title Menu";
        }
        else
        {
            blackOut.GetComponent<CanvasGroup>().alpha = 1;
            blackOut.GetComponent<Fade>().fadeOut = true;
            canChangeMenu = true;
            musicAudioSource.Play();
            StartCoroutine(SwitchCameras(0, titleCamera, menuCamera));
            MainMenu();
        }

        musicAudioSource.loop = true;
        musicAudioSource.clip = soundManagerInstance.mainMenuMusic;
        musicAudioSource.Play();

        fightMenu.GetComponent<BrawlerUpdates>().brawler1.transform.position += new Vector3(0, 0, -10);
        fightMenu.GetComponent<BrawlerUpdates>().brawler2.transform.position += new Vector3(0, 0, -10);

        firstTime = PlayerPrefs.GetInt("SavedFirstRun");
        if (firstTime == 0)
        {
            PlayerPrefs.SetInt("SavedFirstRun", 1);
            InitialRandomizer();

            PlayerPrefs.SetFloat("Music Value", 1);
            PlayerPrefs.SetFloat("Sound Effects Value", 1);
        }

        GetComponent<SaveData>().LoadBrawler();
        GetComponent<SaveData>().LoadFightOptions();
        GetComponent<SaveData>().LoadOptions();
    }

    void Start()
    {
        if(useMainMenu)
        StartCoroutine(MainLogoTime(2));
    }

    private void Update()
    {
        currentSelected = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;

        if (!GetComponent<Fade>().fadeOut)
        {
            musicAudioSource.volume = StateNameController.musicVolume;
        }
        soundEffectsAudioSource.volume = StateNameController.soundEffectsVolume;

        bool aButton = Input.GetButton("Pick Up");
        bool switchB1Side = Input.GetButtonDown("Secondary Special");
        bool switchB2Side = Input.GetButtonDown("Primary Special");
        bool xButtonRandomizer = Input.GetButtonDown("Attack");
        bool xButtonReset = Input.GetButton("Attack");
        bool bButton = Input.GetButtonDown("Dodge");

        if (canChangeMenu)
        {
            if (aButton)
            {
                if (currentMenu == "Title Menu")
                {
                    canChangeMenu = false;
                    mainLogo.GetComponent<Fade>().fadeOut = true;
                    startText.GetComponent<Fade>().fadeOut = true;
                    StartCoroutine(BlackOut(1, 3, 1));
                    StartCoroutine(SwitchCameras(2, titleCamera, menuCamera));
                } 
            } else if (bButton)
            {
                StartCoroutine(Back());
            }
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        if (currentMenu == "Fight Menu" || currentMenu == "Fight Options Menu")
        {
            if(currentSelected.name == "Fight Options Button" || currentSelected.name == "Mode Dropdown")
            {
                b1Side = true;
                b2Side = false;
            }

            if (switchB1Side)
            {
                if (currentSelected.name == "B2 FightStyleSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[0]);
                }
                else if (currentSelected.name == "B2 OutfitSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[1]);
                }
                else if (currentSelected.name == "B2 OutfitVariation")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[2]);
                }
                else if (currentSelected.name == "B2 HairTypeSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[3]);
                }
                else if (currentSelected.name == "B2 HairColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[4]);
                }
                else if (currentSelected.name == "B2 SkinColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[5]);
                }
                else if (currentSelected.name == "B2 NameSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[6]);
                }
                else if (currentSelected.name == "B2 SouvenirSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[7]);
                }

                b1Side = true;
                b2Side = false;
            } else if (switchB2Side)
            {
                if (currentSelected.name == "B1 FightStyleSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[0]);
                } else if(currentSelected.name == "B1 OutfitSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[1]);
                }
                else if (currentSelected.name == "B1 OutfitVariation")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[2]);
                }
                else if (currentSelected.name == "B1 HairTypeSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[3]);
                }
                else if (currentSelected.name == "B1 HairColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[4]);
                }
                else if (currentSelected.name == "B1 SkinColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[5]);
                }
                else if (currentSelected.name == "B1 NameSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[6]);
                }
                else if (currentSelected.name == "B1 SouvenirSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[7]);
                }

                b2Side = true;
                b1Side = false;
            }

            if (xButtonRandomizer)
            {
                if(currentMenu == "Fight Menu")
                {
                    RandomizeBrawler();
                }
                else if(currentMenu == "Fight Options Menu")
                {
                    RandomizeSouvenir();
                }
            }

            if (currentSelected.name == "B1 FightStyleSelection" || currentSelected.name == "B2 FightStyleSelection")
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

                b1FightStyleStats.SetActive(true);
                b2FightStyleStats.SetActive(true);
                BrawlerStatsBars();

            }
            else
            {
                b1FightStyleStats.SetActive(false);
                b2FightStyleStats.SetActive(false);
            }

            if (canSwitchOutfitSelection && (currentSelected.name == "B1 OutfitSelection" || currentSelected.name == "B2 OutfitSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(OutfitSelection("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(OutfitSelection("left"));
                }
            }

            if (canSwitchOutfitVariation && (currentSelected.name == "B1 OutfitVariation" || currentSelected.name == "B2 OutfitVariation"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(OutfitVariation("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(OutfitVariation("left"));
                }
            }

            if (canSwitchHairType && (currentSelected.name == "B1 HairTypeSelection" || currentSelected.name == "B2 HairTypeSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(HairType("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(HairType("left"));
                }
            }

            if (canSwitchHairColor && (currentSelected.name == "B1 HairColorSelection" || currentSelected.name == "B2 HairColorSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(HairColor("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(HairColor("left"));
                }
            }

            if (canSwitchSkinColor && (currentSelected.name == "B1 SkinColorSelection" || currentSelected.name == "B2 SkinColorSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(SkinColor("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(SkinColor("left"));
                }
            }

            if (canSwitchName && (currentSelected.name == "B1 NameSelection" || currentSelected.name == "B2 NameSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(Name("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(Name("left"));
                }
            }

            if (canSwitchSouvenir && (currentSelected.name == "B1 SouvenirSelection" || currentSelected.name == "B2 SouvenirSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(Souvenir("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(Souvenir("left"));
                }
            }

            gameModeSelection = gameModeDropDown.value;
            difficultySelection = difficultyDropdown.value;

            if(throwableToggle.isOn == true)
            {
                throwableAllowed = true;
                StateNameController.throwableSelection = 0;
            }
            else
            {
                throwableAllowed = false;
                StateNameController.throwableSelection = 1;
            }

            b1FightStyleText.text = fightStyles[b1FightStyle];
            b2FightStyleText.text = fightStyles[b2FightStyle];
            StateNameController.b1MainFightStyleSelection = b1FightStyle;
            StateNameController.b2MainFightStyleSelection = b2FightStyle;

            b1OutfitSelectionText.text = "" + b1OutfitSelection;
            b2OutfitSelectionText.text = "" + b2OutfitSelection;
            StateNameController.b1MainOutfit = b1OutfitSelection;
            StateNameController.b2MainOutfit = b2OutfitSelection;

            b1OutfitVariationText.text = "" + b1OutfitVariation;
            b2OutfitVariationText.text = "" + b2OutfitVariation;
            StateNameController.b1MainOutfitVariation = b1OutfitVariation;
            StateNameController.b2MainOutfitVariation = b2OutfitVariation;

            b1HairTypeText.text = "" + b1HairType;
            b2HairTypeText.text = "" + b2HairType;
            StateNameController.b1MainHairType = b1HairType;
            StateNameController.b2MainHairType = b2HairType;

            b1HairColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().hairColors[b1HairColor - 1].color;
            b2HairColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().hairColors[b2HairColor - 1].color;
            StateNameController.b1MainHairColor = b1HairColor;
            StateNameController.b2MainHairColor = b2HairColor;

            b1SkinColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().skinColors[b1SkinColor - 1].color;
            b2SkinColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().skinColors[b2SkinColor - 1].color;
            StateNameController.b1MainSkinColor = b1SkinColor;
            StateNameController.b2MainSkinColor = b2SkinColor;

            b1NameText.text = "" + fightMenu.GetComponent<BrawlerUpdates>().names[b1NameSelection];
            b2NameText.text = "" + fightMenu.GetComponent<BrawlerUpdates>().names[b2NameSelection];
            StateNameController.b1MainNameSelection = b1NameSelection;
            StateNameController.b2MainNameSelection = b2NameSelection;

            StateNameController.b1Name = "" + fightMenu.GetComponent<BrawlerUpdates>().names[b1NameSelection];
            StateNameController.b2Name = "" + fightMenu.GetComponent<BrawlerUpdates>().names[b2NameSelection];

            b1SouvenirText.text = "" + fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers[b1SouvenirSelection];
            b2SouvenirText.text = "" + fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers[b2SouvenirSelection];
            StateNameController.b1MainSouvenirSelection = b1SouvenirSelection;
            StateNameController.b2MainSouvenirSelection = b2SouvenirSelection;

            StateNameController.gameModeSelection = gameModeSelection;
            StateNameController.difficultySelection = difficultySelection;
        }
        else if(currentMenu == "Stats Menu")
        {
            if (xButtonReset)
            {
                if(resetCoroutine == null)
                    resetCoroutine = StartCoroutine(ResetStats());
            }
            else
            {
                resetAmount = 0;
            }

            resetFill.GetComponent<Image>().fillAmount = resetAmount / 1;
        }
        else if(currentMenu == "Settings Menu")
        {
            hudSelection = hudSelectionDropdown.value;

            if (vibrationToggle.isOn)
            {
                vibrationAllowed = true;
            }
            else
            {
                vibrationAllowed = false;
            }

            musicSliderValue = musicSlider.value;
            soundEffectsSliderValue = soundEffectsSlider.value;


            StateNameController.hudSelection = hudSelection;

            if (vibrationAllowed)
            {
                StateNameController.controllerVibration = 0;
            }
            else
            {
                StateNameController.controllerVibration = 1;
            }

            StateNameController.musicVolume = musicSliderValue;

            StateNameController.soundEffectsVolume = soundEffectsSliderValue;
        }
    }

    IEnumerator Back()
    {
        if(currentMenu == "Fight Menu")
        {
            menuCamera.GetComponent<SmoothDampCamera>().smoothDamp = true;
            fightMenu.SetActive(false);
            yield return new WaitForSeconds(1.5f);

            MainMenu();
        } 
        else if(currentMenu == "Fight Options Menu")
        {
            #region Managing Souvenirs' Objects
            if (fightOptionsMenu.GetComponent<FightOptionsMenu>().b1CurrentParticle != null)
                Destroy(fightOptionsMenu.GetComponent<FightOptionsMenu>().b1CurrentParticle);

            if (fightOptionsMenu.GetComponent<FightOptionsMenu>().b2CurrentParticle != null)
                Destroy(fightOptionsMenu.GetComponent<FightOptionsMenu>().b2CurrentParticle);

            if (fightOptionsMenu.GetComponent<FightOptionsMenu>().b1CurrentSuitCase != null)
                Destroy(fightOptionsMenu.GetComponent<FightOptionsMenu>().b1CurrentSuitCase);

            if (fightOptionsMenu.GetComponent<FightOptionsMenu>().b2CurrentSuitCase != null)
                Destroy(fightOptionsMenu.GetComponent<FightOptionsMenu>().b2CurrentSuitCase);

            #endregion

            fightOptionsMenu.SetActive(false);
            yield return new WaitForSeconds(.5f);

            StartCoroutine(FightMenuCoroutine(.25f));
        }
        else if (currentMenu == "Stats Menu")
        {
            statsMenu.SetActive(false);
            yield return new WaitForSeconds(.5f);

            MainMenu();
        }
        else if(currentMenu == "Options Menu")
        {
            for (int i = 0; i < optionsMenuParts.Length; i++)
            {
                optionsMenuParts[i].SetActive(false);
            }

            optionsMenu.SetActive(false);
            yield return new WaitForSeconds(.5f);

            MainMenu();
        }
        else if (currentMenu == "HTP Menu")
        {
            for (int i = 0; i < htpMenuParts.Length; i++)
            {
                htpMenuParts[i].SetActive(false);
            }

            OptionsMenu(); 
        }
        else if (currentMenu == "Controls Menu")
        {
            controlsMenu.SetActive(false);
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine());
        }
        else if (currentMenu == "Combat Menu")
        {
            combatMenu.SetActive(false);
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine());
        }
        else if (currentMenu == "Fight Styles Menu")
        {
            for (int i = 0; i < fightStylesMenuParts.Length; i++)
            {
                fightStylesMenuParts[i].SetActive(false);
            }
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine());
        }
        else if (currentMenu == "Fight Styles Individual Menu")
        {
            for (int i = 0; i < fightStylesIndividualMenus.Length; i++)
            {
                fightStylesIndividualMenus[i].SetActive(false);
            }
            optionsBackground.SetActive(false);

            StartCoroutine(FightStyleCoroutine());
        }
        else if (currentMenu == "Souvenirs Menu")
        {
            souvenirsMenu.SetActive(false);
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine()); 
        }
        else if (currentMenu == "HUDColors Menu")
        {
            hudColorsMenu.SetActive(false);
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine()); 
        }
        else if (currentMenu == "Throwables Menu")
        {
            throwablesMenu.SetActive(false);
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine()); 
        }
        else if (currentMenu == "Finisher Menu")
        {
            finisherMenu.SetActive(false);
            optionsBackground.SetActive(false);

            StartCoroutine(HTPMenuCoroutine());
        }
        else if (currentMenu == "Settings Menu")
        {
            settingsMenu.SetActive(false);
            optionsBackground.SetActive(false);

            GetComponent<SaveData>().SaveOptions();
            StartCoroutine(OptionsMenuCoroutine());
        }
        else if (currentMenu == "Credits Menu")
        {
            creditsMenu.SetActive(false);

            StartCoroutine(OptionsMenuCoroutine());
        }
    }

    IEnumerator MainLogoTime(float t)
    {
        yield return new WaitForSeconds(t);
        mainLogo.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(t);
        startText.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(1f);
        canChangeMenu = true;
    }

    IEnumerator BlackOut(float waitTime, float blackOutTime, float menuActivationWait)
    {
        yield return new WaitForSeconds(waitTime);
        blackOut.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(blackOutTime);
        blackOut.GetComponent<Fade>().fadeOut = true;
        yield return new WaitForSeconds(menuActivationWait);
        if(currentMenu == "Title Menu")
        {
            MainMenu();
        }
        canChangeMenu = true;
    }

    IEnumerator SwitchCameras(float waitTime, GameObject currentCamera, GameObject newCamera)
    {
        yield return new WaitForSeconds(waitTime);
        newCamera.SetActive(true);
        currentCamera.SetActive(false);
        canSwtichFightStyle = true;
        canSwitchOutfitSelection = true;
        canSwitchOutfitVariation = true;
        canSwitchHairType = true;
        canSwitchHairColor = true;
        canSwitchSkinColor = true;
        canSwitchName = true;
    }

    void ButtonPress()
    {
        soundEffectsAudioSource.clip = GetComponent<SoundManager>().buttonPress;
        soundEffectsAudioSource.Play();
    }

    public void MainMenu()
    {
        currentMenu = "Main Menu";
        mainMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(fightButton);
    }

    //FIGHT MENU
    public void FightMenu() //Used when Fight button is pressed
    {
        if (canChangeMenu)
        {
            menuCamera.GetComponent<SmoothDampCamera>().smoothDamp = true;
            StartCoroutine(FightMenuCoroutine(1.5f));
        }
    }

    public IEnumerator FightMenuCoroutine(float time)
    {
        currentMenu = "Fight Menu";

        ButtonPress();
        canChangeMenu = false;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(time);
        fightMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1FightStyleButton);
        b1Side = true;
        b2Side = false;
        fightMenu.GetComponent<BrawlerUpdates>().brawler1.transform.position = 
            new Vector3(fightMenu.GetComponent<BrawlerUpdates>().brawler1.transform.position.x, 
            fightMenu.GetComponent<BrawlerUpdates>().brawler1.transform.position.y, -1.26f);

        fightMenu.GetComponent<BrawlerUpdates>().brawler2.transform.position =
            new Vector3(fightMenu.GetComponent<BrawlerUpdates>().brawler2.transform.position.x,
            fightMenu.GetComponent<BrawlerUpdates>().brawler2.transform.position.y, -1.26f);

        canChangeMenu = true;
    }

    public void FightOptionsMenu()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(FightOptionsMenuCoroutine());
        }
    }

    IEnumerator FightOptionsMenuCoroutine()
    {
        currentMenu = "Fight Options Menu";

        canChangeMenu = false;
        fightMenu.SetActive(false);
        yield return new WaitForSeconds(.75f);
        fightOptionsMenu.SetActive(true);
        fightOptionsMenu.GetComponent<FightOptionsMenu>().b1SouvenirSelection = -1;
        fightOptionsMenu.GetComponent<FightOptionsMenu>().b2SouvenirSelection = -1;
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1SouvenirSouvenirButton);
        b1Side = true;
        b2Side = false;
        canSwitchSouvenir = true;
        canChangeMenu = true;
    }

    #region Fight Menu
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
            if (dir == "right")
            {
                if (b2FightStyle == fightStyles.Length - 1)
                {
                    b2FightStyle = 0;
                }
                else
                {
                    b2FightStyle++;
                }
            }
            else
            {
                if (b2FightStyle == 0)
                {
                    b2FightStyle = fightStyles.Length - 1;
                }
                else
                {
                    b2FightStyle--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwtichFightStyle = true;
    }

    void BrawlerStatsBars()
    {
        if(b1FightStyle == 0)
        {
            b1StatsBars[0].GetComponent<Image>().fillAmount = karateStats[0] / 5;
            b1StatsBars[1].GetComponent<Image>().fillAmount = karateStats[1] / 5;
            b1StatsBars[2].GetComponent<Image>().fillAmount = karateStats[2] / 5;
            b1StatsBars[3].GetComponent<Image>().fillAmount = karateStats[3] / 5;
            b1StatsBars[4].GetComponent<Image>().fillAmount = karateStats[4] / 5;
        } 
        else if (b1FightStyle == 1)
        {
            b1StatsBars[0].GetComponent<Image>().fillAmount = boxingStats[0] / 5;
            b1StatsBars[1].GetComponent<Image>().fillAmount = boxingStats[1] / 5;
            b1StatsBars[2].GetComponent<Image>().fillAmount = boxingStats[2] / 5;
            b1StatsBars[3].GetComponent<Image>().fillAmount = boxingStats[3] / 5;
            b1StatsBars[4].GetComponent<Image>().fillAmount = boxingStats[4] / 5;
        }
        else if (b1FightStyle == 2)
        {
            b1StatsBars[0].GetComponent<Image>().fillAmount = mmaStats[0] / 5;
            b1StatsBars[1].GetComponent<Image>().fillAmount = mmaStats[1] / 5;
            b1StatsBars[2].GetComponent<Image>().fillAmount = mmaStats[2] / 5;
            b1StatsBars[3].GetComponent<Image>().fillAmount = mmaStats[3] / 5;
            b1StatsBars[4].GetComponent<Image>().fillAmount = mmaStats[4] / 5;
        }
        else if (b1FightStyle == 3)
        {
            b1StatsBars[0].GetComponent<Image>().fillAmount = tkdStats[0] / 5;
            b1StatsBars[1].GetComponent<Image>().fillAmount = tkdStats[1] / 5;
            b1StatsBars[2].GetComponent<Image>().fillAmount = tkdStats[2] / 5;
            b1StatsBars[3].GetComponent<Image>().fillAmount = tkdStats[3] / 5;
            b1StatsBars[4].GetComponent<Image>().fillAmount = tkdStats[4] / 5;
        }
        else if (b1FightStyle == 4)
        {
            b1StatsBars[0].GetComponent<Image>().fillAmount = kungFuStats[0] / 5;
            b1StatsBars[1].GetComponent<Image>().fillAmount = kungFuStats[1] / 5;
            b1StatsBars[2].GetComponent<Image>().fillAmount = kungFuStats[2] / 5;
            b1StatsBars[3].GetComponent<Image>().fillAmount = kungFuStats[3] / 5;
            b1StatsBars[4].GetComponent<Image>().fillAmount = kungFuStats[4] / 5;
        }
        else if (b1FightStyle == 5)
        {
            b1StatsBars[0].GetComponent<Image>().fillAmount = wrestlingStats[0] / 5;
            b1StatsBars[1].GetComponent<Image>().fillAmount = wrestlingStats[1] / 5;
            b1StatsBars[2].GetComponent<Image>().fillAmount = wrestlingStats[2] / 5;
            b1StatsBars[3].GetComponent<Image>().fillAmount = wrestlingStats[3] / 5;
            b1StatsBars[4].GetComponent<Image>().fillAmount = wrestlingStats[4] / 5;
        }


        if (b2FightStyle == 0)
        {
            b2StatsBars[0].GetComponent<Image>().fillAmount = karateStats[0] / 5;
            b2StatsBars[1].GetComponent<Image>().fillAmount = karateStats[1] / 5;
            b2StatsBars[2].GetComponent<Image>().fillAmount = karateStats[2] / 5;
            b2StatsBars[3].GetComponent<Image>().fillAmount = karateStats[3] / 5;
            b2StatsBars[4].GetComponent<Image>().fillAmount = karateStats[4] / 5;
        }
        else if (b2FightStyle == 1)
        {
            b2StatsBars[0].GetComponent<Image>().fillAmount = boxingStats[0] / 5;
            b2StatsBars[1].GetComponent<Image>().fillAmount = boxingStats[1] / 5;
            b2StatsBars[2].GetComponent<Image>().fillAmount = boxingStats[2] / 5;
            b2StatsBars[3].GetComponent<Image>().fillAmount = boxingStats[3] / 5;
            b2StatsBars[4].GetComponent<Image>().fillAmount = boxingStats[4] / 5;
        }
        else if (b2FightStyle == 2)
        {
            b2StatsBars[0].GetComponent<Image>().fillAmount = mmaStats[0] / 5;
            b2StatsBars[1].GetComponent<Image>().fillAmount = mmaStats[1] / 5;
            b2StatsBars[2].GetComponent<Image>().fillAmount = mmaStats[2] / 5;
            b2StatsBars[3].GetComponent<Image>().fillAmount = mmaStats[3] / 5;
            b2StatsBars[4].GetComponent<Image>().fillAmount = mmaStats[4] / 5;
        }
        else if (b2FightStyle == 3)
        {
            b2StatsBars[0].GetComponent<Image>().fillAmount = tkdStats[0] / 5;
            b2StatsBars[1].GetComponent<Image>().fillAmount = tkdStats[1] / 5;
            b2StatsBars[2].GetComponent<Image>().fillAmount = tkdStats[2] / 5;
            b2StatsBars[3].GetComponent<Image>().fillAmount = tkdStats[3] / 5;
            b2StatsBars[4].GetComponent<Image>().fillAmount = tkdStats[4] / 5;
        }
        else if (b2FightStyle == 4)
        {
            b2StatsBars[0].GetComponent<Image>().fillAmount = kungFuStats[0] / 5;
            b2StatsBars[1].GetComponent<Image>().fillAmount = kungFuStats[1] / 5;
            b2StatsBars[2].GetComponent<Image>().fillAmount = kungFuStats[2] / 5;
            b2StatsBars[3].GetComponent<Image>().fillAmount = kungFuStats[3] / 5;
            b2StatsBars[4].GetComponent<Image>().fillAmount = kungFuStats[4] / 5;
        }
        else if (b2FightStyle == 5)
        {
            b2StatsBars[0].GetComponent<Image>().fillAmount = wrestlingStats[0] / 5;
            b2StatsBars[1].GetComponent<Image>().fillAmount = wrestlingStats[1] / 5;
            b2StatsBars[2].GetComponent<Image>().fillAmount = wrestlingStats[2] / 5;
            b2StatsBars[3].GetComponent<Image>().fillAmount = wrestlingStats[3] / 5;
            b2StatsBars[4].GetComponent<Image>().fillAmount = wrestlingStats[4] / 5;
        }
    }

    IEnumerator OutfitSelection(string dir)
    {
        canSwitchOutfitSelection = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1OutfitSelection == 2)
                {
                    b1OutfitSelection = 1;
                }
                else
                {
                    b1OutfitSelection++;
                }
            }
            else
            {
                if (b1OutfitSelection == 1)
                {
                    b1OutfitSelection = 2;
                }
                else
                {
                    b1OutfitSelection--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2OutfitSelection == 2)
                {
                    b2OutfitSelection = 1;
                }
                else
                {
                    b2OutfitSelection++;
                }
            }
            else
            {
                if (b2OutfitSelection == 1)
                {
                    b2OutfitSelection = 2;
                }
                else
                {
                    b2OutfitSelection--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchOutfitSelection = true;
    }

    IEnumerator OutfitVariation(string dir)
    {
        canSwitchOutfitVariation = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1OutfitVariation == 5)
                {
                    b1OutfitVariation = 1;
                }
                else
                {
                    b1OutfitVariation++;
                }
            }
            else
            {
                if (b1OutfitVariation == 1)
                {
                    b1OutfitVariation = 5;
                }
                else
                {
                    b1OutfitVariation--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2OutfitVariation == 5)
                {
                    b2OutfitVariation = 1;
                }
                else
                {
                    b2OutfitVariation++;
                }
            }
            else
            {
                if (b2OutfitVariation == 1)
                {
                    b2OutfitVariation = 5;
                }
                else
                {
                    b2OutfitVariation--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchOutfitVariation = true;
    }

    IEnumerator HairType(string dir)
    {
        canSwitchHairType = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1HairType == 10)
                {
                    b1HairType = 1;
                }
                else
                {
                    b1HairType++;
                }
            }
            else
            {
                if (b1HairType == 1)
                {
                    b1HairType = 10;
                }
                else
                {
                    b1HairType--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2HairType == 10)
                {
                    b2HairType = 1;
                }
                else
                {
                    b2HairType++;
                }
            }
            else
            {
                if (b2HairType == 1)
                {
                    b2HairType = 10;
                }
                else
                {
                    b2HairType--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchHairType = true;
    }

    IEnumerator HairColor(string dir)
    {
        canSwitchHairColor = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1HairColor == 9)
                {
                    b1HairColor = 1;
                }
                else
                {
                    b1HairColor++;
                }
            }
            else
            {
                if (b1HairColor == 1)
                {
                    b1HairColor = 9;
                }
                else
                {
                    b1HairColor--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2HairColor == 9)
                {
                    b2HairColor = 1;
                }
                else
                {
                    b2HairColor++;
                }
            }
            else
            {
                if (b2HairColor == 1)
                {
                    b2HairColor = 9;
                }
                else
                {
                    b2HairColor--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchHairColor = true;
    }

    IEnumerator SkinColor(string dir)
    {
        canSwitchSkinColor = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1SkinColor == 11)
                {
                    b1SkinColor = 1;
                }
                else
                {
                    b1SkinColor++;
                }
            }
            else
            {
                if (b1SkinColor == 1)
                {
                    b1SkinColor = 11;
                }
                else
                {
                    b1SkinColor--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2SkinColor == 11)
                {
                    b2SkinColor = 1;
                }
                else
                {
                    b2SkinColor++;
                }
            }
            else
            {
                if (b2SkinColor == 1)
                {
                    b2SkinColor = 11;
                }
                else
                {
                    b2SkinColor--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchSkinColor = true;
    }

    IEnumerator Name(string dir)
    {
        canSwitchName = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1NameSelection == fightMenu.GetComponent<BrawlerUpdates>().names.Length-1)
                {
                    b1NameSelection = 0;
                }
                else
                {
                    b1NameSelection++;
                }
            }
            else
            {
                if (b1NameSelection == 0)
                {
                    b1NameSelection = fightMenu.GetComponent<BrawlerUpdates>().names.Length - 1;
                }
                else
                {
                    b1NameSelection--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2NameSelection == fightMenu.GetComponent<BrawlerUpdates>().names.Length - 1)
                {
                    b2NameSelection = 0;
                }
                else
                {
                    b2NameSelection++;
                }
            }
            else
            {
                if (b2NameSelection == 0)
                {
                    b2NameSelection = fightMenu.GetComponent<BrawlerUpdates>().names.Length - 1;
                }
                else
                {
                    b2NameSelection--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchName = true;
    }

    void InitialRandomizer()
    {
        PlayerPrefs.SetInt("B1 FightStyle", Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().fightingTypeAnimators.Length));
        PlayerPrefs.SetInt("B1 OutfitType", Random.Range(1, 3));
        PlayerPrefs.SetInt("B1 OutfitVariation", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().karateOutfit1Variations.Length + 1));
        PlayerPrefs.SetInt("B1 HairType", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairStyles.Length + 1));
        PlayerPrefs.SetInt("B1 HairColor", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairColors.Length + 1));
        PlayerPrefs.SetInt("B1 SkinColor", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().skinColors.Length + 1));
        PlayerPrefs.SetInt("B1 NameSelection", Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().names.Length));

        PlayerPrefs.SetInt("B2 FightStyle", Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().fightingTypeAnimators.Length));
        PlayerPrefs.SetInt("B2 OutfitType", Random.Range(1, 3));
        PlayerPrefs.SetInt("B2 OutfitVariation", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().karateOutfit1Variations.Length + 1));
        PlayerPrefs.SetInt("B2 HairType", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairStyles.Length + 1));
        PlayerPrefs.SetInt("B2 HairColor", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairColors.Length + 1));
        PlayerPrefs.SetInt("B2 SkinColor", Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().skinColors.Length + 1));
        PlayerPrefs.SetInt("B2 NameSelection", Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().names.Length));
    }

    void RandomizeBrawler()
    {
        if (b1Side)
        {
            b1FightStyle = Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().fightingTypeAnimators.Length);
            b1OutfitSelection = Random.Range(1, 3);
            b1OutfitVariation = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().karateOutfit1Variations.Length+1);
            b1HairType = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairStyles.Length+1);
            b1HairColor = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairColors.Length+1);
            b1SkinColor = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().skinColors.Length+1);
            b1NameSelection = Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().names.Length);
        }
        else
        {
            b2FightStyle = Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().fightingTypeAnimators.Length);
            b2OutfitSelection = Random.Range(1, 3);
            b2OutfitVariation = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().karateOutfit1Variations.Length + 1);
            b2HairType = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairStyles.Length + 1);
            b2HairColor = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().hairColors.Length + 1);
            b2SkinColor = Random.Range(1, fightMenu.GetComponent<BrawlerUpdates>().skinColors.Length + 1);
            b2NameSelection = Random.Range(0, fightMenu.GetComponent<BrawlerUpdates>().names.Length);
        }


    }

    #endregion

    #region Fight Options Menu
    IEnumerator Souvenir(string dir)
    {
        canSwitchSouvenir = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1SouvenirSelection == fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers.Length - 1)
                {
                    b1SouvenirSelection = 0;
                }
                else
                {
                    b1SouvenirSelection++;
                }
            }
            else
            {
                if (b1SouvenirSelection == 0)
                {
                    b1SouvenirSelection = fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers.Length - 1;
                }
                else
                {
                    b1SouvenirSelection--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2SouvenirSelection == fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers.Length - 1)
                {
                    b2SouvenirSelection = 0;
                }
                else
                {
                    b2SouvenirSelection++;
                }
            }
            else
            {
                if (b2SouvenirSelection == 0)
                {
                    b2SouvenirSelection = fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers.Length - 1;
                }
                else
                {
                    b2SouvenirSelection--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchSouvenir = true;
    }

    void RandomizeSouvenir()
    {
        if (b1Side)
        {
            b1SouvenirSelection = Random.Range(0, fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers.Length);
        }
        else if (b2Side)
        {
            b2SouvenirSelection = Random.Range(0, fightOptionsMenu.GetComponent<FightOptionsMenu>().souvniers.Length);
        }
    }
    #endregion

    public void StatsMenu()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(StatsMenuCoroutine());
        }
    }

    IEnumerator StatsMenuCoroutine()
    {
        currentMenu = "Stats Menu";

        canChangeMenu = false;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(.75f);

        GetComponent<SaveData>().LoadStats();
        statsMenu.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }

    IEnumerator ResetStats()
    {
        resetAmount += .02f;
        yield return new WaitForSeconds(.1f);
        if (resetAmount >= 1) {
            StateNameController.totalFights = 0;
            StateNameController.totalWins = 0;
            StateNameController.winRate = 0;
            for (int i = 0; i < StateNameController.fightStyleUsage.Length; i++)
            {
                StateNameController.fightStyleUsage[i] = 0;
            }
            StateNameController.avgBasicAttackConnectionRate = 0;
            StateNameController.avgDamageInflicted = 0;
            StateNameController.avghealthRecovered = 0;
            StateNameController.avgStaminaUsed = 0;
            StateNameController.avgDodgesPerformed = 0;
            for (int i = 0; i < StateNameController.souvenirUsage.Length; i++)
            {
                StateNameController.souvenirUsage[i] = 0;
            }

            GetComponent<SaveData>().SaveStats();
            GetComponent<SaveData>().LoadStats();
        }

        resetCoroutine = null;
    }

    public void OptionsMenu()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(OptionsMenuCoroutine());
        }
    }

    IEnumerator OptionsMenuCoroutine()
    {
        currentMenu = "Options Menu";

        canChangeMenu = false;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(.75f);
        optionsMenu.SetActive(true);

        for(int i = 0; i < optionsMenuParts.Length; i++)
        {
            optionsMenuParts[i].SetActive(true);
        }

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(howToPlayButton);
        canChangeMenu = true;
    }

    #region How To Play
    public void HTPMenu()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(HTPMenuCoroutine());
        }
    }

    IEnumerator HTPMenuCoroutine()
    {
        currentMenu = "HTP Menu";

        canChangeMenu = false;
        for (int i = 0; i < optionsMenuParts.Length; i++)
        {
            optionsMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);
        htpMenu.SetActive(true);

        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(true);
        }

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(controlsButton);
        canChangeMenu = true;
    }

    public void Controls()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(ControlsCoroutine());
        }
    }

    IEnumerator ControlsCoroutine()
    {
        currentMenu = "Controls Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        controlsMenu.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }

    public void Combat()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(CombatCoroutine());
        }
    }

    IEnumerator CombatCoroutine()
    {
        currentMenu = "Combat Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        combatMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }

    public void FightStyles()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(FightStyleCoroutine());
        }
    }

    IEnumerator FightStyleCoroutine()
    {
        currentMenu = "Fight Styles Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        for (int i = 0; i < fightStylesMenuParts.Length; i++)
        {
            fightStylesMenuParts[i].SetActive(true);
        }

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(karateFightStyleButton);
        canChangeMenu = true;
    }

    public void FightStyleSubMenu(string fightStyle)
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(FightStyleSubMenuCoroutine(fightStyle));
        }
    }

    IEnumerator FightStyleSubMenuCoroutine(string fs)
    {
        currentMenu = "Fight Styles Individual Menu";

        canChangeMenu = false;
        for (int i = 0; i < fightStylesMenuParts.Length; i++)
        {
            fightStylesMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        if(fs == "Karate")
        {
            fightStylesIndividualMenus[0].SetActive(true);
        }
        else if (fs == "Boxing")
        {
            fightStylesIndividualMenus[1].SetActive(true);
        }
        else if (fs == "MMA")
        {
            fightStylesIndividualMenus[2].SetActive(true);
        }
        else if (fs == "Taekwondo")
        {
            fightStylesIndividualMenus[3].SetActive(true);
        }
        else if (fs == "Kung Fu")
        {
            fightStylesIndividualMenus[4].SetActive(true);
        }
        else if (fs == "Wrestling")
        {
            fightStylesIndividualMenus[5].SetActive(true);
        }

        fightStylesMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(karateFightStyleButton);
        canChangeMenu = true;
    }

    public void Souvenirs()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(SouvenirsCoroutine());
        }
    }


    IEnumerator SouvenirsCoroutine()
    {
        currentMenu = "Souvenirs Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        souvenirsMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }

    public void HUDColors()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(HUDColorsCoroutine());
        }
    }

    IEnumerator HUDColorsCoroutine()
    {
        currentMenu = "HUDColors Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        hudColorsMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }

    public void Throwables()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(ThrowablesCoroutine());
        }
    }

    IEnumerator ThrowablesCoroutine()
    {
        currentMenu = "Throwables Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        throwablesMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }

    public void Fisnisher()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(FisnisherCoroutine());
        }
    }

    IEnumerator FisnisherCoroutine()
    {
        currentMenu = "Finisher Menu";

        canChangeMenu = false;
        for (int i = 0; i < htpMenuParts.Length; i++)
        {
            htpMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);

        finisherMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }
    #endregion

    public void SettingsMenu()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(SettingsMenuCoroutine());
        }
    }

    IEnumerator SettingsMenuCoroutine()
    {
        currentMenu = "Settings Menu";

        canChangeMenu = false;
        GetComponent<SaveData>().LoadOptions();
        for (int i = 0; i < optionsMenuParts.Length; i++)
        {
            optionsMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);
        settingsMenu.SetActive(true);
        optionsBackground.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(hudSelectionDropdown.gameObject);
        canChangeMenu = true;
    }

    public void CreditsMenu()
    {
        if (canChangeMenu)
        {
            ButtonPress();
            StartCoroutine(CreditsMenuCoroutine());
        }
    }

    IEnumerator CreditsMenuCoroutine()
    {
        currentMenu = "Credits Menu";

        canChangeMenu = false;
        for (int i = 0; i < optionsMenuParts.Length; i++)
        {
            optionsMenuParts[i].SetActive(false);
        }
        yield return new WaitForSeconds(.75f);
        creditsMenu.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        canChangeMenu = true;
    }
}
