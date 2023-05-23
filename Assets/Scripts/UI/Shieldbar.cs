using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shieldbar : MonoBehaviour {

    Slider _shieldSlider;
    TMP_Text _shieldText;
    private float _slideSpeed = 400;
    private float _target = 100;

    private void Start() {
        _shieldText = GetComponentInChildren<TMP_Text>();
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
        _shieldText.text = Mathf.RoundToInt(_shieldSlider.value).ToString() + "/" + _shieldSlider.maxValue.ToString();
    }
}   