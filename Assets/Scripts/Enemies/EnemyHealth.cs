using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthManager, HealthInterface
{
    public GameObject chestPrefab;

    public EnemyHealth(float health, float maxHealth) : base(health, maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }

    public override void Heal(float healAmount)
    {
        base.Heal(healAmount);
        Debug.Log(Health);
    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        Debug.Log(Health);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameObject chest = Instantiate(chestPrefab, transform.position, Quaternion.identity);
    }
}