using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRecoil : MonoBehaviour
{
    float force = 10f;// controls recoil amplitude 
    float upSpeed = 9f; // controls smoothing speed var dnSpeed: 
    float dnSpeed = 6f; //how fast the weapon returns to original position

    private Vector3 initalAngle; // initial angle = 
    [SerializeField] float targetX; // unfiltered recoil angle 
    [SerializeField] float targetY;
    Vector3 newAngle = Vector3.zero; // smoothed angle

    private float factor = 0f;

    void Start()
    {
        initalAngle = transform.localEulerAngles; // save original angles 
        //Debug.Log()
    }
    public void Recoil()
    {
        targetX += force; // add recoil force 
       // targetY += force;
     //   targetY *= -2;
        if (targetY >= 0)
            targetY -= force;
        else
            targetY += force;
    }
    void Update()
    {
        // smooth movement a little 
        newAngle.x = Mathf.Lerp(newAngle.x, targetX, upSpeed*Time.deltaTime);   // Calculate the vertical rotation to wanna push up
        newAngle.y = Mathf.Lerp(newAngle.y, targetY, upSpeed * Time.deltaTime); // Calculate the horizontal rotation you wanna push up
                                                                        //To do. Make this randomized to left||right
        transform.localEulerAngles = initalAngle - newAngle; // move the camera upwards.

        targetX = Mathf.Lerp(targetX, 0, dnSpeed * Time.deltaTime);   
        targetY = Mathf.Lerp(targetY, 0, dnSpeed * Time.deltaTime);
    }    
}
