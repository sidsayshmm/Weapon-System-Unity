using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 screenPoint;
    private void Update()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(screenPoint));
    }
}   