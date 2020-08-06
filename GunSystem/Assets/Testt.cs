using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Testt : MonoBehaviour
{

    [SerializeField] [Range(0,30)]private int testX = 0;
    [SerializeField] [Range(0,30)]private int testY = 0;

    private void Start()
    {
        string json = JsonUtility.ToJson("text");
        GetComponent<AudioSource>()?.Play();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.green);
        Vector3 newVector;
        
        //newVector = new Vector3(transform.forward.x + testX*Mathf.Deg2Rad, transform.forward.y + testY*Mathf.Deg2Rad,transform.forward.z );
        newVector =  Quaternion.Euler(-testX, -testY, 0) * transform.forward ;
        
        
       // var rot = Quaternion.AngleAxis(testX, transform.up) * Quaternion.AngleAxis(-testY, transform.right);
       // newVector = rot * transform.forward;
        
        
        // newVector = newVector.normalized;
        
       // newVector = Quaternion.AngleAxis(testY, Vector3.up) * Quaternion.AngleAxis(testX, Vector3.right) * transform.rotation * Vector3.forward; 
       
       Debug.DrawRay(transform.position,newVector*100,Color.yellow);
    }
}
