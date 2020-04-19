using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;
    private Vector3 offset = new Vector3(-18.37f, 15f, -18.37f); // new Vector3(-6.7f, 11.3f, -11.2f); // new Vector3(-18.37f, 15f, -18.37f);

    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = Target.transform.position + offset;
    }
}
