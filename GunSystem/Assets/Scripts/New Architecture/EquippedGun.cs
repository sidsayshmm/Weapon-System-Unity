using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class EquippedGun : MonoBehaviour
{
  //  [SerializeField] protected AllGuns allGuns;
    [HideInInspector] public AllGunStatus inventory;
    [HideInInspector] public BaseGunDefinition currentGun; 
    [HideInInspector] public GunBehaviour gunBehaviour;
    #region
    [HideInInspector] public WeaponManager weaponManager;
    public float fireTimer = 0f;
    
    public float burstTimer;
    public ShootModes currentShootMode;
    public SightType currentSightMode;
    public bool usingADS;
    public int continouosFire;
    public float rateOfFire;

    public bool keyUp = true;
    public int burstCounter;

    [SerializeField] [ReadOnly] [Range(10, 250)] public float currentAcc;
    #endregion

    public float nextActionTime = 0.0f;
    public float period = 0.1f;

    public bool doingAction = false;// Actions like reloading, scoping etc

    protected virtual void Awake()
    {
        inventory = GetComponent<AllGunStatus>();
        weaponManager = GetComponent<WeaponManager>();
        gunBehaviour = GetComponentInChildren<GunBehaviour>();
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
            StartReload();
        if (Input.GetKey(KeyCode.Mouse2))
            CheckModeChange();
        CheckShoot();
    }

    protected virtual void StartReload()
    {

    }

    protected void CheckModeChange()
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
        else if (Input.GetMouseButtonDown(0))
        {
            if (!doingAction)
                CheckFire();
        }

    }

    public void CheckFire()
    {
        if (fireTimer >= rateOfFire && !doingAction)
            StartFire();
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
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
      //  Debug.Log($"Updated gun {currentGun}");
        currentAcc = currentGun.maxAccuracy;
        // Primary and secondary..
    }

    public void StartFire()
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
       // Debug.Log($"Inside Fire() function... {currentGun}");

    }
    public virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
    }
}
