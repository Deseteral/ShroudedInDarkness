using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;

    private Vector3 offset = new Vector3(0f, 10f, -6f);

    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = Target.transform.position + offset;
    }
}
