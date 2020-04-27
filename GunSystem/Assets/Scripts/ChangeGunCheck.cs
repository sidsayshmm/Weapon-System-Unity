using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunCheck : MonoBehaviour
{
    [SerializeField] public GunDefinition[] allGuns;
    public GunDefinition currentGun;
    public int currentGunIndex = 1;
    public EquippedGunBehaviour egb;

    public void Start()
    {
        currentGun = allGuns[currentGunIndex];
        egb = GetComponent<EquippedGunBehaviour>();
        egb.OnChange(allGuns[currentGunIndex]);
    }

    public void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            NextWeapon();
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            PreviousWeapon();
    }

    public void UpdateGun(int updatedGunIndex)
    {
        for (int i = 0; i < allGuns.Length; i++)
        {
            allGuns[i].gunModel.SetActive(false);
        }
        allGuns[updatedGunIndex].gunModel.SetActive(true);

        egb.OnChange(allGuns[updatedGunIndex]);
    }

    public void NextWeapon()
    {
        currentGunIndex++;
        if (currentGunIndex > allGuns.Length)
            currentGunIndex = 0;

        UpdateGun(currentGunIndex);

    }

    public void PreviousWeapon()
    {
        currentGunIndex--;
        if (currentGunIndex <= 0)
            currentGunIndex = allGuns.Length;

        UpdateGun(currentGunIndex);
    }
}
