using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.CursorToggle(true);
    }

    public void ResumeGame()
    {
        GameManager.instance.CursorToggle(false);
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}