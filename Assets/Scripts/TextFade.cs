using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    private float targetOpacity = 0f;

    private Text text;
    private float hideAfter = float.MinValue;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > hideAfter) targetOpacity = 0f;
        EaseTextOpacity(targetOpacity);
    }

    public void Show(float time = float.MaxValue)
    {
        targetOpacity = 1f;
        hideAfter = (Time.timeSinceLevelLoad + time);
    }

    public void Hide()
    {
        targetOpacity = 0f;
    }

    private void EaseTextOpacity(float targetOpacity)
    {
        float alpha = text.color.a;
        alpha += (targetOpacity - alpha) * 0.05f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
