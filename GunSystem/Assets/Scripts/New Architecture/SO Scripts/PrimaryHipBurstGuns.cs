using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "Prim Hip Burst Gun")]
public class PrimaryHipBurstGuns : PrimaryGunsDefinition
{
    [SerializeField] protected float burstPause;
    [SerializeField] protected float burstRate;
}
