﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Full,
    Semi
}

public enum BurstType
{
    isBurst,
    noBurst
}

public enum ShootType
{
    ADS,
    Hipfire
}

public enum ShootMode
{
    Normal,
    SingleShot
}

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "GunTemplate")]
public class GunDefinition : ScriptableObject
{
    public string gunName;
    public Mesh model;

    public GunType gunType;
    public BurstType burstType;
    public ShootType shootType;
    public ShootMode shootMode;

    public int shotsPerRound;
    public float firingRate;

    public int maxAmmo;
    public int maxClips;


    public bool burstOnly;


    public float scopeInTime;     //Require this only if isADS is true. 

 
    public int burstPause;       //Require this only if isBurst is true. 
    public int burstRate;        //Require this only if isBurst is true. 

    public int reloadTime;



    public Vector3 nosePoint; //Get this done later to add recoil to nose!
}
