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

    private float rateOfFire = 0;
    private float fireTimer = 0;
    void Start()
    {
        inventory = inventory.GetComponent<AllGunStatus>();
    }
    
    private void Update()
    {
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
                if (usingADS)
                    FireADS(); 
                else
                    Fire();
                readyToFire = false;
            }

            if (Input.GetMouseButtonUp(0))
                readyToFire = true;
        }

        else if (currentGun.isAuto && fireTimer >= rateOfFire)
        {
            if (Input.GetMouseButton(0))
            {
                //keep firing
                if (usingADS)
                    FireADS();   
                else
                    Fire();
            }
        }
    }


    public void OnChange(GunDefinition newGunDef)
    {
        currentGun = newGunDef;
        usingADS = false;
        rateOfFire = 1 / currentGun.firingRate;
        fireTimer = rateOfFire;
        Debug.Log("CurrentGunFiringRate " + currentGun.firingRate);
        Debug.Log("Calculate rateOfFire" + rateOfFire);
        Debug.Log("Current gun name " + currentGun.gunName + ". Max ammo = " + currentGun.maxAmmo + ". FireTimer " + fireTimer);
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
