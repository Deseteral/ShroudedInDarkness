using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Target.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Target.transform.position + offset;
    }
}
