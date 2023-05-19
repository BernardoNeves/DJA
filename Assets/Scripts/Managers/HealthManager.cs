using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class HealthManager : MonoBehaviour
{
    [Header("Health")]
    public float _currentHealth;
    public float _currentMaxHealth;
    [Header("Shield")]
    public float _currentShield;
    public float _currentMaxShield;
    public float _shieldRechargeAmount;
    public float _shieldRechargeRate;
    public float _shieldRechargeCooldown;

    private float _timeSinceLastDamage = 0f;


    [SerializeField]

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

    public float Shield
    {
        get
        {
            return _currentShield;
        }
        set
        {
            _currentShield = value;
        }
    }

    public float MaxShield
    {
        get
        {
            return _currentMaxShield;
        }
        set
        {
            _currentMaxShield = value;
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

    public virtual void Start()
    {
        StartCoroutine(ShieldRegen());
    }

    public virtual void Update()
    {
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }

        if (_currentShield < 0)
        {
            _currentShield = 0;
        }
        if (_currentShield > _currentMaxShield)
        {
            _currentShield = _currentMaxShield;
        }
        _timeSinceLastDamage += Time.deltaTime;
    }

    public virtual void Damage(float damageAmount)
    {
        _timeSinceLastDamage = 0f;

        if (_currentShield > 0)
        {
            _currentShield -= damageAmount;
        }
        if (_currentShield <= 0)
        {
            _currentShield = 0;

            if (_currentHealth > 0)
            {
                _currentHealth -= damageAmount;
            }
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDeath();
            }
        }
    }

    public virtual void Heal(float healAmount)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        else if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }

    IEnumerator ShieldRegen()
    {
        if (_timeSinceLastDamage >= _shieldRechargeCooldown)
        {
            if (_currentShield < _currentMaxShield)
            {
                _currentShield += _shieldRechargeAmount;
            }
            else if (_currentShield > _currentMaxShield)
            {
                _currentShield = _currentMaxShield;
            }
        }
        yield return new WaitForSeconds(1/_shieldRechargeRate);
        StartCoroutine(ShieldRegen());
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }

}
