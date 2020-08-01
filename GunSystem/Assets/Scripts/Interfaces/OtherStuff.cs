using System;
using UnityEngine;

public interface IAttachment {}


public class Stock : IAttachment
{
    public float WeightMultiplier;
    public float somethingElse;
    public GameObject x;
}


public class Grip : IAttachment
{
    
}

public class Bullet : IAttachment
{
    
}

public class Suppressor : IAttachment
{
    
}

public class Sights : IAttachment
{
    public Sightss name;
    public GameObject model;
    public float accuracyModifier;
    public float someOtherModifier;
}

public enum Sightss
{
    RedDot = (1<<0),
    Holographic= (1<<1),
    Tactical = (1<<2)
}

public interface ISights
{
    
}

public class BaseSight : ISights
{
    public Sightss type;
    public GameObject model;
    public float someModifier;
    public float someOtherModifier;
}

public interface IMagazine
{
    
}

public class Magazine : IMagazine
{
    public MagTypes type;
    public float someModifier;
    public float someOtherModifier;
}

public enum MagTypes
{
    Fast,
    Extended
}