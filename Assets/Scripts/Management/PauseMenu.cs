using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject eventSystem;
    public GameObject pauseMenu;
    public GameObject resumeButton;

    private void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool pause = Input.GetKeyDown(KeyCode.JoystickButton7);
        bool back = Input.GetButtonDown("Dodge");

        if(UniversalFight.fight == false) { return; }

        if ((pause && !BackMenu.gamePaused) || (back && gamePaused))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        GetComponent<Vibrations>().vIntensity = 0;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(resumeButton);
        gamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gamePaused = false;
    }
}
