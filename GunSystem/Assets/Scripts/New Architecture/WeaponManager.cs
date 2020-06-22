using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public AllGuns allGuns;
    [SerializeField] private int typeIndex = 1;
    [SerializeField] private int gunIndex = 0;
    private BaseGunDefinition currentGun;
    [SerializeField]private EquippedGun equippedGun;

    public void Start()
    {
        //Debug.Log("INITIALISING FIRST GUN");
        if (typeIndex == 1)
            currentGun = allGuns.primaryGuns[gunIndex];
        else
            currentGun = allGuns.secondaryGuns[gunIndex];

        equippedGun.UpdateGun(currentGun);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ChangeWeapon(true);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ChangeWeapon(false);
    }

    private void ChangeWeapon(bool next)
    {
        // Use the bool next when meelee weapons added.
        if (typeIndex == 1) 
            typeIndex = 2;
        else 
            typeIndex = 1;

    }

    private void UpdateGun()
    {

    }
}
