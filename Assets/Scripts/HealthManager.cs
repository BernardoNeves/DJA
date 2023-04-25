using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager
{
    public float _currentHealth;
    public float _currentMaxHealth;

    public float Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }
    
    public float MaxHealth
    {
        get
        {
            return _currentMaxHealth;
        }
        set
        {
            _currentMaxHealth = value;
        }
    }

    public HealthManager(float health, float maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        if (_currentHealth > _currentMaxHealth)
            {
                _currentHealth = _currentMaxHealth;
            }
    }

    public void Damage(float damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
        }
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
    }
    public void Heal(float healAmount)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }

}
