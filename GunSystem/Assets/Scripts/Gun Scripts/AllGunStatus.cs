using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGunStatus : MonoBehaviour
{
 //   public ChangeGunCheck cgc;
    public WeaponManager weaponManager;

    public int[] ammoAll;
    public Dictionary<string, int> status;

    public void Start()
    {
       status = new Dictionary<string, int>();
        Debug.Log(weaponManager);
        Debug.Log(weaponManager.allGuns);
        Debug.Log(weaponManager.allGuns.primaryGuns[0]);
        Debug.Log((BaseGunDefinition)weaponManager.allGuns.primaryGuns[0]);

        /*
       foreach(BaseGunDefinition def in weaponManager.allGuns.primaryGuns)
       {
            status.Add(def.name, def.clipSize);
       }
        foreach (BaseGunDefinition def in weaponManager.allGuns.secondaryGuns)
        {
            status.Add(def.name, def.clipSize);
        }*/
    }
}
