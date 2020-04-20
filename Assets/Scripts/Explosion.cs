using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float timeToHide;
    private float timeToDie;

    public GameObject EffectLight;

    void Start()
    {
        timeToHide = Time.timeSinceLevelLoad + 1f;
        timeToDie = Time.timeSinceLevelLoad + 5f;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= timeToHide)
        {
            EffectLight.GetComponent<Light>().intensity = 0f;
        }

        if (Time.timeSinceLevelLoad >= timeToDie)
        {
            Destroy(this.gameObject);
        }
    }
}
