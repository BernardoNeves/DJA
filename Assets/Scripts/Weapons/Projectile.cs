using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage;

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Player")
        {

            GameManager.instance.PlayerHealth.Damage(damage);
            Debug.Log("You Got Hit");

        }
            //Destroy(this.gameObject);
    }

}