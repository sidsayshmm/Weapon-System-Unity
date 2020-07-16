using System;
using UnityEngine;

public class FPSCameraControl : MonoBehaviour
{
    readonly float mouseSensitivity = 100f;

    [SerializeField] private Vector3 def = new Vector3(1, 1, 1);

    
    public Transform playerBody;
    public Transform cameraDad;
    
    float xRotation = 0f;

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


        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //erBody.GetComponent<Transform>().rotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.tag == "")
            Debug.Log("");
    }
}
