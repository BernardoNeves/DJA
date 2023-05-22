using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour {

    Slider _healthSlider;
    TMP_Text _healthText;
    private float _slideSpeed = 500;
    private float _target = 100;

    private void Start() {
        _healthText = GetComponentInChildren<TMP_Text>();
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
        _healthText.text = Mathf.RoundToInt(_healthSlider.value).ToString() + "/" + _healthSlider.maxValue.ToString();

    }
}   