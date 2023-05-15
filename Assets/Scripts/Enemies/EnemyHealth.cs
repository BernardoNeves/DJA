using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthManager, HealthInterface
{
    [SerializeField] Healthbar _healthbar;
    public EnemySpawner enemySpawner;

    void Start() {

        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    
    }

    public override void Update()
    {
        _healthbar.SetHealth(Health);
        _healthbar.SetMaxHealth(MaxHealth);
    }

    public EnemyHealth(float health, float maxHealth) : base(health, maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        enemySpawner.enemyCount--;
    }
}