using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    private new Light light;
    private Color targetColor;
    private TimeProgress timeProgress = new TimeProgress();

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        light.color = Color.Lerp(light.color, targetColor, timeProgress.GetProgress());
    }

    public void ChangeColor(Color targetColor, float duration)
    {
        this.targetColor = targetColor;
        timeProgress.Start(duration);
    }
}
