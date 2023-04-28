using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : HealthManager, HealthInterface
{
    public ObjectHealth(float health, float maxHealth) : base(health, maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
        
    }
}