using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : HealthManager, HealthInterface
{
    [Header("UI")]
    [SerializeField] Healthbar _healthbar;
    [SerializeField] Shieldbar _shieldbar;
    public GameObject damageText;

    private EnemySpawner enemySpawner;

    public override void Start()
    {
        base.Start();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    
    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        if(damageText)
            ShowDamageText(damageAmount);
    }

    public override void Update()
    {
        base.Update();
        _healthbar.SetHealth(Health);
        _healthbar.SetMaxHealth(MaxHealth);

        if (_shieldbar)
        {
            _shieldbar.SetShield(Shield);
            _shieldbar.SetMaxShield(MaxShield);
        }
    }

    public EnemyHealth(float health, float maxHealth, float shield, float maxShield) : base(health, maxHealth, shield, maxShield)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        _currentShield = shield;
        _currentMaxShield = maxShield;

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

    public void ShowDamageText(float damageAmount)
    {
        GameObject text = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        text.GetComponent<TMP_Text>().text = damageAmount.ToString();
     
    }
}