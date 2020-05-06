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
    public bool isScoping = false;


    private bool readyToFire = true;
    private bool usingADS = false;
    private bool usingBurst = false;

    private float rateOfFire = 0;
    private float fireTimer = 0;


    private float burstTimer;
    private int burstCounter;

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

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartReload();
        CheckModeChange();
        CheckShoot();
    }

    private void FixedUpdate()
    {
        fireTimer += Time.fixedDeltaTime;


        if(usingBurst)
        {
            burstTimer += Time.fixedDeltaTime;
            if (burstTimer >= currentGun.burstPause)
            {
                burstCounter = 0;
                burstTimer = 0.0f;
            }
        }
    }

    public void CheckModeChange()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (currentGun.shootType == ShootType.ADS)
            {
                usingADS = !usingADS;
                isScoping = true;
                Invoke("ModifyScope", currentGun.scopeInTime);
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            if (currentGun.burstOnly)
                return;
            else if (currentGun.burstType == BurstType.isBurst)
                usingBurst = !usingBurst;
        }
    }

    public void CheckShoot()
    {
        //Should not be reloading, not be scoping in, should have ammo, should be greater than burst pause..should be greater than fire rate 
        // andd...readytofire(in case of smg)

        if (currentGun.gunType == GunType.Full)
        {
            if(Input.GetMouseButton(0) && fireTimer >= rateOfFire && !isReloading && !isScoping)
            {
                readyToFire = false;

                if (!ammoLeft)
                    DryFire();
                else if (burstCounter <= currentGun.burstRate)
                {
                    if (usingADS)
                        FireADS();
                    else
                        Fire();
                }
            }
        }

        else if(currentGun.gunType == GunType.Semi)
        {
            if (Input.GetMouseButtonUp(0))
                readyToFire = true;

            if (Input.GetMouseButton(0) && fireTimer >= rateOfFire && readyToFire && !isReloading && !isScoping)
            {
                if (!ammoLeft)
                    DryFire();
                else if (burstCounter <= currentGun.burstRate)
                {
                    if (usingADS)
                        FireADS();
                    else
                        Fire();
                }
            }
        }
    }

    public void OnChange(GunDefinition newGunDef)
    {
        currentGun = newGunDef;
        usingADS = false;

        if (currentGun.burstOnly)
            usingBurst = true;
        else
            usingBurst = false;

        rateOfFire = 1 / currentGun.firingRate;
        fireTimer = rateOfFire;
        ammoLeft = true;
        Debug.Log("Gun name " + currentGun.gunName + ". Max ammo = " + currentGun.maxAmmo + ". FireTimer " + fireTimer);
    }

    public void Fire()
    {
        fireTimer = 0f;
        if (usingBurst)
            burstCounter++;

        //Fire stuff here
        Debug.Log("Firing");
        inventory.status[currentGun.name]--;
    }

    public void FireADS()
    {
        fireTimer = 0f;
        if (usingBurst)
            burstCounter++;

        //Firing from ADS here!
        Debug.Log("Firing");
        inventory.status[currentGun.name]--;
    }

    public void DryFire()
    {
        //Do Something and the call reload
        StartReload();
    }

    public void ModifyScope()
    {
        isScoping = false;
    }
    public void StartReload()
    {
        if (inventory.status[currentGun.name] == currentGun.maxAmmo)
            return;

        isReloading = true;
        Invoke("Reload", currentGun.reloadTime);
    }
    
    public void Reload()
    {
        Debug.Log("Starting Reload");
        if (isReloading)
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
