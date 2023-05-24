using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}