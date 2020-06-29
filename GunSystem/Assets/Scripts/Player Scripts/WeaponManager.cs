using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public AllGuns allGuns;
    [SerializeField] private int typeIndex = 1;
    [SerializeField] private int gunIndex = 0;
    private BaseGunDefinition currentGun;
    [SerializeField]private EquippedGun equippedGun;

    private Coroutine x;

    public void Awake()
    {
        equippedGun = GetComponent<EquippedGun>();
        if (typeIndex == 1)
            currentGun = allGuns.primaryGuns[gunIndex];
        else
            currentGun = allGuns.secondaryGuns[gunIndex];

        equippedGun.UpdateGun(currentGun);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ChangeWeapon(true, 0f);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ChangeWeapon(false, 0f);

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown((KeyCode)i + 48))
            {
                ChangeWeapon(true, 1f);
            }
        }
    }

    private void ChangeWeapon(bool next, float n)
    {
        if (n == 1)
        {
            if (x != null)
            {
                StopCoroutine(x);
                equippedGun.doingAction = false;
            }
            float time = 1f;
            if (currentGun.weaponType == WeaponType.Primary)
            {
                time = 2f;
                if (allGuns.primaryGuns.Count > gunIndex)
                    gunIndex++;
                else
                    gunIndex = 0;

                currentGun = allGuns.primaryGuns[gunIndex];
                x = StartCoroutine(equippedGun.GunChange(currentGun, time));
            }
            else if (currentGun.weaponType == WeaponType.Secondary)
            {
                if (allGuns.secondaryGuns.Count > gunIndex)
                    gunIndex++;
                else
                    gunIndex = 0;

                currentGun = allGuns.secondaryGuns[gunIndex];
                x = StartCoroutine(equippedGun.GunChange(currentGun, time));
            }
            return;
        }

        if (x!=null)
        {
            StopCoroutine(x);
            equippedGun.doingAction = false;
        }
        if (next) typeIndex++;
        else typeIndex--;

        if (typeIndex > 2) typeIndex = 1;
        else if (typeIndex <= 0) typeIndex = 2;

        if (typeIndex == 1)
            currentGun = allGuns.primaryGuns[gunIndex];
        else
            currentGun = allGuns.secondaryGuns[gunIndex];

        var timer = typeIndex == 1 ? 2 : 1;
        x = StartCoroutine(equippedGun.GunChange(currentGun, timer)); 
    }
}
