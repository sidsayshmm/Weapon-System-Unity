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
    public GunBehaviour gunBehaviour;
    #region
    [HideInInspector] public WeaponManager weaponManager;
    protected float fireTimer = 0f;
    
    private float burstTimer;
    protected ShootModes currentShootMode;
    private bool usingADS;
    private int continouosFire;
    private float rateOfFire;
    private bool doingAction = false;  // Actions like reloading, scoping etc
    private bool keyUp = true;
    protected int burstCounter;

    [SerializeField] [Range(10, 250)] public float currentAcc;
    #endregion

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    protected virtual void Awake()
    {
        inventory = GetComponent<AllGunStatus>();
        weaponManager = GetComponent<WeaponManager>();
        if (weaponManager != null)
            Debug.Log("Calling object name =  " + gameObject.name + "Null check = " + weaponManager);
        else
            Debug.Log("Calling object name =  " + gameObject.name + "Null check = " + "Null");
        
        
        // New inventory.. ??
    }


    //weaponManager.Start();
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
        Debug.Log($"LOGGING MAX ACC {currentGun.maxAccuracy} AND 2nd PARAM = {this}", gameObject);
            
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
        //if(currentGun.)

        //currentGun.Fire(0,1);

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

    protected void Fire()
    {

    }

    protected virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
    }
}
