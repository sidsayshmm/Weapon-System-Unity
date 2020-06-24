using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    Vector2 centerPoint;
    private Camera fpsCamera;
    private EquippedGun equippedGun;
    private void Awake()
    {
        //base.Awake();
        fpsCamera = Camera.main;
        equippedGun = GetComponentInParent<EquippedGun>();
        centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        //Do I need this ? 
    }

    private void FixedUpdate()
    {
        
    }

    public void ADSFire()
    {
      //  Debug.Log($"YOO");
        equippedGun.Fire();
        if(equippedGun.currentShootMode == ShootModes.Burst)
        {
            equippedGun.burstCounter++;
        }
        equippedGun.fireTimer = 0f;
        equippedGun.currentAcc -= equippedGun.currentGun.accuracyDropPerShot;   // Change it acc to ADS drop per shot
        if (equippedGun.currentAcc <= 0)
            equippedGun.currentAcc = 10;

        equippedGun.inventory.status[equippedGun.currentGun.name]--;
    //    Debug.Log($"FIRING Inside ADS() function... {equippedGun.currentGun}");
        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    public void HipFire()
    {
        Debug.Log($"YOO");
        equippedGun.Fire();
       // 
        if (equippedGun.currentShootMode == ShootModes.Burst)
        {
            equippedGun.burstCounter++;
        }
        equippedGun.fireTimer = 0f;
        equippedGun.currentAcc -= equippedGun.currentGun.accuracyDropPerShot;   // Change it acc to hipgfire drop per shot
        if (equippedGun.currentAcc <= 0)
            equippedGun.currentAcc = 10;

        equippedGun.inventory.status[equippedGun.currentGun.name]--;
        Debug.Log($"FIRING Inside Hipfire() function... {equippedGun.currentGun}");
        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    public void DrawCrosshair()
    {

    }

    private void ShootStuff(Vector2 screenPointToFire)
    {
        Debug.Log("Shoot stuff called");
        Ray ray = fpsCamera.ScreenPointToRay(screenPointToFire);
        Vector3 goalPoint = Vector3.zero;
       // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100f);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        if (hits.Length != 0)
        {
            goalPoint = hits[hits.Length-1].point;
           // Debug.Log("Found a target");
        }
        else
        {
            // Get a point somehow... god knows how.
        }
        Debug.Log($"First ray length = {hits.Length}");
        foreach(RaycastHit x in hits)
        {
            Debug.Log("POINT FIRST RAY HIT "+ x.point);
        }

        Vector3 startPoint = fpsCamera.transform.position;   // Figure out a way to set startPoint as gun muzzle
        Vector3 bulletDir = (goalPoint - startPoint).normalized;

        int notPlayerLayer = ~(1 << LayerMask.NameToLayer("Player"));

        RaycastHit[] thiccCast = Physics.SphereCastAll(startPoint, 0.1f, bulletDir, 100f, notPlayerLayer);
        
        Debug.DrawRay(startPoint, bulletDir * 1000, Color.green, 100f);
        Debug.Log($"THICC CASST LENGTH {thiccCast.Length}");
        int i = 0;
        foreach(RaycastHit hitP in thiccCast)
        {
        //    Debug.Log("Decal");
            var decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            decal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log(" decal number = "+ i +" pos = " +  hitP.point + hitP.collider.gameObject.name);
            i++;
            decal.transform.position = hitP.point;
            decal.gameObject.name = "UNO DOS TRESS ???????";
        }
    }

    //  RaycastHit[] thiccCast = Physics.SphereCastAll(startPoint, 10f, bulletDir, 1000f, 0);  //layer mask is 0 .. change later or remove it

}
