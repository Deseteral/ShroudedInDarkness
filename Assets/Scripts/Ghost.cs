using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float Speed = 1000f;

    private GameObject player;
    private new Rigidbody rigidbody;

    private GameObject torchlightCollider;
    private bool reverseGoesBrr;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        torchlightCollider = GameObject.Find("TorchlightCollider");
    }

    void FixedUpdate()
    {
        Vector3 diff = transform.position - player.transform.position;
        diff.y = 0f;
        float direction = Mathf.Atan2(diff.z, -diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, direction, 0f);

        float actualSpeed = reverseGoesBrr ? Speed : -Speed;
        rigidbody.AddForce((diff.normalized * actualSpeed), ForceMode.Force);    
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "TorchlightCollider") reverseGoesBrr = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name == "TorchlightCollider") reverseGoesBrr = false;
    }
}
