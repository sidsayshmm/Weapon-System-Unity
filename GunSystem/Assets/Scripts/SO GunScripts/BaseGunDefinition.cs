using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;


/*
 * Damage
 * Accuracy
 * Range
 * Fire rate
 * Mobility
 *
 *
 *Sights -  Holo / Tactical / Red Dot
 *Grips - Foregrip
 * Muzzle - Suppressor
 * Magazine - Fast Reload, Extended, **Quickdraw**
 * Laser
 * Stock
 * Bullet
 * 
 */




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
    #region
    [PreviewField(125)] [HideLabel] [HorizontalGroup("Bleh",125)]
    public GameObject gunMesh;
    
    [LabelWidth(100)]  [BoxGroup("Bleh/Basic Stats")] [GUIColor(255/255f,59/255f,59/255f)]
    public string weaponName;
    [LabelWidth(100)] [BoxGroup("Bleh/Basic Stats")]  [Range(3,100)] [GUIColor(59/255f,245/255f,255/255f)]
    public int clipSize;
    [LabelWidth(100)] [BoxGroup("Bleh/Basic Stats")]  [Range(3,200)] [GUIColor(59/255f,245/255f,255/255f)]
    public int maxAmmo;
    [LabelWidth(100)] [BoxGroup("Bleh/Basic Stats")]  [Range(3,100)] [GUIColor(59/255f,245/255f,255/255f)]
    public float reloadTime;
    [LabelWidth(100)] [BoxGroup("Bleh/Basic Stats")]  [Range(1,10)]  [GUIColor(59/255f,245/255f,255/255f)]
    public int shotsPerRound;
    
    [PropertySpace]
    
    [LabelWidth(150)] [GUIColor(255/255f,59/255f,59/255f)]
    public WeaponType weaponType;
    [LabelWidth(150)] [GUIColor(59/255f,245/255f,255/255f)]
    public SightType sightType;
    [LabelWidth(150)] [GUIColor(59/255f,245/255f,255/255f)]
    public SightType availableSightTypes;
    [LabelWidth(150)] [GUIColor(255/255f,59/255f,59/255f)]
    public ShootModes defaultShootMode;
    
    #endregion
    [LabelWidth(150)] [GUIColor(59/255f,245/255f,255/255f)]
    
    public AvailableShootModes availableShootModes;
    public Attachments availableAttachments;
    
    [BoxGroup("Modes")]
    public BurstFireDetails burstFireDetails;
    [BoxGroup("Modes")]
    public SemiFireDetails semiFireDetails;
    [BoxGroup("Modes")]
    public AutoFireDetails autoFireDetails;


    public Attachables[] attachmentModifiers;

    
    public float firingRate;
    public float accuracyDropPerShot;
    public float accuracyGainPerSec;
    public float maxAccuracy;
    public float scopeInTime;  
    public float scopeOutTime;
    public float burstPause;
    public float burstRate;


    


    
    public void blaaa()
    {
        if (availableAttachments.HasFlag(Attachments.Magazine))
        {
            //Something..
        }
    }
    
    public abstract void AdsFire(ShootData shootData);
    public abstract void HipFire(ShootData shootData);
    public abstract void AddAdsRecoil(GameObject gunModel);
    public abstract void AddHipRecoil(GameObject gunModel);
}


public class GunSlotAttachments
{
    [OdinSerialize] public ISightTypes[] availableSights;
    [OdinSerialize] public IMagazineTypes[] availableMagazines;
    [OdinSerialize] public IGripTypes[] availableGrips;
    [OdinSerialize] public IMuzzleTypes[] availableMuzzles;
    [OdinSerialize] public IBulletTypes[] availableBullets;
    [OdinSerialize] public IOtherAttachments[] otherAttachments;
}



[System.Serializable]
public class BurstFireDetails
{
    [Range(1,10)] public float burstRate;
    [Range(1,10)] public float burstPause;
    [Range(2,5)] public int burstCount;
    
}
[System.Serializable]
public class SemiFireDetails
{
    [Range(1,10)] public float firingRate;
}
[System.Serializable]
public class AutoFireDetails
{
    [Range(1,10)] public float firingRate;
}


public enum ModifierType
{
    Additive,
    Multiplicative,
    Flat
}

public struct Attachables
{
    public AttachmentName attachmentName;
    public AttachmentModifiers[] attributes;
}
public struct AttachmentModifiers
{
    public Modifiables name;
    public ModifierType type ;
    public float modifierValue;
}

public enum Modifiables
{
    Damage,
    Range,
    Mobility,
    Accuracy,
    ReloadTime
}

public enum AttachmentName
{
    HeavyMag,
    LightMag,
    FastMag,
    Stock,
    Suppressor
}