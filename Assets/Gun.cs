using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] WeaponUI _weaponUI;

    float timeSinceLastShot;
        
    public void Start() {

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {

        if (!gunData.reloading && this.gameObject.activeSelf) {

            StartCoroutine(Reload());
        
        }

    }

    private IEnumerator Reload()
    {

        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() {

        float timeBetweenShot = 1f / (gunData.fireRate / 60f);


        if (gunData.reloading == true) {

            return false;

        } else if (timeSinceLastShot < timeBetweenShot) {

            return false;

        } else {

            return true;

        }

    }

    public void Shoot() {

        var ray = new Ray(PlayerCamera.position, PlayerCamera.forward);
        RaycastHit hitInfo;

        if (gunData.currentAmmo > 0) {

            if (CanShoot()) {

                if (Physics.Raycast(ray, out hitInfo, gunData.maxDistance))
                {

                    HealthInterface healthInterface = hitInfo.transform.GetComponent<HealthInterface>();
                    healthInterface?.Damage(gunData.damage);

                } 

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShoot();

            }

        }

    }
    
    private void Update() {

        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(PlayerCamera.position, PlayerCamera.forward * gunData.maxDistance);

        _weaponUI.UpdateInfo(gunData.magSize, gunData.currentAmmo);

    }

    private void OnGunShoot() { }   

}