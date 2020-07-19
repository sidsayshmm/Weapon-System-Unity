using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "New Secondary Gun")]
public class SecondaryGunsDefinition : BaseGunDefinition
{
    public override void AdsFire(ShootData shootData)
    {
        AddAdsRecoil(shootData.gunObject);
    }

    public override void HipFire(ShootData shootData)
    {
        AddHipRecoil(shootData.gunObject);
    }

    public override void AddAdsRecoil(GameObject gunModel)
    {

    }

    public override void AddHipRecoil(GameObject gunModel)
    {

    }
}
