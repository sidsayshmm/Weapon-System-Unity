using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "Sec Hip Burst Gun")]
public class SecondaryHipBurstGuns : SecondaryGunsDefinition
{
    [SerializeField] protected float burstPause;
    [SerializeField] protected float burstRate;
}
