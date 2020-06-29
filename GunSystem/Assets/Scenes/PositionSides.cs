using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSides : MonoBehaviour
{

	public Transform pointA;
	public Transform pointB;
	public Transform diceSide;
	public float distanceFromCenter;
	public Vector3 normalVector;
	//public Vector3 rotationVector;
	RaycastHit hit;

	void Update()
	{

		if (Physics.Linecast(pointA.position, pointB.position, out hit))
		{

			normalVector = hit.normal;
			//rotationVector = Quaternion.LookRotation(normalVector).eulerAngles;
		}
		Debug.DrawLine(pointA.position, pointB.position, hit.collider == null ? Color.red : Color.green);
		/*
		if (diceSide != null)
		{

			diceSide.position = normalVector * distanceFromCenter;
			Debug.DrawRay(diceSide.parent.position, normalVector * distanceFromCenter, Color.yellow);
		}*/
	}
}