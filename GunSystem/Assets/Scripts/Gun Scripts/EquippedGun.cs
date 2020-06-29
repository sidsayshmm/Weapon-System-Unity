using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
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
    public int continuousFire = 0;
    public float rateOfFire;
    public bool keyUp = true;
    public int burstCounter;
    public float nextActionTime = 0.0f;
    public float period = 0.1f;

    [ReadOnly] public bool doingAction = false;
    private bool isReloading = false;
    private bool isScoping = false;

    public float actionTime = 0f;
    public float sliderMax = 1f;

    private float startScopeTime;
    #endregion

    private Coroutine reloadCoroutine;
    private Coroutine scopeCoroutine;
    [ReadOnly] [Range(10, 250)] public float currentAcc;


    #region Properties
    
    public float CurrentAccuracy
    {
        get { return currentAcc; }
    }

    #endregion

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
        {
            if(!isReloading)
                reloadCoroutine = StartCoroutine(Reload());
        }
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
        if(currentGun.sightType == SightType.ADS)
        {
            if(Input.GetMouseButton(1) && !doingAction)
            {
                Debug.Log("Starting scope");
                startScopeTime = Time.time;
                scopeCoroutine = StartCoroutine(Scoping(currentGun.scopeInTime));
            }
            else if(Input.GetMouseButtonDown(1) && isScoping)
            {
                float scopeOutTime = Time.time - startScopeTime;
                if (scopeCoroutine != null)
                    StopCoroutine(scopeCoroutine);
                scopeCoroutine = StartCoroutine(Scoping(scopeOutTime));
            }
        }
    }

    void CheckShoot()
    {
        if(Input.GetMouseButtonUp(0))
        {
            keyUp = true;
            continuousFire = 0;
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
        continuousFire = 0;
        rateOfFire = 1.0f / currentGun.firingRate;
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
        currentSightMode = SightType.Normal;
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
        continuousFire++;
        SetSlider(rateOfFire);
        actionTime = rateOfFire;
        inventory.status[currentGun.name]--;
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
            StopCoroutine(reloadCoroutine);

        if(isScoping)
            StopCoroutine(scopeCoroutine);

        isReloading = false;
        actionTime = 0;

        doingAction = true;
        actionTime = timer;
        SetSlider(timer);

        yield return new WaitForSeconds(timer);
        UpdateGun(newGunDef);
        doingAction = false;
    }
    public IEnumerator Scoping(float timer)
    {
        Debug.Log($"Starting scoping at {Time.time}");
        Debug.Log(timer);
        doingAction = true;
        isScoping = true;
        actionTime = timer;
        SetSlider(timer);
        yield return new WaitForSeconds(timer);

        doingAction = false;
        if (currentSightMode == SightType.ADS)
            currentSightMode = SightType.Normal;
        else
            currentSightMode = SightType.ADS;

        isScoping = false;
        doingAction = false;
        Debug.Log($"Current shoot mode = { currentSightMode} at {Time.time} ");
    }


    private void SetSlider(float maxVal)
    {
        sliderMax = maxVal;
    }
}
