using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;

    public GameObject TorchlightCollider;
    public GameObject Torchlight;
    private Light torchlightLight;

    public bool IsTorchlightActive = false;
    public float TorchlightLengthActive = 10f;
    public float TorchlightLength = 4f;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        torchlightLight = Torchlight.GetComponent<Light>();
    }

    void Update()
    {
        IsTorchlightActive = Input.GetMouseButton(0);

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
        {
            float actualLightLength = IsTorchlightActive ? TorchlightLengthActive : TorchlightLength;

            torchColliderScale.y = actualLightLength;
            torchColliderPosition.x = (actualLightLength / 2) + 2;
            torchlightLight.range = (actualLightLength * 2) + 2;
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
