using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GunDefinition", menuName = "GunTemplate")]
public class GunDefinition : ScriptableObject
{
    public string gunName;
    public bool isAuto;
    public bool isSemi;
    public int maxAmmo;
    
    
    public GameObject gunModel;
}
