using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New allGuns", menuName = "All Guns Holder")]
public class AllGuns : ScriptableObject
{
    public List<PrimaryGunsDefinition> primGuns;
    public List<SecondaryGunsDefinition> secoGuns;
}
