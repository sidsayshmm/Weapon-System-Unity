using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayGunDetails : MonoBehaviour
{

  //  public EquippedGunBehaviour egb;
    public EquippedGun equippedGun;

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
       // Debug.Log("0" + equippedGun.period);
       // Debug.Log("1 " + equippedGun.inventory);
       // Debug.Log("2 " + equippedGun.inventory.status[equippedGun.currentGun.name]);
        currentAmmo.text = equippedGun.inventory.status[equippedGun.currentGun.name].ToString() + " / " + equippedGun.currentGun.maxClips.ToString();
        // gunName.text = egb.currentGun.gunName;

        gunName.text = equippedGun.currentGun.name;
    }
}
