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
    }
    public override void OnDeath()
    {
        Debug.Log("Dead");
        Application.Quit(); // Application.LoadLevel(DeathScreen);
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
}