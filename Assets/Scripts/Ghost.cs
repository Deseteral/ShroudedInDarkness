﻿using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float Speed = 1000f;
    public float TargettingDistance = 20f;
    public float ReceivedAttackForce = 550f;

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

    void Update()
    {
        // AI, set target
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool canSeePlayer = (distanceToPlayer <= TargettingDistance);

        if (!canSeePlayer && (confiusionTargetTime > Time.timeSinceLevelLoad))
        {
            target = transform.position;
        }
        else
        {
            bool prevFrameTrackingPlayer = trackingPlayer;

            if (canSeePlayer)
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

        // Rotate towards movement direction
        float directionAngle = Mathf.Atan2(-direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, directionAngle, 0f);
    }

    void FixedUpdate()
    {
        // Actually move
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget > 1f)
        {
            rigidbody.AddForce((direction.normalized * Speed), ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "TorchlightCollider" || collider.name == "CampfireTrigger")
        {
            BackOff(ReceivedAttackForce);
        }
    }

    private void BackOff(float scale)
    {
        rigidbody.AddForce((-direction * scale), ForceMode.Impulse);
    }
}
