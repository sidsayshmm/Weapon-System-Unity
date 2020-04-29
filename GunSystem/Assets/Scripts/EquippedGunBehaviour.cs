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
    public float timeLeft = 0f;
    private bool readyToFire = true;
    private bool usingADS = false;
    private bool usingBurst = false;

    void Start()
    {
        inventory = inventory.GetComponent<AllGunStatus>();
    }

    void Update()
    {
        CheckInput();
    }


    public void CheckInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (currentGun.isBurst)
                usingBurst = !usingBurst;
            else if (currentGun.isADS)
                usingADS = !usingADS;

        }


        if (currentGun.isSemi)
        {
            if (Input.GetMouseButtonDown(0) && readyToFire)
            {
                //Fire once
                if (usingADS)
                    FireADS(); 
                else
                    Fire();
                readyToFire = false;
            }

            if (Input.GetMouseButtonUp(0))
                readyToFire = true;
        }
        else if (currentGun.isAuto)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Firing from automatic!");
                Fire(); //Keep firing!
            }
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

    public void FireADS()
    {

    }

    public void DryFire()
    {
        //Do Something and the call reload
        Debug.Log("Ammo empty in " + currentGun.gunName);
        StartCoroutine("Reload");
        Invoke("Reload", currentGun.reloadTime);
        isReloading = true;
        
    }

    public void Reload()
    {
        if(isReloading)
        {
            isReloading = false;
            inventory.status[currentGun.name] = currentGun.maxAmmo;
        }
    }

    public void DrawCrosshair()
    {

    }
}
