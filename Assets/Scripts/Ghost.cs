using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float Speed = 1000f;

    private GameObject player;
    private new Rigidbody rigidbody;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 diff = transform.position - player.transform.position;
        diff.y = 0f;
        float direction = Mathf.Atan2(diff.z, -diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, direction, 0f);
        Debug.Log(diff.normalized * -Speed);
        rigidbody.AddForce((diff.normalized * -Speed), ForceMode.Force);    
    }
}
