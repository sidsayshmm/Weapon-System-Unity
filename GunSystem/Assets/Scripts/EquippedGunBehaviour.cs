using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class EquippedGunBehaviour : MonoBehaviour
{
    public GunDefinition currentGun;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void OnChange(GunDefinition newGunDef)
    {
        currentGun = newGunDef;
    }

    public void Fire()
    {
        //Fire stuff here
    }

    public void Reload()
    {
        //Reload stuff here
    }

    public void DrawCrosshair()
    {

    }
}
