using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager, HealthInterface
{
    [SerializeField] Healthbar _healthbar;

    public List<ItemStack> itemList = new List<ItemStack>();

    public PlayerHealth(float health, float maxHealth) : base(health, maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }
    private void Start()
    {
        StartCoroutine(CallItemUpdate());
    }

    public override void Update()
    {
        base.Update();
        _healthbar.SetHealth(Health);
        _healthbar.SetMaxHealth(MaxHealth);

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

    public void CallItemOnHit(EnemyHealth enemyHealth)
    {
        foreach (ItemStack i in itemList)
        {
            i.Item.OnHit(enemyHealth, i.Stacks);
        }
    }
}