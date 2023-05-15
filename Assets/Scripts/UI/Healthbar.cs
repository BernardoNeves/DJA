using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    Slider _healthSlider;
    private float _slideSpeed = 500;
    private float _target = 100;

    private void Start() {

        _healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth) {
        _healthSlider.maxValue = maxHealth;
    }

    public void SetHealth(float health) {
        _target = health;
    }

    private void FixedUpdate()
    {
        _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, _target, _slideSpeed * Time.deltaTime);
    }
}   