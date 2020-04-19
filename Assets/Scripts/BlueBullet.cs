using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    public float Speed = 10f;

    void Start()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f), Space.Self);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos += (transform.forward * (Speed * Time.deltaTime));
        transform.position = pos;
    }
}
