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
public enum DefaultShootMode
{
    Normal,
    Burst,
    Single
}

public class BaseGunDefinition : ScriptableObject
{
    public WeaponType weaponType;
    public SightType sightType;
    public AvailableShootModes shootModes;
    public DefaultShootMode defShootMode;
    public int clipSize;
    public float reloadTime;
    public int shotsPerRound;
    public float firingRate;
    public bool modeChanges;
    
}