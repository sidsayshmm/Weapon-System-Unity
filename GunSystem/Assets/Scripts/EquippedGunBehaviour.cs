using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedGunBehaviour : MonoBehaviour
{
    public GunDefinition currentGun;
    public EquippedGunStatus egs;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && egs.currentAmmo > 0)
        {
            //Fire from that gun.
            currentGun.Fire();
            egs.currentAmmo--;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            currentGun.Reload();
            egs.currentAmmo = currentGun.maxAmmo;
        }
    }

    public void OnChange(GunDefinition newGunDef)
    {
        currentGun = newGunDef;
        egs = newGunDef.status;
    }
}
