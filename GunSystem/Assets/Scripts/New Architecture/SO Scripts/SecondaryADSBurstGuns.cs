using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "Sec ADS Burst Gun")]
public class SecondaryADSBurstGuns : SecondaryADSGuns
{
    [SerializeField] protected float burstPause;
    [SerializeField] protected float burstRate;
}
