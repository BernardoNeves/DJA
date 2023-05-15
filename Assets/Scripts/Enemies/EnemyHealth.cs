using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : HealthManager, HealthInterface
{
    [SerializeField] Healthbar _healthbar;
    public EnemySpawner enemySpawner;
    public GameObject damageText;

    void Start() {

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

    public void ShowDamageText(float damageAmount)
    {
        GameObject text = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        text.GetComponent<TMP_Text>().text = damageAmount.ToString();
     
    }
}