using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireable
{
    void Fire();
}


public interface IRecoil<T>
{
    void Recoil(T currentFire);
}