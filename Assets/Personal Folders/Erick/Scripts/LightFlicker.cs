using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float flickerIntensity = 0.2f;
    public float flickersPerSecond = 3.0f;
    public float speedRandomness = 1.0f;

    private float time;
    private float startingIntensity;
    private Light lightSource;

    void Start()
    {
        lightSource = GetComponent<Light>();
        startingIntensity = lightSource.intensity;
    }

    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        lightSource.intensity = startingIntensity + Mathf.Sin(time * flickersPerSecond) * flickerIntensity;
    }
}