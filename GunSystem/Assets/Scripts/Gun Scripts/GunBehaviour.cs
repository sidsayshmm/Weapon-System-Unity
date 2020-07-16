using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    Vector2 centerPoint;
    private Camera fpsCamera;
    private EquippedGun equippedGun;


    private float force = 10f;// controls recoil amplitude 
    private float upSpeed = 9f; // controls smoothing speed var dnSpeed: 
    private float dnSpeed = 6f; //how fast the weapon returns to original position
    private Vector3 initialAngle; // initial angle = 
    private float targetX; // unfiltered recoil angle 
    private float targetY;
    private Vector3 newAngle = Vector3.zero; // smoothed angle
    private Ray ray;


    [SerializeField] bool usingRecoil = true;

    private void Awake()
    {
        fpsCamera = Camera.main;
        equippedGun = GetComponentInParent<EquippedGun>();
        centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        initialAngle = fpsCamera.transform.localEulerAngles;
    }

    private void Update()
    {

        newAngle.x = Mathf.Lerp(newAngle.x, targetX, upSpeed * Time.deltaTime);   // Calculate the vertical rotation to wanna push up
        newAngle.y = Mathf.Lerp(newAngle.y, targetY, upSpeed * Time.deltaTime); // Calculate the horizontal rotation you wanna push up
                                                                                //To do. Make this randomized to left||right
        fpsCamera.transform.localEulerAngles = initialAngle - newAngle; // move the camera upwards.

        targetX = Mathf.Lerp(targetX, 0, dnSpeed * Time.deltaTime);
        targetY = Mathf.Lerp(targetY, 0, dnSpeed * Time.deltaTime);
        centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }


    private void LateUpdate()
    {
        ray = fpsCamera.ScreenPointToRay(centerPoint);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
    }

    public void ADSFire()
    {
        equippedGun.Fire();
        
        Recoil(equippedGun.continuousFire); ;
        
        if (equippedGun.currentShootMode == ShootModes.Burst)
        {
            equippedGun.burstCounter++;
        }
        equippedGun.fireTimer = 0f;
        equippedGun.currentAcc -= equippedGun.currentGun.accuracyDropPerShot;   // Change it acc to ADS drop per shot
        if (equippedGun.currentAcc <= 0)
            equippedGun.currentAcc = 10;

        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    public void HipFire()
    {
        equippedGun.Fire();
       
        if (usingRecoil)
            Recoil(equippedGun.continuousFire);
        
        if (equippedGun.currentShootMode == ShootModes.Burst)
        {
            equippedGun.burstCounter++;
        }

        equippedGun.fireTimer = 0f;
        equippedGun.currentAcc -= equippedGun.currentGun.accuracyDropPerShot;   // Change it acc to hipgfire drop per shot
        if (equippedGun.currentAcc <= 0)
            equippedGun.currentAcc = 10;


        ShootStuff(centerPoint);  //Do things to centrePoint
    }

    public void DrawCrosshair()
    {

    }

    private void ShootStuff(Vector2 screenPointToFire)
    {
        var goalPoint = Vector3.zero;

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100f);
        if (hits.Length != 0)
        {
            goalPoint = hits[hits.Length - 1].point;
        }
        else
        {
            // Get a point somehow... god knows how.
        }

        int notPlayerLayer = ~(1 << LayerMask.NameToLayer("Player"));
        Vector3 startPoint = fpsCamera.transform.position;   // Figure out a way to set startPoint as gun muzzle
        Vector3 bulletDir = (goalPoint - startPoint).normalized;

        RaycastHit[] thiccCast = Physics.SphereCastAll(startPoint, 0.1f, bulletDir, 100f, notPlayerLayer);

        //   Debug.DrawRay(startPoint, bulletDir * 1000, Color.green, 100f);

        foreach (RaycastHit hitP in thiccCast)
        {
            var decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            decal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            decal.transform.position = hitP.point;
            decal.gameObject.layer = 8;

        }
    }

    private void Recoil(int value)
    {
        if(value < 3)
        {
            Debug.Log("Min");
            targetX += UnityEngine.Random.Range(force / 2, force);
        }
        else if(value <= 8)
        {
            Debug.Log("Med");
            targetX += UnityEngine.Random.Range(force / 2, force);
            if (targetY >= 0)
                targetY -= force/2;
            else
                targetY += force/2;
        }
        else
        {
            Debug.Log("Max");
            targetX += UnityEngine.Random.Range(force / 2, force);
            if (targetY >= 0)
                targetY -= force;
            else
                targetY += force;
        }
    }
}