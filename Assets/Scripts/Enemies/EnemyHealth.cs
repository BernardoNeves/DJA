using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, HealthInterface
{
    public GameObject chestPrefab;


    [Header("Health")]
    public float _initialHealth;
    public float _initialMaxHealth;

    public HealthManager _enemyHealth;

    void Start()
    {
        _enemyHealth = new HealthManager(_initialHealth, _initialMaxHealth);
    }

    void Update()
    {
        if (_enemyHealth.Health <= 0)
        {
            GameObject chest = Instantiate(chestPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    public void Damage(float dmg)
    {
        _enemyHealth.Damage(dmg);
        Debug.Log(_enemyHealth.Health);
    }
    public void Heal(float heal)
    {
        _enemyHealth.Heal(heal);
        Debug.Log(_enemyHealth.Health);
    }
}