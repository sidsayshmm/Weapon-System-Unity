using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestingScene : MonoBehaviour
{
    Vector3 originPosition;
    Quaternion originRotation;
    [SerializeField] float shake_decay;
    [SerializeField] float shake_intensity = 0f;


    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Shake"))
        {
            Shake();
        }
    }

    void FixedUpdate()
    {
        if (shake_intensity > 0)
        {
            Debug.Log("Shaking");
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
                            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
                            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
                            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
                            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay;
        }

    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = .3f;
        shake_decay = 0.1f;

        
    }
}
