using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootData
{
    public readonly GameObject gunObject;
    public readonly EquippedGun gunScript;
    public readonly bool useRecoil;
    public readonly Vector2 centrePoint;

    public ShootData(GameObject gunObject, EquippedGun gunScript, bool useRecoil, Vector2 centrePoint)
    {
        this.gunObject = gunObject;
        this.gunScript = gunScript;
        this.useRecoil = useRecoil;
        this.centrePoint = centrePoint;
    }
}