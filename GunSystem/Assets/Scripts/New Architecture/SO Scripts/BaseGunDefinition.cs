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

public enum TypeShoot
{
    Normal,
    Burst
}
public class BaseGunDefinition : ScriptableObject
{
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected SightType sightType;
    [SerializeField] protected TypeShoot typeShoot;
    [SerializeField] protected int clipSize;
    [SerializeField] protected float reloadTime;
}