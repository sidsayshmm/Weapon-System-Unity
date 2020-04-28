using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshRenderer))]
public class EquippedGunBehaviour : MonoBehaviour
{
    public GunDefinition currentGun;
    public AllGunStatus inventory;

    public bool isReloading = false;
    void Start()
    {
        inventory = inventory.GetComponent<AllGunStatus>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (inventory.status[currentGun.name] <= 0)
                DryFire();
            else if (!isReloading) //check if it is still reloading
                Fire();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void OnChange(GunDefinition newGunDef)
    {
        currentGun = newGunDef;
    }

    public void Fire()
    {
        //Fire stuff here
        Debug.Log("Firing from " +currentGun.gunName);
        inventory.status[currentGun.name]--;
    }

    public void DryFire()
    {
        //Do Something and the call reload
        Debug.Log("Ammo empty in " + currentGun.gunName);
        StartCoroutine("Reload");
    }

    public IEnumerator Reload()
    {
        Debug.Log("Starting reload");
        isReloading = true;
        while(isReloading)
        {
            inventory.status[currentGun.name] = currentGun.maxAmmo;
            yield return currentGun.reloadTime;
        }
        isReloading = false;
    }

    public void DrawCrosshair()
    {

    }
}
