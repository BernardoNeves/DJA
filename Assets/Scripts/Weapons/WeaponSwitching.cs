using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WeaponSwitching : MonoBehaviour {

    public StarterAssetsInputs _input;

    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start() {

        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;

    }

    private void SetWeapons() {

        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {

            weapons[i] = transform.GetChild(i);

        }
    }

    private void Update() {

        int previousSelectedWeapon = selectedWeapon;

        /*
        for (int i = 0; i < keys.Length; i++) {

            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime) {

                selectedWeapon = i;

            }

        }

        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);
        */
        if (_input.weaponSwap >= 1 || _input.weaponSwap <= -1)
        {
            if (selectedWeapon == 0)
            {
                selectedWeapon = 1;
            }
            else
            {
                selectedWeapon = 0;
            }
            Select(selectedWeapon);
        }


        timeSinceLastSwitch += Time.deltaTime;

    }

    private void Select(int weaponIndex) {
    
        for (int i = 0; i < weapons.Length; i++) {

            weapons[i].gameObject.SetActive(i == weaponIndex);

        }

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();

    }

    private void OnWeaponSelected() { }

}