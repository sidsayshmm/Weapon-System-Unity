using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CurrentCircle : MonoBehaviour
{

    public EquippedGunBehaviour egb;
    
    public int shotsFired;

    public Vector3 centerPoint;
    private float radius;

    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;
    public Vector3 pointE;
    public Vector3 pointF;
    public Vector2 finalPoint;

    public float cosAngle;
    public float sinAngle;

    private float random1;
    private float random2;

    public Material mat;
    public Material mat2;
    private Camera cam;
    GameObject sphere;
    GameObject sphere2;
    GameObject sphere3;
    GameObject sphere4;

    void Start()
    {
        egb = GetComponent<EquippedGunBehaviour>();
        centerPoint = new Vector3(Screen.width / 2, Screen.height / 2,-5f);
        cam = Camera.main;
        CreateCentrePoint(centerPoint);


        
        sinAngle = Mathf.Sin(30 * Mathf.PI / 180);
        cosAngle = Mathf.Cos(30 * Mathf.PI / 180);


        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //radius =  Mathf.Lerp(minRadius,maxRadius,currentAcc/curretGun.MaxAcc)
    }

    void Update()
    {
        radius = Mathf.Lerp(200f, 20f, egb.currentAcc / 250.0f);

        if(Input.GetMouseButton(0))
        {
            Debug.Log("MSP" + Input.mousePosition);
          //  Debug.Log("MWP" + cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5f)));
        }

        OuterBounds();
        InnerBounds();
    }

    public void InnerBounds()
    {
        float x = (float)egb.continousFire;
        sphere4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere4.gameObject.name = "POINT D";

        float yval = Mathf.Lerp(pointC.y, pointA.y, x / 10f);

        pointD = pointC;
        pointD.y = yval;


        //  sphere4.transform.position = new Vector3(-cam.ScreenToWorldPoint(pointD).x, -cam.ScreenToWorldPoint(pointD).y, -5f);
        sphere4.transform.position = Vector3.MoveTowards(sphere4.transform.position, new Vector3(-cam.ScreenToWorldPoint(pointD).x, -cam.ScreenToWorldPoint(pointD).y, -5f), 1f);

    }
    public void OuterBounds()
    {
        sphere.GetComponent<Renderer>().material = mat;
        sphere2.GetComponent<Renderer>().material = mat;
        sphere3.GetComponent<Renderer>().material = mat;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        pointA = new Vector3(centerPoint.x + radius * sinAngle, centerPoint.y + radius * cosAngle, -5f);
        pointB = new Vector3(centerPoint.x - radius * sinAngle, centerPoint.y + radius * cosAngle, -5f);
        pointC = centerPoint;
        sphere.transform.position = new Vector3(-cam.ScreenToWorldPoint(pointA).x, -cam.ScreenToWorldPoint(pointA).y, -5f);
        sphere.gameObject.name = "POINT A";
        sphere2.transform.position = new Vector3(-cam.ScreenToWorldPoint(pointB).x, -cam.ScreenToWorldPoint(pointB).y, -5f);
        sphere2.gameObject.name = "POINT B";
        sphere3.transform.position = new Vector3(cam.ScreenToWorldPoint(pointC).x, cam.ScreenToWorldPoint(pointC).y, -5f);
        sphere3.gameObject.name = "POINT C";
    }
    
    public void CreateCentrePoint(Vector2 point)
    {
        var spherex = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        spherex.transform.position = new Vector3(cam.ScreenToWorldPoint(point).x, cam.ScreenToWorldPoint(point).y, -5f); ;
        spherex.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        spherex.GetComponent<Renderer>().material = mat2;
     //   Debug.Log("Done");
    }
}

/*
    public void SelectPoint()
    {
       // Debug.Log("Starting Point");
        //radius = 15000 / currentAcc;
        pointA = new Vector2(centerPoint.x + radius * sinAngle, centerPoint.y + radius * cosAngle);
        pointB = new Vector2(centerPoint.x - radius * sinAngle, centerPoint.y + radius * cosAngle);
        pointC = centerPoint;
        finalPoint = (1 - Mathf.Sqrt(random1)) * pointC + Mathf.Sqrt(random2) * (1 - random2) * pointA + Mathf.Sqrt(random1) * random2 * pointB;

        CreateSphere(finalPoint); 
    }

    public void CreateSphere(Vector2 point)
    {
        var spherex = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        spherex.transform.position = new Vector3(cam.ScreenToWorldPoint(point).x, cam.ScreenToWorldPoint(point).y, -5f); ;
        spherex.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        spherex.GetComponent<Renderer>().material = mat;
        //Debug.Log("Done");
    }
*/