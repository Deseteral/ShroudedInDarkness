using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    private new Light light;
    private Color targetColor;
    private float targetTime = float.MinValue;
    private float time;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        if (targetTime > Time.timeSinceLevelLoad)
        {
            float progress = (targetTime - Time.timeSinceLevelLoad) / time;
            light.color = Color.Lerp(light.color, targetColor, progress);
        }
    }

    public void ChangeColor(Color targetColor, float time)
    {
        this.targetColor = targetColor;
        this.time = time;
        targetTime = Time.timeSinceLevelLoad + time;
    }
}
