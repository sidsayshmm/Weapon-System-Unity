using System;
using UnityEngine;
using Sirenix.OdinInspector;


[Flags]
public enum Attachments
{
    Stock = (1<<0),
    Sight= (1<<1),
    Blah = (1<<2),
    Magazine = (1<<3)
}
public abstract class BaseGunDefinition : SerializedScriptableObject, IShootable , IRecoilable
{
    public string weaponName;
    public Mesh gunMesh;
    
    [Header("Type")]
    public WeaponType weaponType;
    public SightType sightType;

    [Header("Modes")]
    public ShootModes defShootMode;
    public bool modeChanges;
    public AvailableShootModes availableShootModes;

    [Header("Basic Details")]
    public int clipSize;
    public int maxAmmo;
    public float reloadTime;
    public int shotsPerRound;
    public float firingRate;

    [Header("Accuracy Details")]
    public float accuracyDropPerShot;
    public float accuracyGainPerSec;
    public float maxAccuracy;

    [Header("ADS Details")]
    public float scopeInTime;  
    public float scopeOutTime;

    [Header("Burst Details")]
    public float burstPause;
    public float burstRate;


    public Attachments bleh;
    
    public void blaaa()
    {
        if (bleh.HasFlag(Attachments.Magazine))
        {
            //Something..
        }
    }
    
    public abstract void AdsFire(ShootData shootData);
    public abstract void HipFire(ShootData shootData);
    public abstract void AddAdsRecoil(GameObject gunModel);
    public abstract void AddHipRecoil(GameObject gunModel);
}