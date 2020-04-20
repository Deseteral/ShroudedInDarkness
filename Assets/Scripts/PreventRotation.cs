using UnityEngine;

public class PreventRotation : MonoBehaviour
{
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = originalRotation;
    }
}
