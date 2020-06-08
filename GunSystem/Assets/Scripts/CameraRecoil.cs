using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    float force = 2.5f;// controls recoil amplitude 
    float upSpeed = 9f; // controls smoothing speed var dnSpeed: 
    float dnSpeed = 9f; //how fast the weapon returns to original position

    private Vector3 ang0; // initial angle = 
    float targetX; // unfiltered recoil angle 
    Vector3 ang = Vector3.zero; // smoothed angle

    void Start()
    {
        ang0 = transform.localEulerAngles; // save original angles 
    }
    void Recoil()
    {
        targetX += force; // add recoil force 
    }
    void Update()
    { // smooth movement a little 
        if(Input.GetMouseButton(1))
        {
            Recoil();
            Debug.Log("RECOIL ONCE");
        }
        ang.x = Mathf.Lerp(ang.x, targetX, upSpeed*Time.deltaTime);
        transform.localEulerAngles = ang0 - ang; // move the camera or weapon 
        targetX = Mathf.Lerp(targetX, 0, dnSpeed* Time.deltaTime); // returns to rest 
    }
}
