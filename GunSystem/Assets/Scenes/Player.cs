using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTests
{
    public class Player : MonoBehaviour
    {
        Vector2 centerPoint;
        private Camera fpsCamera;

        float force = 50f;// controls recoil amplitude 
        float upSpeed = 9f; // controls smoothing speed var dnSpeed: 
        float dnSpeed = 6f; //how fast the weapon returns to original position
        private Vector3 initalAngle; // initial angle = 
        private float targetX; // unfiltered recoil angle 
        private float targetY;
        Vector3 newAngle = Vector3.zero; // smoothed angle


        [SerializeField] bool usingRecoil;

        private void Awake()
        {
            Debug.Log("Active");
                fpsCamera = Camera.main;
            centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            initalAngle = fpsCamera.transform.localEulerAngles;
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Recoil();
                ShootStuff(centerPoint);
            }

            newAngle.x = Mathf.Lerp(newAngle.x, targetX, upSpeed * Time.deltaTime);   // Calculate the vertical rotation to wanna push up
            newAngle.y = Mathf.Lerp(newAngle.y, targetY, upSpeed * Time.deltaTime); // Calculate the horizontal rotation you wanna push up
                                                                                    //To do. Make this randomized to left||right
            fpsCamera.transform.localEulerAngles = initalAngle - newAngle; // move the camera upwards.

            targetX = Mathf.Lerp(targetX, 0, dnSpeed * Time.deltaTime);
            targetY = Mathf.Lerp(targetY, 0, dnSpeed * Time.deltaTime);
            centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        }

        private void LateUpdate()
        {
            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * 100, Color.green);
        }

        private void ShootStuff(Vector2 screenPointToFire)
        {
            Ray ray = fpsCamera.ScreenPointToRay(screenPointToFire);
            Vector3 goalPoint = Vector3.zero;

            RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * 100, Color.red, 100f);
        }

        private void Recoil()
        {
            targetX += force;

            if (targetY >= 0)
                targetY -= force;
            else
                targetY += force;
        }
    }
}