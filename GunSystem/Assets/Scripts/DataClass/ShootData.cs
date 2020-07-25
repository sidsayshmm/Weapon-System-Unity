using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootData
{
    public readonly GameObject gunObject;
    public readonly EquippedGun gunScript;
    public readonly bool useRecoil;
    public readonly Vector2 centrePoint;
    private readonly Camera fpsCamera;

    public ShootData(GameObject gunObject, EquippedGun gunScript, bool useRecoil, Vector2 centrePoint, Camera fpsCamera)
    {
        this.gunObject = gunObject;
        this.gunScript = gunScript;
        this.useRecoil = useRecoil;
        this.centrePoint = centrePoint;
        this.fpsCamera = fpsCamera;
    }

    public void ShootStuff(Vector2 pointToFire)
    {
        var goalPoint = Vector3.zero;
        Ray ray = fpsCamera.ScreenPointToRay(pointToFire);
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
}