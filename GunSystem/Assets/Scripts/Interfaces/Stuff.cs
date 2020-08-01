using UnityEngine;
using Sirenix.OdinInspector;

public interface ISightTypes
{
}

public interface IMagazineTypes
{
}
public interface IGripTypes{}
public interface IMuzzleTypes{}
public interface IBulletTypes{}
public interface IOtherAttachments{}





/*
public interface IBurstFire : IShootModes{ }
public interface IAutoFire : IShootModes{ }
public interface ISemiFire : IShootModes{ }

public interface IHipFire : ISightModes { }
public interface IScopeFire : ISightModes{}
*/


public interface ISightModes {}
public class HipFireStats : ISightModes
{
    [Range(10,250)] [GUIColor(255/255f,59/255f,59/255f)]
    public float maxAccuracy;
    [Range(5,50)] [GUIColor(59/255f,245/255f,255/255f)]
    public float accuracyDropPerShot;
    [Range(5,50)] [GUIColor(59/255f,245/255f,255/255f)]
    public float accuracyGainPerSec;
}
public class ScopeFireStats : ISightModes
{
    [Range(0,2)] [GUIColor(59/255f,245/255f,255/255f)]
    public float scopeInTime;  
    [Range(0,2)] [GUIColor(59/255f,245/255f,255/255f)]
    public float scopeOutTime;
    
    [PropertySpace]
    
    [Range(10,250)] [GUIColor(255/255f,59/255f,59/255f)]
    public float maxAccuracy;
    [Range(5,50)] [GUIColor(59/255f,245/255f,255/255f)]
    public float accuracyDropPerShot;
    [Range(5,50)] [GUIColor(59/255f,245/255f,255/255f)]
    public float accuracyGainPerSec;
    
}

public interface IShootModes
{
}
public class BurstFireStats : IShootModes
{
    [Range(0,3)] [GUIColor(59/255f,245/255f,255/255f)]
    public float burstPause;
    [Range(0,3)] [GUIColor(59/255f,245/255f,255/255f)]
    public float burstRate;
    [Range(0,3)] [GUIColor(59/255f,245/255f,255/255f)]
    public float burstCount;
}
public class AutoFireStats : IShootModes
{
    [GUIColor(255/255f,59/255f,59/255f)]
    public float firingRate;
}
public class SemiFireStats : IShootModes
{
    [GUIColor(255/255f,59/255f,59/255f)]
    public float firingRate;
}