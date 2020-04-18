using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 delta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.AddForce(delta * Speed, ForceMode.Force);
    }
}
