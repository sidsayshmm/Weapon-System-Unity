using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : EquippedGun
{
    Vector2 centerPoint;
    [SerializeField] Camera fpsCamera;
    private void Start()
    {
        centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        
    }

    private void ADSFire()
    {
        Fire();
        if(currentShootMode == ShootModes.Burst)
        {
            burstCounter++;
        }
        fireTimer = 0f;
        currentAcc -= currentGun.accuracyDropPerShot;   // Change it acc to ADS drop per shot
        if (currentAcc <= 0)
            currentAcc = 10;

        inventory.status[currentGun.name]--;

        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    private void HipFire()
    {
        Fire();
        if (currentShootMode == ShootModes.Burst)
        {
            burstCounter++;
        }
        fireTimer = 0f;
        currentAcc -= currentGun.accuracyDropPerShot;   // Change it acc to hipgfire drop per shot
        if (currentAcc <= 0)
            currentAcc = 10;

        inventory.status[currentGun.name]--;

        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    private void DrawCrosshair()
    {

    }

    private void ShootStuff(Vector2 screenPointToFire)
    {
        Ray ray = fpsCamera.ScreenPointToRay(screenPointToFire);
        Vector3 goalPoint = Vector3.zero;
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            goalPoint = hit.point;
        }
        else
        {
            // Get a point somehow... god knows how.
        }

        Vector3 startPoint = fpsCamera.transform.position;   // Figure out a way to set startPoint as gun muzzle
        Vector3 bulletDir = (goalPoint - startPoint).normalized;

        RaycastHit[] thiccCast = Physics.SphereCastAll(startPoint, 1f, bulletDir, 1000f, 0);  //layer mask is 0 .. change later or remove it
        
        foreach(RaycastHit hitP in thiccCast)
        {
            var decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            decal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            decal.transform.position = hitP.point;
        }
    }
}
