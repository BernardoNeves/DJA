using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour {

    public TMP_Text magazineSizeText;
    public TMP_Text magazineBulletText;

    public void UpdateInfo(int magazineSize, int magazineBullets) {

        magazineSizeText.text = magazineSize.ToString();
        magazineBulletText.text = magazineBullets.ToString();

    }
}