using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FPSMovement : MonoBehaviour
{
    // Player info
    public GameObject player;
    private CharacterController controller;

    //Inputs
    float xAxis;
    float zAxis;

    private Vector3 moveDirection = Vector3.zero;

    //MovementSpeed
    public float speed = 6.0f;
    public float jumpSpeed = 80.0f;
    public float gravity = 20.0f;

    private void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        

       // Debug.Log(controller.isGrounded);

        moveDirection = new Vector3(xAxis, 0.0f, zAxis);
        moveDirection *= speed;

        if (Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                moveDirection.y = jumpSpeed;
                Debug.Log("JUMPING");
            }

        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
