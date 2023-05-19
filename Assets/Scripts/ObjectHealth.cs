using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectHealth : HealthManager, HealthInterface
{
    public GameObject damageText;
   
    public ObjectHealth(float health, float maxHealth) : base(health, maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }

    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        if (damageText)
            ShowDamageText(damageAmount);
    }


    public void ShowDamageText(float damageAmount)
    {
        GameObject text = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        text.GetComponent<TMP_Text>().text = damageAmount.ToString();

    }
}