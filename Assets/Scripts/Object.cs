using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour, HealthInterface
{
    [Header("Health")]
    public float _initialHealth;
    public float _initialMaxHealth;

    public HealthManager _objectHealth;

    void Start()
    {
        _objectHealth = new HealthManager(_initialHealth, _initialMaxHealth);
    }

    void Update()
    {
        if (_objectHealth.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float dmg)
    {
        _objectHealth.Damage(dmg);
        Debug.Log(_objectHealth.Health);
    }
    public void Heal(float heal)
    {
        _objectHealth.Heal(heal);
        Debug.Log(_objectHealth.Health);
    }
}