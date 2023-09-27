using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenu : MonoBehaviour
{
    public GameObject backMenu;
    public static bool gamePaused = false;

    private void Start()
    {
        gamePaused = false;
    }

    void Update()
    {
        bool pause = Input.GetKeyDown(KeyCode.JoystickButton6);
        bool back = Input.GetButtonDown("Dodge");

        if (UniversalFight.fight == false) { return; }

        if ((pause && !PauseMenu.gamePaused) || (back && gamePaused))
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
        backMenu.SetActive(true);
        gamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        backMenu.SetActive(false);
        gamePaused = false;
    }
}
