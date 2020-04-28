using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GunDefinition", menuName = "GunTemplate")]
public class GunDefinition : ScriptableObject
{
    public string gunName;
    public Mesh model;

    public int shotsPerRound;
    public int firingRate;

    public int maxAmmo;
    public int maxClips;

    public bool isAuto;
    public bool isSemi;


    public bool isBurst;
    public int burstPause;
    public int burstCounter;
    public int burstRate;

    public int reloadTime;



    public Vector3 nosePoint; //Get this done later to add recoil to nose!
}
