using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayGunDetails : MonoBehaviour
{

    public EquippedGun equippedGun;

    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI sightMode; 
    public Slider timeSlider;
    public float x;

    private void Start()
    {

    }
    
    private void Update()
    {
        timeSlider.maxValue = equippedGun.sliderMax;
        timeSlider.value = equippedGun.actionTime;
        currentAmmo.text = equippedGun.inventory.status[equippedGun.currentGun.name].ToString() + " / " + equippedGun.currentGun.maxAmmo.ToString();
        gunName.text = equippedGun.currentGun.name;
        sightMode.text = equippedGun.currentSightMode.ToString();
    }
}
