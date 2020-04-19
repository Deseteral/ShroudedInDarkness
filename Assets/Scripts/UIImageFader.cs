using UnityEngine;
using UnityEngine.UI;

public class UIImageFader : MonoBehaviour
{
    private TimeProgress timeProgress = new TimeProgress();
    private Image image;
    public float TargetAlpha = 0f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        Color c = image.color;
        c.a = Mathf.Lerp(c.a, TargetAlpha, timeProgress.GetProgress());
        image.color = c;
    }

    public void FadeIn(float duration)
    {
        TargetAlpha = 0f;
        timeProgress.Start(duration);
    }

    public void FadeOut(float duration)
    {
        TargetAlpha = 1f;
        timeProgress.Start(duration);
    }
}
