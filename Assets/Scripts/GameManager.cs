using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Sun;

    private const float DAY_NIGHT_CYCLE_SEC = (10f * 60f);

    void Start()
    {
    }

    void Update()
    {
        float timeProgress = (((Time.timeSinceLevelLoad % DAY_NIGHT_CYCLE_SEC) / DAY_NIGHT_CYCLE_SEC) * 360);
        Sun.transform.rotation = Quaternion.Euler(timeProgress, -30, 0);
    }
}
