using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float Speed = 1000f;
    public float TargettingDistance = 20f;

    private GameObject player;
    private GameObject torchlightCollider;
    private new Rigidbody rigidbody;

    private Vector3 direction;
    private Vector3 target;
    private Vector3 spawnPoint;

    private float confiusionTargetTime = float.MinValue;
    private bool trackingPlayer = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        spawnPoint = transform.position;

        player = GameObject.FindWithTag("Player");
        torchlightCollider = GameObject.Find("Player/TorchlightCollider");
    }

    void FixedUpdate()
    {
        // AI, set target
        if (confiusionTargetTime > Time.timeSinceLevelLoad)
        {
            target = transform.position;
        }
        else
        {
            bool prevFrameTrackingPlayer = trackingPlayer;

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= TargettingDistance)
            {
                target = player.transform.position;
                trackingPlayer = true;
            }
            else
            {
                target = spawnPoint;
                trackingPlayer = false;
            }

            if (prevFrameTrackingPlayer && !trackingPlayer)
            {
                confiusionTargetTime = (Time.timeSinceLevelLoad + Random.Range(2f, 6f));
            }
        }

        // Calc direction
        direction = (transform.position - target);
        direction.y = 0f;
        direction *= -1f;

        // Actually move
        rigidbody.AddForce((direction.normalized * Speed), ForceMode.Force);

        // Rotate towards movement direction
        float directionAngle = Mathf.Atan2(-direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, directionAngle, 0f);
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.name == "TorchlightCollider" || collider.name == "CampfireTrigger")
        {
            BackOff(0.5f);
        }
    }

    private void BackOff(float scale)
    {
        rigidbody.AddForce((-direction * Speed * scale), ForceMode.Force);
    }
}
