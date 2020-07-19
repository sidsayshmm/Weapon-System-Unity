using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    public bool usingAds;
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

    private Vector2 centerPoint;
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

    private void Start()
    {
       centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
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
        if(currentGun.sightType == SightType.Ads)
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
                {
                    StopCoroutine(scopeCoroutine);
                    Debug.Log("Reversing SIGHT MODE WITHOUT COMPLETION");
                }
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

    private void CheckFire()
    {
        if (fireTimer >= rateOfFire)
            ChooseFireMode();
    }

    public void UpdateGun(BaseGunDefinition newGun)
    {
        if(newGun == null)
        {
            throw new Exception("Gun is null. Good luck you fucking idiot");
        }
        currentGun = newGun;
        usingAds = false;
        continuousFire = 0;
        rateOfFire = 1.0f / currentGun.firingRate;
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
        currentSightMode = SightType.Normal;
        currentAcc = currentGun.maxAccuracy;
    }

    private void ChooseFireMode()
    {
        keyUp = false;
        if (currentSightMode == SightType.Ads)
        {
            gunBehaviour.AdsFire();
            ShootData shootData = new ShootData(this.gameObject,this,true, centerPoint);
            currentGun.AdsFire(shootData);
        }
        else
        {
            gunBehaviour.HipFire();
            ShootData shootData = new ShootData(this.gameObject,this,true, centerPoint);
            currentGun.AdsFire(shootData);
        }
    }

    public void Fire()
    {
        continuousFire++;
        SetSlider(rateOfFire);
        actionTime = rateOfFire;
        inventory.status[currentGun.name]--;
    }

    private IEnumerator Reload()
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
    private IEnumerator Scoping(float timer)
    {
        if (currentSightMode == SightType.Ads)
            currentSightMode = SightType.Normal;
        else
            currentSightMode = SightType.Ads;
        doingAction = true;
        isScoping = true;
        actionTime = timer;
        SetSlider(timer);
        yield return new WaitForSeconds(timer);

        doingAction = false;
       

        isScoping = false;
        doingAction = false;
        Debug.Log($"Current shoot mode = { currentSightMode} at {Time.time} ");
    }


    private void SetSlider(float maxVal)
    {
        sliderMax = maxVal;
    }
}
