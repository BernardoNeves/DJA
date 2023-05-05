using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {

    public GameObject victoryMenu;

    private bool isPaused = false;
/*
    void Start () {

        victoryMenu.SetActive(false);

    }


    void Update () {

        if (GameManager.instance.GameWon) {

            Victory();

        }
    
    }
*/

    public void Victory () {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;

        Time.timeScale = 0f;
        isPaused = true;
        victoryMenu.SetActive(true);
    
    }

    public void Resume () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;

        Debug.Log(isPaused);

        Time.timeScale = 1f;
        isPaused = false;
        victoryMenu.SetActive(false);

        Debug.Log(isPaused);

    }

    public void Restart () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");

    }

    public void LoadMainMenu () {

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    } 

}