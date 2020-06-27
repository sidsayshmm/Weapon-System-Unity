using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EquippedGun : MonoBehaviour
{

    [HideInInspector] public AllGunStatus inventory;
    [NonSerialized] public BaseGunDefinition currentGun; 
    [HideInInspector] public GunBehaviour gunBehaviour;
    [HideInInspector] public WeaponManager weaponManager;

    #region
    public ShootModes currentShootMode;
    public SightType currentSightMode;

    [NonSerialized] public float fireTimer = 0f;
    public float burstTimer;
    public bool usingADS;
    public int continouosFire;
    public float rateOfFire;
    public bool keyUp = true;
    public int burstCounter;
    public float nextActionTime = 0.0f;
    public float period = 0.1f;

    [ReadOnly] public bool doingAction = false;
    private bool isReloading = false;

    public float actionTime = 0f;
    public float sliderMax = 1f;
    #endregion

    private Coroutine reloadCoroutine;
    [ReadOnly] [Range(10, 250)] public float currentAcc;

    private void Awake()
    {
        inventory = GetComponent<AllGunStatus>();
        weaponManager = GetComponent<WeaponManager>();
        gunBehaviour = GetComponentInChildren<GunBehaviour>();
    }

    private void FixedUpdate()
    {
        fireTimer += Time.fixedDeltaTime;
        if (currentShootMode == ShootModes.Burst)
        {
            burstTimer += Time.fixedDeltaTime;
            if(burstTimer >= currentGun.burstPause)
            {
                burstCounter = 1;
                burstTimer = 0.0f;
            }
        }            
        if (currentAcc < currentGun.maxAccuracy)
        {
            if (Time.fixedTime > nextActionTime)
            {
                nextActionTime += period;
                currentAcc += currentGun.accuracyGainPerSec;

                if (currentAcc > currentGun.maxAccuracy) 
                    currentAcc = currentGun.maxAccuracy;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            reloadCoroutine = StartCoroutine(Reload());
        if (Input.GetKey(KeyCode.Mouse2))
            CheckModeChange();
        CheckShoot();

        if(inventory.status[currentGun.name]<=0 && !doingAction)
        {
            DryFire();
        }

        if(actionTime >= 0)
        {
            actionTime -= Time.deltaTime;
            if (actionTime < 0)
                actionTime = 0;
        }
    }

    private void DryFire()
    {
        //Do reload stuff.
        reloadCoroutine = StartCoroutine(Reload());
    }


    private void CheckModeChange()
    {
        if(currentGun.modeChanges)
        {

        }
    }

    void CheckShoot()
    {
        if(Input.GetMouseButtonUp(0))
        {
            keyUp = true;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!doingAction)
                CheckFire();
        }
    }

    public void CheckFire()
    {
        if (fireTimer >= rateOfFire)
            ChooseFireMode();
    }

    public void UpdateGun(BaseGunDefinition newGun)
    {
        if(newGun == null)
        {
            Debug.Log("NEW GUN IS NULL YOU COMPLETE IDIOT");
        }
        currentGun = newGun;
        usingADS = false;
        continouosFire = 0;
        rateOfFire = 1.0f / currentGun.firingRate;
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
        currentAcc = currentGun.maxAccuracy;
    }

    public void ChooseFireMode()
    {
        keyUp = false;
        if (currentSightMode == SightType.ADS)
            gunBehaviour.ADSFire();
        else
            gunBehaviour.HipFire();
    }

    public void Fire()
    {
        SetSlider(rateOfFire);
        actionTime = rateOfFire;
    }

    public IEnumerator Reload()
    {
        SetSlider(currentGun.reloadTime);

        actionTime = currentGun.reloadTime;
        isReloading = true;
        doingAction = true;

        yield return new WaitForSeconds(currentGun.reloadTime);

        isReloading = false;
        doingAction = false;
        inventory.status[currentGun.name] = currentGun.clipSize;
    }

    public IEnumerator GunChange(BaseGunDefinition newGunDef, float timer)
    {
        if(isReloading)
        {
            StopCoroutine(reloadCoroutine);
            doingAction = false;
            isReloading = false;
            actionTime = 0;
        }
        doingAction = true;
        actionTime = timer;
        yield return new WaitForSeconds(timer);
        UpdateGun(newGunDef);
        doingAction = false;
    }

    private void SetSlider(float maxVal)
    {
        sliderMax = maxVal;
    }
}
