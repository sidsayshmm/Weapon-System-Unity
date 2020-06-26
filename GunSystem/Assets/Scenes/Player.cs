using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int numberMax;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Matrix4x4 localToWorld = transform.localToWorldMatrix;

            Mesh mesh = GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;
            Debug.Log($"Length = {vertices.Length }    Mesh Vertices : ");

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = localToWorld.MultiplyPoint3x4(vertices[i]);
            }

            vertices = BubbleSort(vertices);
            Vector3 totalPosition = Vector3.zero;
            for (int i = 0; i < numberMax; i++)
            {

                //Debug.Log(vertices[i]);
                totalPosition += vertices[i];
            }
            totalPosition = totalPosition / numberMax;

            var decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            decal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            decal.transform.position = totalPosition;
        }
    }


    Vector3[] BubbleSort(Vector3[] vertices)
    {
        int n = vertices.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (vertices[j].y < vertices[j + 1].y)
                {
                    Vector3 temp = vertices[j];
                    vertices[j] = vertices[j + 1];
                    vertices[j + 1] = temp;
                }
            }
        }
        return vertices;
    }
}