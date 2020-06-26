using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayGunDetails : MonoBehaviour
{

  //  public EquippedGunBehaviour egb;
    public EquippedGun equippedGun;

    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI gunName;
    public Slider timeSlider;
    public float x;
    void Start()
    {
        
    }

    // Change this entire thing later from Update to when required only.
    void Update()
    {
        timeSlider.maxValue = equippedGun.sliderMax;
        timeSlider.value = equippedGun.actionTime;
        currentAmmo.text = equippedGun.inventory.status[equippedGun.currentGun.name].ToString() + " / " + equippedGun.currentGun.maxClips.ToString();
        gunName.text = equippedGun.currentGun.name;
    }
}
