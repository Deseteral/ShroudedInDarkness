using UnityEngine;

public class GuiFaceCamera : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        // TODO: Force UI to ignore Z testing
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
