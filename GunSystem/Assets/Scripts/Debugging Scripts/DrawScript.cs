using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script is no longer in use >_>
public class DrawScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public CurrentCircle circle;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
        DrawTriangle(positions);
    }
    void DrawTriangle(Vector3[] vertexPositions)
    {
        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(vertexPositions);
    }


    private void Update()
    {
        //Vector3 A = new Vector3(circle.pointC.x, circle.pointC.y, 0);
       // Vector3 B = new Vector3(circle.pointD.x, circle.pointD.y, 0);
       // Vector3 C = new Vector3(circle.centerPoint.x, circle.centerPoint.y, 0);

       // Debug.Log("A = " + A + " B = " + B + " C = " + C);
       // Debug.Log("Final Point = " + circle.finalPoint);
    }

}
/*
//  Vector3[] positions = new Vector3[3] { A,B,C };

Vector3 A = new Vector3(circle.pointC.x, circle.pointC.y, 0);
Vector3 B = new Vector3(circle.pointD.x, circle.pointD.y, 0);
Vector3 C = new Vector3(circle.centerPoint.x, circle.centerPoint.y, 0);

circle = circle.GetComponent<CurrentCircle>(); */