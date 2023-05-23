using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {

    public TMP_Text waveText;
    private GameManager gameManager;

    private void Start() {

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null) {

            Debug.LogError("No GameManager found in the Scene");
            return;

        }

    }


    private void Update() {

        waveText.text = "Wave " + gameManager.waveNumber.ToString();

    }

}