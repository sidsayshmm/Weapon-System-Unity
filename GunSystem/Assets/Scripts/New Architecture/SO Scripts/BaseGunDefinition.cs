using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Primary,
    Secondary
}
public enum SightType
{
    ADS,
    Normal
}
[Flags]
public enum AvailableShootModes
{
    Normal = 0,
    Burst = 1,
    Single = 2
}
public enum ShootModes
{
    Normal,
    Burst,
    Single
}
public class BaseGunDefinition : ScriptableObject
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
    public int maxClips;
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
}