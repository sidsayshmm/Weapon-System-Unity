using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGunStatus : MonoBehaviour
{
    public ChangeGunCheck cgc;

    public int[] ammoAll;
    public Dictionary<string, int> status;

    public void Start()
    {
       status = new Dictionary<string, int>();
       foreach(GunDefinition def in cgc.allGuns)
       {
            status.Add(def.gunName, def.maxAmmo);
       }
    }
}
