using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shieldbar : MonoBehaviour {

    Slider _shieldSlider;
    private float _slideSpeed = 500;
    private float _target = 100;

    private void Start() {

        _shieldSlider = GetComponent<Slider>();
    }

    public void SetMaxShield(float maxHealth) {
        _shieldSlider.maxValue = maxHealth;
    }

    public void SetShield(float health) {
        _target = health;
    }

    private void FixedUpdate()
    {
        _shieldSlider.value = Mathf.MoveTowards(_shieldSlider.value, _target, _slideSpeed * Time.deltaTime);
    }
}   