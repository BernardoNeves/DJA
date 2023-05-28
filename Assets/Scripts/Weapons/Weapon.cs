using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header("References")]
    public GunData weaponData;
    public Animator animator;
    public WeaponUI _weaponUI;

    private float timeSinceLastAttack = 0;
    public bool isAttacking = false;

    public void OnEnable() {
        PlayerBehaviour.shootInput += Attack;
        _weaponUI.UpdateInfo(1, 1);
    }

    private void OnDisable()
    {
        PlayerBehaviour.shootInput -= Attack;
    }

    private bool CanAttack() {

        float timeBetweenAttack = 1f / (weaponData.fireRate / 60f);


        if (weaponData.reloading == true) {

            return false;

        } else if (timeSinceLastAttack < timeBetweenAttack) {

            return false;

        }
        return true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isAttacking && collider.tag != "Player")
        {
            HealthInterface healthInterface = collider.transform.GetComponent<HealthInterface>();
            healthInterface?.Damage(weaponData.damage);
            EnemyHealth enemyHealth = collider.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null && enemyHealth.Health > 0)
            {
                GameManager.instance.Player.GetComponent<PlayerHealth>().CallItemOnHit(enemyHealth, weaponData.damage);
            }
        }
    }

    public void Attack() {

        if (CanAttack()) {
            animator.SetTrigger("Attack");
            StartCoroutine(Attacking());
            timeSinceLastAttack = 0;
        }
    }
    
    private void Update() {
        timeSinceLastAttack += Time.deltaTime;
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.35f);
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }
}