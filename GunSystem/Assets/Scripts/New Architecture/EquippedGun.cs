using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquippedGun : MonoBehaviour
{
    [SerializeField] protected AllGuns allGuns;
    [SerializeField] public AllGunStatus inventory;
    public BaseGunDefinition currentGun;

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
    

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    void Start()
    {
       // inventory = inventory.GetComponent<AllGunStatus>();
        // New inventory.. ??
    }

    private void FixedUpdate()
    {
        fireTimer += Time.fixedDeltaTime;
        if (currentShootMode == ShootModes.Burst)
        {
            burstTimer += Time.fixedDeltaTime;
            // if(burstTimer >= currentGun.burstPause)
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
        //if(currentGun.)

        //currentGun.Fire(0,1);

    }

    public void UpdateGun(BaseGunDefinition newGun)
    {
        currentGun = newGun;
        usingADS = false;
        continouosFire = 0;
        rateOfFire = 1.0f / currentGun.firingRate;
        doingAction = false;
        currentShootMode = currentGun.defShootMode;
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
