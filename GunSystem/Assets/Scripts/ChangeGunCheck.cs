using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunCheck : MonoBehaviour
{
    [SerializeField] public GunDefinition[] allGuns;
    public GunDefinition currentGun;
    public int currentGunIndex = 1;
    public EquippedGunBehaviour egb;

    [SerializeField] public GameObject equippedGunObject;

    public void Start()
    {
        currentGun = allGuns[currentGunIndex];
        egb = egb.GetComponent<EquippedGunBehaviour>();
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
        Debug.Log("Updating gun");
        if(egb.isReloading)
        {
            Debug.Log("Cancelled Reload");
            egb.isReloading = false;
        }

        egb.OnChange(allGuns[updatedGunIndex]);
        if (equippedGunObject.GetComponent<MeshFilter>() == null)
        {
            equippedGunObject.AddComponent<MeshFilter>();
            Debug.Log("Adding shit!");
        }
        equippedGunObject.GetComponent<MeshFilter>().sharedMesh = allGuns[updatedGunIndex].model;
    }

    public void NextWeapon()
    {
        currentGunIndex++;
        if (currentGunIndex >= allGuns.Length)
            currentGunIndex = 0;

        UpdateGun(currentGunIndex);
    }

    public void PreviousWeapon()
    {
        currentGunIndex--;
        if (currentGunIndex < 0)
            currentGunIndex = allGuns.Length-1;

        UpdateGun(currentGunIndex);
    }
}
