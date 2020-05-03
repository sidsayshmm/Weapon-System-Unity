using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;


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

    private float rateOfFire = 0;
    private float fireTimer = 0;

    private bool ammoLeft = true;
    void Start()
    {
        inventory = inventory.GetComponent<AllGunStatus>();
    }
    
    private void Update()
    {
        if (inventory.status[currentGun.name] <= 0)
            ammoLeft = false;
        else
            ammoLeft = true;

        CheckInput();
    }

    private void FixedUpdate()
    {
        fireTimer += Time.deltaTime;
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
            if (Input.GetMouseButtonDown(0) && readyToFire && fireTimer >= rateOfFire) // Merge these conditions in Update later.
            {
                //Fire once
                if (usingADS && ammoLeft)
                    FireADS();
                else if (ammoLeft)
                    Fire();
                else
                    DryFire();
                readyToFire = false;
            }

            if (Input.GetMouseButtonUp(0))
                readyToFire = true;
        }

        else if (currentGun.isAuto)
        {
            if (Input.GetMouseButton(0) && fireTimer >= rateOfFire)
            {
                //keep firing
                if (usingADS && ammoLeft)
                    FireADS();
                else if (ammoLeft)
                    Fire();
                else
                    DryFire();
            }
        }
    }


    public void OnChange(GunDefinition newGunDef)
    {
        currentGun = newGunDef;
        usingADS = false;
        rateOfFire = 1 / currentGun.firingRate;
        fireTimer = rateOfFire;
        ammoLeft = true;
        Debug.Log("Gun name " + currentGun.gunName + ". Max ammo = " + currentGun.maxAmmo + ". FireTimer " + fireTimer);
    }

    public void Fire()
    {
        fireTimer = 0f;
        //Fire stuff here
        Debug.Log("Firing");
        inventory.status[currentGun.name]--;
    }

    public void FireADS()
    {
        fireTimer = 0f;
        //Firing from ADS here!
        Debug.Log("Firing");
        inventory.status[currentGun.name]--;
    }

    public void DryFire()
    {
        //Do Something and the call reload
        Invoke("Reload", currentGun.reloadTime);
        isReloading = true;   
    }

    public void Reload()
    {
        Debug.Log("Starting Reload");
        if(isReloading)
        {
            isReloading = false;
            inventory.status[currentGun.name] = currentGun.maxAmmo;
        }
        Debug.Log("Finished Reload");
    }

    public void DrawCrosshair()
    {

    }
}
