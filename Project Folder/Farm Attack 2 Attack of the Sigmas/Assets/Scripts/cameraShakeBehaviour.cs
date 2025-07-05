using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShakeBehaviour : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.1f;

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Apply Perlin noise to the camera position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;

            // Decrease shake timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position when the shake is over
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera(float intensity)
    {
        shakeIntensity = intensity;
           // Trigger camera shake
           shakeTimer = shakeDuration;
    }
}
