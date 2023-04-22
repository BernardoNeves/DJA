using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, HealthInterface
{
    [Header("Health")]
    public float _initialHealth;
    public float _initialMaxHealth;

    [SerializeField] Healthbar _healthbar;


    void Start()
    {
        GameManager.gameManager._playerHealth.Health = _initialHealth;
        GameManager.gameManager._playerHealth.MaxHealth = _initialMaxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            Damage(20);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            Heal(20);
        }
    }

    public void Damage(float dmg)
    {
        GameManager.gameManager._playerHealth.Damage(dmg);
        Debug.Log(GameManager.gameManager._playerHealth.Health);
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            // KILL PLAYER
        }

        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }
    public void Heal(float heal)
    {
        GameManager.gameManager._playerHealth.Heal(heal);
        Debug.Log(GameManager.gameManager._playerHealth.Health);

        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }
}