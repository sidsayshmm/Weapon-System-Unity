using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunDefinition : ScriptableObject
{
    public int clipSize;

}

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "ADSGuns")]
public class ADSGuns : BaseGunDefinition
{

}

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "HipGuns")]
public class HipFireGuns : BaseGunDefinition
{

}

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "SniperGuns")]
public class SniperGuns : BaseGunDefinition
{
    
}

public class AllGuns : ScriptableObject
{
    public ADSGuns[] adsGuns;
    public HipFireGuns[] hipFireGuns;
    public SniperGuns[] sniperGuns;
}