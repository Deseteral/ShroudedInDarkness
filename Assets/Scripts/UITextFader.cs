using UnityEngine;
using UnityEngine.UI;

public class UITextFader : MonoBehaviour
{
    private TimeProgress timeProgress = new TimeProgress();
    private Text text;
    public float TargetAlpha = 0f;

    void Start()
    {
        text = GetComponent<Text>();

        Color c = text.color;
        c.a = TargetAlpha;
        text.color = c;
    }

    void Update()
    {
        Color c = text.color;
        c.a = Mathf.Lerp(c.a, TargetAlpha, timeProgress.GetProgress());
        text.color = c;
    }

    public void FadeIn(float duration)
    {
        TargetAlpha = 1f;
        timeProgress.Start(duration);
    }

    public void FadeOut(float duration)
    {
        TargetAlpha = 0f;
        timeProgress.Start(duration);
    }
}
