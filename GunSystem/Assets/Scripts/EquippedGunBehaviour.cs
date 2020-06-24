using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshRenderer))]
public class EquippedGunBehaviour : MonoBehaviour
{
    [Header("I know you know what this means")]
    public GunDefinition currentGun;
    public AllGunStatus inventory;
    public CurrentCircle currentCircle;
    public CameraRecoil camRecoil;
    Vector3 startPoint;

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

    public int continousFire=0;


    private float nextActionTime = 0.0f;
    public float period = 0.1f;



    [SerializeField] [Range(10, 250)] public float currentAcc;
    void Start()
    {
        inventory = inventory.GetComponent<AllGunStatus>();
        currentCircle = GetComponent<CurrentCircle>();

     //   Debug.Log(Mathf.Tan(Mathf.Deg2Rad * 45));
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
                burstCounter = 1;
                burstTimer = 0.0f;
            }
        }

        if(currentAcc < currentGun.maxAccuracy)
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
            {
                usingBurst = !usingBurst;
                Debug.Log(usingBurst);
            }
        }
    }

    public void CheckShoot()
    {
        //Should not be reloading, not be scoping in, should have ammo, should be greater than burst pause..should be greater than fire rate 
        // andd...readytofire(in case of smg)

        if (currentGun.gunType == GunType.Full)
        {
            if (Input.GetMouseButtonUp(0))
            {
               // Debug.Log("Mouse up");
                continousFire = 0;
            }

            if (Input.GetMouseButton(0) && fireTimer >= rateOfFire && !isReloading && !isScoping)
            {
                readyToFire = true;
                if (!ammoLeft)
                    DryFire();
                else if (burstCounter <= currentGun.burstRate)
                {
                    continousFire++;
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
            { readyToFire = true;  Debug.Log("MouseUp"); }    

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
        continousFire = 0;
        currentAcc = currentGun.maxAccuracy;
        if (currentGun.burstOnly)
            usingBurst = true;
        else
            usingBurst = false;

        rateOfFire = 1 / currentGun.firingRate;
        fireTimer = rateOfFire;
        ammoLeft = true;
       // Debug.Log("Gun name " + currentGun.gunName + ". Max ammo = " + currentGun.maxAmmo + ". FireTimer " + fireTimer);
    }

    public void Fire()
    {
        fireTimer = 0f;
        if (usingBurst)
            burstCounter++;

        // currentCircle.SelectPoint();
        // Make a raycast from the received point
        // if null fuck xD

        camRecoil.Recoil();

        Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        //  Vector2 selectedScreenPoint = centerPoint + Random.insideUnitCircle * radius;
        Vector2 selectedScreenPoint = centerPoint;
        Debug.Log($"Selected screen point {selectedScreenPoint}");
        ShootStuff(selectedScreenPoint);

        currentAcc -= currentGun.accuracyDropPerShot;
        if (currentAcc <= 0)
            currentAcc = 10;

       // Vector2 selectedPoint = new Vector2(currentCircle.pointD.x,currentCircle.pointD.y);
       // ShootStuff(selectedPoint);

        inventory.status[currentGun.name]--;
    }

    public void FireADS()
    {
        
        
        fireTimer = 0f;
        if (usingBurst)
            burstCounter++;
        //camRecoil.Recoil();
  
        int radius = 1;
        Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        //  Vector2 selectedScreenPoint = centerPoint + Random.insideUnitCircle * radius;
        Vector2 selectedScreenPoint = centerPoint;
        Debug.Log($"Selected screen point {selectedScreenPoint}");
        ShootStuff(selectedScreenPoint);

        currentAcc -= currentGun.accuracyDropPerShot;
        if (currentAcc <= 0)
            currentAcc = 10;

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
    
    public void ShootStuff(Vector2 pos)
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction*100, Color.red, 100f);
        

        RaycastHit[] hits = Physics.RaycastAll(ray, 1000f);

        foreach(RaycastHit hitp in hits)
        {
            //Debug.Log(hitp);
            //Get the appropriate hitpont... in hitPoint;
            
        }

        Vector3 hitPoint = hits[0].point;
        Decals(hits);
        ActualRay(hitPoint);
    }
    public void Decals(RaycastHit[] hits)
    {
        GameObject x = GameObject.CreatePrimitive(PrimitiveType.Sphere);
     //   Instantiate(x, hits[0].point, Quaternion.FromToRotation(Vector3.up,hits[0].normal));
    
        x.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        x.gameObject.name = "F";
        x.transform.position = hits[0].point;

    }
    public void ActualRay(Vector3 endPoint)
    {
        //Gun Muzzle
        //Ray ray;
        startPoint = Camera.main.transform.position;
        Vector3 direction = (endPoint - startPoint).normalized;


       // RaycastHit[] hits = Physics.RaycastAll(startPoint, direction, 100000f, 0); //LayerMaskk???!?!??

        RaycastHit[] ThiccCast = Physics.SphereCastAll(startPoint, 10f, direction, 100000f, 0); // LayerMask fix later?!

    //    Debug.DrawRay(Camera.main.transform.position, direction * 100, Color.green, 100f);

        foreach (RaycastHit hitp in ThiccCast) // Or  (RaycastHit hitp in hits)
        {
            Debug.Log(hitp);
            //Do damage according to path.
            //break if reach a non - penetrable object.
        }
    }
}
