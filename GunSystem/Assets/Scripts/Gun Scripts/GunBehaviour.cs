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
        //This.. Do I need this too?
    }

    public void ADSFire()
    {
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
        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    public void HipFire()
    {
        equippedGun.Fire();

        if (equippedGun.currentShootMode == ShootModes.Burst)
        {
            equippedGun.burstCounter++;
        }

        equippedGun.fireTimer = 0f;
        equippedGun.currentAcc -= equippedGun.currentGun.accuracyDropPerShot;   // Change it acc to hipgfire drop per shot
        if (equippedGun.currentAcc <= 0)
            equippedGun.currentAcc = 10;

        equippedGun.inventory.status[equippedGun.currentGun.name]--;
        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    public void DrawCrosshair()
    {

    }

    private void ShootStuff(Vector2 screenPointToFire)
    {
        Ray ray = fpsCamera.ScreenPointToRay(screenPointToFire);
        Vector3 goalPoint = Vector3.zero;

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        if (hits.Length != 0)
        {
            goalPoint = hits[hits.Length-1].point;
        }
        else
        {
            // Get a point somehow... god knows how.
        }

        int notPlayerLayer = ~(1 << LayerMask.NameToLayer("Player"));
        Vector3 startPoint = fpsCamera.transform.position;   // Figure out a way to set startPoint as gun muzzle
        Vector3 bulletDir = (goalPoint - startPoint).normalized;

        RaycastHit[] thiccCast = Physics.SphereCastAll(startPoint, 0.1f, bulletDir, 100f, notPlayerLayer);
        
        Debug.DrawRay(startPoint, bulletDir * 1000, Color.green, 100f);

        foreach(RaycastHit hitP in thiccCast)
        {
            var decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            decal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            decal.transform.position = hitP.point;
            decal.gameObject.layer = 8;

        }
    }
}
