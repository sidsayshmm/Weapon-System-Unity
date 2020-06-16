using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "Prim ADS Burst Gun")]
public class PrimaryADSBurstGuns : PrimaryADSGuns
{
    [SerializeField] public float burstPause;
    [SerializeField] public float burstRate;
}
