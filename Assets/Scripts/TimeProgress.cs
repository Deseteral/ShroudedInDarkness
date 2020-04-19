using UnityEngine;

public class TimeProgress
{
    private float endsAfter;
    private float duration = float.MinValue;

    public void Start(float duration)
    {
        this.duration = duration;
        endsAfter = (Time.timeSinceLevelLoad + duration);
    }

    public float GetProgress()
    {
        if (duration == float.MinValue) return 0f;
        if (Time.timeSinceLevelLoad > endsAfter) return 1f;
        return (1f - ((endsAfter - Time.timeSinceLevelLoad) / duration));
    }
}
