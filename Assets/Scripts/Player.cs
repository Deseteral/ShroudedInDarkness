using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;
    public GameObject TorchlightCollider;

    private new Rigidbody rigidbody;

    public bool IsTorchlightLong = false;
    private float torchlightLengthLong = 10f;
    private float torchlightLengthSmall = 4f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() 
    {
        // Rotation
        // TODO: Controller support
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, (1 << 8)))
        {
            Vector3 diff = transform.position - hit.point;
            float direction = Mathf.Atan2(diff.z, -diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, direction, 0f);
        }

        // Torchlight
        Vector3 torchColliderScale = TorchlightCollider.transform.localScale;
        Vector3 torchColliderPosition = TorchlightCollider.transform.localPosition;
        if (IsTorchlightLong)
        {
            torchColliderScale.y = torchlightLengthLong;
            torchColliderPosition.x = 2 + (torchlightLengthLong / 2);
        } 
        else 
        {
            torchColliderScale.y = torchlightLengthSmall;
            torchColliderPosition.x = 2 + (torchlightLengthSmall / 2);
        }
        TorchlightCollider.transform.localScale = torchColliderScale;
        TorchlightCollider.transform.localPosition = torchColliderPosition;
    }

    void FixedUpdate()
    {
        Vector3 delta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.AddForce(delta * Speed, ForceMode.Force);
    }
}
