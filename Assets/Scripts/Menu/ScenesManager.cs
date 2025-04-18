using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public GameObject blackOut;
    AudioSource soundEffectsAudioSource;

    private void Awake()
    {
        Instance = this;
        soundEffectsAudioSource = GameObject.Find("Sound Effects").GetComponent<AudioSource>();
    }

    public enum Scene
    {
        MainMenu,
        Dojo
    }

    public void LoadDojo()
    {
        GameObject.Find("Menu Manager").GetComponent<SaveData>().SaveBrawler();
        GameObject.Find("Menu Manager").GetComponent<SaveData>().SaveFightOptions();
        StartCoroutine(LoadingDojo());
    }

    IEnumerator LoadingDojo()
    {
        blackOut.GetComponent<Fade>().fadeIn = true;
        GameObject.Find("Menu Manager").GetComponent<Fade>().fadeOut = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Scene.Dojo.ToString());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public void Exit()
    {
        ButtonPress();
        Application.Quit();
    }

    void ButtonPress()
    {
        soundEffectsAudioSource.clip = GameObject.Find("Menu Manager").GetComponent<SoundManager>().buttonPress;
        soundEffectsAudioSource.Play();
    }
}
