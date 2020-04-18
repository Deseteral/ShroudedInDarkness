using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float Speed = 1000f;

    private GameObject player;
    private new Rigidbody rigidbody;

    private GameObject torchlightCollider;

    private Vector3 direction;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        torchlightCollider = GameObject.Find("TorchlightCollider");
    }

    void FixedUpdate()
    {
        direction = (transform.position - player.transform.position);
        direction.y = 0f;
        direction *= -1f;

        rigidbody.AddForce((direction.normalized * Speed), ForceMode.Force);

        float directionAngle = Mathf.Atan2(-direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, directionAngle, 0f);
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.name == "TorchlightCollider" || collider.name == "CampfireTrigger")
        {
            rigidbody.AddForce((-direction * Speed * 0.5f), ForceMode.Force);
        }
    }
}
