using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public StarterAssetsInputs _input;

    private bool isPaused = false;

    void Start()
    {

        pauseMenu.SetActive(false);

    }

    void Update() {

        if (_input.pause) {
            Pause();
        }

    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;

        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);

    }

    public void ResumeGame() {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;


        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);

    }

    public void RestartGame() {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void MainMenu() {

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}