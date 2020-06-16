using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunDefinition", menuName = "Sec ADS Normal Gun")]
public class SecondaryADSGuns : SecondaryGunsDefinition
{
    [SerializeField] protected float scopeInTime;
    [SerializeField] protected float scopeOutTime;
}
