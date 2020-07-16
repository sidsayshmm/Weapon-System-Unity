using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "New Primary Gun")]
public class PrimaryGunsDefinition : BaseGunDefinition
{
    public override void HipFire()
    {
        base.HipFire();
        Camera x = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Collider.
    }

    public override void ADSFire()
    {
        base.ADSFire();
    }
}
