using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CurrentCircle : MonoBehaviour
{
    [Range(10,100)] public float currentAcc;
    public int shotsFired;

    public Vector3 centerPoint;
    private float radius;

    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
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

    void Start()
    {
        centerPoint = new Vector3(Screen.width / 2, Screen.height / 2,-5f);
        cam = Camera.main;
        CreateSphere(centerPoint);

        currentAcc = 100;


        sinAngle = Mathf.Sin(30 * Mathf.PI / 180);
        cosAngle = Mathf.Cos(30 * Mathf.PI / 180);

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().material = mat;
        sphere2.GetComponent<Renderer>().material = mat;
        sphere3.GetComponent<Renderer>().material = mat;
        sphere.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        sphere2.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        sphere3.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    
    }

    void Update()
    {
        radius = 150000 / currentAcc;
        pointA = new Vector3(centerPoint.x + radius * sinAngle, centerPoint.y + radius * cosAngle, -5f);
        pointB = new Vector3(centerPoint.x - radius * sinAngle, centerPoint.y + radius * cosAngle, -5f);
        pointC = centerPoint;

        Debug.Log(" A = " + pointA + " B = " + pointB + " C = " + pointC);
        Debug.Log("WP A = " + cam.ScreenToWorldPoint(pointA) + "WP B = " + cam.ScreenToWorldPoint(pointB) + "WP C = " + cam.ScreenToWorldPoint(pointC));
        if(Input.GetMouseButton(0))
            Debug.Log(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5f)));
        OuterBounds();
    }

    public void OuterBounds()
    {
        sphere.transform.position = new Vector3(cam.ScreenToWorldPoint(pointA).x, cam.ScreenToWorldPoint(pointA).y, cam.ScreenToWorldPoint(pointA).z);
 
        sphere.transform.position = new Vector3(cam.ScreenToWorldPoint(pointB).x, cam.ScreenToWorldPoint(pointB).y, cam.ScreenToWorldPoint(pointB).z);

        sphere.transform.position = new Vector3(cam.ScreenToWorldPoint(pointC).x, cam.ScreenToWorldPoint(pointC).y, cam.ScreenToWorldPoint(pointC).y);
    }

    public void SelectPoint()
    {
        Debug.Log("Starting Point");
        radius = 15000 / currentAcc;
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
        spherex.GetComponent<Renderer>().material = mat2;
        Debug.Log("Done");
    }


}
