using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private new Light light;
    private float intensityMax;

    void Start()
    {
        light = GetComponent<Light>();
        intensityMax = light.intensity;
    }

    void Update()
    {
        light.intensity += (Random.Range(0f, intensityMax) - light.intensity) * 0.1f;
    }
}
