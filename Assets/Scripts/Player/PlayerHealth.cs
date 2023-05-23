using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager, HealthInterface
{
    [Header("UI")]
    [SerializeField] Healthbar _healthbar;
    [SerializeField] Shieldbar _shieldbar;

    [Header("Inventory")]
    public List<ItemStack> itemList = new List<ItemStack>();

    public PlayerHealth(float health, float maxHealth, float shield, float maxShield) : base(health, maxHealth, shield, maxShield)
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
    public override void Start()
    {
        base.Start();
        StartCoroutine(CallItemUpdate());
    }

    public override void Update()
    {
        base.Update();
        _healthbar.SetHealth(Health);
        _healthbar.SetMaxHealth(MaxHealth);

        _shieldbar.SetShield(Shield);
        _shieldbar.SetMaxShield(MaxShield);

    }

    public override void OnDeath()
    {
        Debug.Log("Dead");
    }

    IEnumerator CallItemUpdate()
    {
        foreach (ItemStack i in itemList)
        {
            i.Item.Update(this, i.Stacks);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }

    public void CallItemOnPickup()
    {
        foreach (ItemStack i in itemList)
        {
            i.Item.OnPickup(i.Stacks);
        }
    }

    public void CallItemOnHit(EnemyHealth enemyHealth, float damageamount)
    {
        foreach (ItemStack i in itemList)
        {
            i.Item.OnHit(enemyHealth, damageamount, i.Stacks);
        }
    }

}