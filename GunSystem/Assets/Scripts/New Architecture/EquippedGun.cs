using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquippedGun : MonoBehaviour
{
    [SerializeField] protected AllGuns allGuns;
    public AllGunStatus inventory;
    private BaseGunDefinition currentGun;

    private float fireTimer = 0;
    private float burstTimer;
    private DefaultShootMode currentShootMode;
    private bool usingADS;
    private int continouosFire;
    private float rateOfFire;
    private bool doingAction = false;  // Actions like reloading, scoping etc
    private bool keyUp = true;
    private int burstCounter;
    

    void Start()
    {
        inventory = inventory.GetComponent<AllGunStatus>();
        // New inventory.. ??
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        fireTimer += Time.fixedDeltaTime;
        if(currentShootMode == DefaultShootMode.Burst)
        {
            burstTimer += Time.fixedDeltaTime;
           // if(burstTimer >= currentGun.burstPause)
            {
                burstCounter = 1;
                burstTimer = 0.0f;
            }
        }
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartReload();
        if (Input.GetKey(KeyCode.Mouse2))
            CheckModeChange();
        CheckShoot();
    }

    void StartReload()
    {

    }

    void CheckModeChange()
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
    }

    public void UpdateGun(BaseGunDefinition newGun)
    {
        if(newGun.weaponType == WeaponType.Primary)
        {
            if(newGun.sightType == SightType.ADS)
            {
                if(newGun.shootModes == AvailableShootModes.Burst)
                {
                    var currentGun = newGun as PrimaryADSBurstGuns;
                }
            }
        }
        usingADS = false;
        continouosFire = 0;
        rateOfFire = 1.0f / currentGun.firingRate;
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
    }
}
