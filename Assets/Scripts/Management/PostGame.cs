using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour
{
    [Header("Post-Game")]
    public GameObject eventSystem;
    public GameObject blackOut;
    public TextMeshProUGUI winnerText;
    public GameObject postGameMenu;
    public GameObject playAgainButton;

    public enum Scene
    {
        MainMenu,
        Dojo
    }

    private void Awake()
    {
        winnerText.GetComponent<CanvasGroup>().alpha = 0;
    }

    public IEnumerator ShowboatCompleteCoroutine()
    {
        yield return new WaitForSeconds(.5f);

        if (UniversalFight.usingMenuData)
        {
            if (GetComponent<IdManagear>().brawler1.GetComponent<Health>().health <= 0)
            {
                winnerText.text = StateNameController.b2Name + "\nWINS";
            }
            else
            {
                winnerText.text = StateNameController.b1Name + "\nWINS";
            }
        }
        else
        {
            winnerText.text = "BRAWLER \nWINS";
        }

        winnerText.GetComponent<Fade>().fadeIn = true;

        yield return new WaitForSeconds(3);

        winnerText.GetComponent<Fade>().fadeOut = true;

        yield return new WaitForSeconds(2);

        postGameMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(playAgainButton);
    }

    public void MainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        StateNameController.useMainMenu = false;
        blackOut.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
