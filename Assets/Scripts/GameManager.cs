using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Sun;
    public bool DEBUG_DayNightCycle = true;

    private const float DAY_NIGHT_CYCLE_SEC = (10f * 60f);

    void Start()
    {
    }

    void Update()
    {
        float timeProgress = DEBUG_DayNightCycle 
            ? (((Time.timeSinceLevelLoad % DAY_NIGHT_CYCLE_SEC) / DAY_NIGHT_CYCLE_SEC) * 360)
            : 90f;
        Sun.transform.rotation = Quaternion.Euler(timeProgress, -30, 0);
    }
}
