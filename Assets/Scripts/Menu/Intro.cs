using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        Dojo
    }

    void Start()
    {
        Application.targetFrameRate = -1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(SwitchScene());
    }



    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
