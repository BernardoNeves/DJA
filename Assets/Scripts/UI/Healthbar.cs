using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    Slider _healthSlider;

    private void Start() {

        _healthSlider = GetComponent<Slider>();

    }

    public void SetMaxHealth(float maxHealth) {

        _healthSlider.maxValue = maxHealth;
    }

    public void SetHealth(float health) {

        _healthSlider.value = health; 

    }

}   