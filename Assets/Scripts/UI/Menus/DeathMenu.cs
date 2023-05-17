using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public GameObject deathMenu;

    private bool isPaused = false;

    void Start () { 
    
        deathMenu.SetActive(false);
    
    }

    void Update () {

        if (GameManager.instance.PlayerHealth.Health == 0) {
            Death();
        }

    }

    void Death () {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;

        Time.timeScale = 0f;
        isPaused = true;
        deathMenu.SetActive(true);

    }

    public void Restart () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void LoadMainMenu  () {

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}