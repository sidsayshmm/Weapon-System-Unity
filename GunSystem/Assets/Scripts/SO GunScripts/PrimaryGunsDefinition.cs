﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "Old/New Primary Gun")]
public class PrimaryGunsDefinition : BaseGunDefinition
{
    public override void AdsFire(ShootData shootData)
    {
        shootData.gunScript.Fire();
        
        if (shootData.gunScript.currentShootMode == ShootModes.Burst)
            shootData.gunScript.burstCounter++;

        shootData.gunScript.fireTimer = 0f;
        shootData.gunScript.currentAcc -= shootData.gunScript.currentGun.accuracyDropPerShot;
        
        if(shootData.gunScript.currentAcc <= 0)
            shootData.gunScript.currentAcc = 10;
        
        shootData.ShootStuff(shootData.centrePoint);
        
        if(shootData.useRecoil)
            AddAdsRecoil(shootData.gunObject);
    }

    public override void HipFire(ShootData shootData)
    {
        shootData.gunScript.Fire();
        if (shootData.gunScript.currentShootMode == ShootModes.Burst)
            shootData.gunScript.burstCounter++;
        
        shootData.gunScript.fireTimer = 0f;
        shootData.gunScript.currentAcc -= shootData.gunScript.currentGun.accuracyDropPerShot;
        
        if(shootData.gunScript.currentAcc <= 0)
            shootData.gunScript.currentAcc = 10;
        
        
        if(shootData.useRecoil)
            AddHipRecoil(shootData.gunObject);
    }

    public override void AddAdsRecoil(GameObject gunModel)
    {

    }

    public override void AddHipRecoil(GameObject gunModel)
    {

    }
}
