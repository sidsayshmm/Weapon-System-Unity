using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "New Primary Gun")]
public class PrimaryGunsDefinition : BaseGunDefinition
{
    public override void HipFire()
    {
        base.HipFire();
    }

    public override void ADSFire()
    {
        base.ADSFire();
    }
}
