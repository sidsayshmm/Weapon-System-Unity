using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestingScene : MonoBehaviour
{
    Vector3 originPosition;
    Quaternion originRotation;
    [FormerlySerializedAs("shake_decay")] [SerializeField] float shakeDecay;
    [FormerlySerializedAs("shake_intensity")] [SerializeField] float shakeIntensity = 0f;

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Shake"))
        {
            Shake();
        }
    }

    void FixedUpdate()
    {
        if (shakeIntensity > 0)
        {
            Debug.Log("Shaking");
            transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;
            transform.rotation = new Quaternion(
                            originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                            originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                            originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                            originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);
            shakeIntensity -= shakeDecay;
        }

    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shakeIntensity = .3f;
        shakeDecay = 0.1f;

        
    }
}
