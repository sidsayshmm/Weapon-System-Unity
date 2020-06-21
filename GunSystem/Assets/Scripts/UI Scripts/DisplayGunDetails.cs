using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayGunDetails : MonoBehaviour
{

  //  public EquippedGunBehaviour egb;
    public EquippedGun weaponManager;

    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI gunName;

    void Start()
    {
        // egb = egb.GetComponent<EquippedGunBehaviour>();
    }

    // Change this entire thing later from Update to when required only.
    void Update()
    {
      //  currentAmmo.text = egb.inventory.status[egb.currentGun.name].ToString() + " / " + egb.currentGun.maxAmmo.ToString();
        currentAmmo.text = weaponManager.inventory.status[weaponManager.currentGun.name].ToString() + " / " + weaponManager.currentGun.maxClips.ToString();
        // gunName.text = egb.currentGun.gunName;

        gunName.text = weaponManager.currentGun.name;
    }
}
