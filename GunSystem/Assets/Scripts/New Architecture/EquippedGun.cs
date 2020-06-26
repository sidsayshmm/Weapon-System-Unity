using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EquippedGun : MonoBehaviour
{
  //  [SerializeField] protected AllGuns allGuns;
    [HideInInspector] public AllGunStatus inventory;
    [HideInInspector] public BaseGunDefinition currentGun; 
    [HideInInspector] public GunBehaviour gunBehaviour;
    [HideInInspector] public WeaponManager weaponManager;

    #region
    [NonSerialized] public float fireTimer = 0f;
    public float burstTimer;
    public ShootModes currentShootMode;
    public SightType currentSightMode;
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

    public LayerMask mask;

    [ReadOnly] [Range(10, 250)] public float currentAcc;
   

    // Actions possible are Reloading, Scoping in, Scoping out, Attachment , Changing

    // Priority order...
    //  Gun Change / Drop  then    Reloading       then     
    //

    private void Awake()
    {
        inventory = GetComponent<AllGunStatus>();
        weaponManager = GetComponent<WeaponManager>();
        gunBehaviour = GetComponentInChildren<GunBehaviour>();
        Debug.Log(rateOfFire);      
        //weaponManager.Start();
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
            //add accgainpersec.
            if (Time.fixedTime > nextActionTime)
            {
                //Debug.Log("Adding! " + Time.fixedTime + " Next Action Time = " + nextActionTime);
                nextActionTime += period;
                currentAcc += currentGun.accuracyGainPerSec;

                if (currentAcc > currentGun.maxAccuracy) currentAcc = currentGun.maxAccuracy;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());
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

        Debug.Log(actionTime);
    }

    private void DryFire()
    {
        //Do Dry Fire Stuff and call Reload
        StartCoroutine(Reload());
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
       // Debug.Log($"Updating Gun to {newGun} and current gun is {currentGun}");
        usingADS = false;
        continouosFire = 0;
        rateOfFire = 1.0f / currentGun.firingRate;
        Debug.Log($"New Firing rate = {rateOfFire}");
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
      //  Debug.Log($"Updated gun {currentGun}");
        currentAcc = currentGun.maxAccuracy;
        // Primary and secondary..
    }

    public void ChooseFireMode()
    {
     //   Debug.Log($"Just before calling fire functions... {currentGun}");
        keyUp = false;
        if (currentSightMode == SightType.ADS)
            gunBehaviour.ADSFire();
        else
            gunBehaviour.HipFire();//Call derived hipfire
    }

    public void Fire()
    {
        SetSlider(rateOfFire);
        actionTime = rateOfFire;
        Debug.Log($"New timer value = {actionTime}");
        Debug.Log($"Inside Fire() function... {currentGun}");

    }
    public IEnumerator Reload()
    {
        Debug.Log("Starting reload");
        SetSlider(currentGun.reloadTime);
        actionTime = currentGun.reloadTime;
        isReloading = true;
        doingAction = true;
        yield return new WaitForSeconds(currentGun.reloadTime);
        isReloading = false;
        doingAction = false;
        inventory.status[currentGun.name] = currentGun.clipSize;
        Debug.Log("Ending reload");
    }

    public IEnumerator GunChange(BaseGunDefinition newGunDef, float timer)
    {
        doingAction = true;        
        yield return new WaitForSeconds(timer);
        UpdateGun(newGunDef);
        doingAction = false;
    }

    private void SetSlider(float maxVal)
    {
        sliderMax = maxVal;
    }

}
