using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FPSCameraControl : MonoBehaviour
{
    readonly float mouseSensitivity = 100f;

    [SerializeField] private Vector3 def = new Vector3(1, 1, 1);

    
    public Transform playerBody;
    public Transform cameraDad;
    
    float xRotation = 0f;

    //public Transform camTransform;
    public Vector3 lastRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        cameraDad.Rotate(Vector3.right * -mouseY);
        playerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("GOING ONCE");
           // transform.Rotate(-5f, 0, 0);
        }

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //erBody.GetComponent<Transform>().rotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Vector2 mouseInput = new Vector2(mouseX, mouseY);
        if(mouseInput != null)
        {
            lastRotation = Camera.main.transform.rotation.eulerAngles;
            //Debug.Log("RESET = " + Camera.main.transform.rotation);
        }
    }

    void GoUp()
    {
        //Rotate about x for up 
        //Rotate about y for left/right recoil.
    }

    void GoDown()
    {
        //Start rotating downwards. 
        //  transform.rotation = Quaternion.Slerp(transform.rotation, lastRotation, 5f * Time.deltaTime);
        // transform.rotation = transform.Rotate(lastRotation * mouseSensitivity);

        // transform.Rotate
    }


    /*
     * delegate x = A + B;
     * 
     * 
     * 
     * 
     */
}
