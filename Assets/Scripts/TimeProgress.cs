using UnityEngine;

public class TimeProgress
{
    private float endsAfter;
    private float duration = float.MinValue;
    private float startTime;

    public void Start(float duration)
    {
        this.duration = duration;
        endsAfter = (Time.timeSinceLevelLoad + duration);
        startTime = Time.timeSinceLevelLoad;
    }

    public bool IsDone()
    {
        if (duration == float.MinValue) return false;
        if (Time.timeSinceLevelLoad > endsAfter) return true;
        return false;
    }

    public void Reset()
    {
        duration = float.MinValue;
    }

    public float GetProgress()
    {
        if (duration == float.MinValue) return 0f;
        if (Time.timeSinceLevelLoad > endsAfter) return 1f;
        // return (1f - ((endsAfter - Time.timeSinceLevelLoad) / duration));
        return ((Time.timeSinceLevelLoad - startTime) / duration);
    }
}
