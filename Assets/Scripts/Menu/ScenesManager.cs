using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        MainMenu,
        Dojo
    }

    public void LoadDojo()
    {
        GameObject.Find("Menu Manager").GetComponent<SaveData>().SaveBrawler();
        SceneManager.LoadScene(Scene.Dojo.ToString());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
