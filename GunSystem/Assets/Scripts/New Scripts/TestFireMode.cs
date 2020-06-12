using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFireMode : MonoBehaviour, IFireable, IRecoil<int>
{
    public void Fire()
    {
        // This Fires through the sight.
    }

    public void Recoil(int currentFire)
    {
        
    }
}
