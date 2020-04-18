using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() 
    {
        // TODO: Controller support
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, (1 << 8)))
        {
            Vector3 diff = transform.position - hit.point;
            float direction = Mathf.Atan2(diff.z, -diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, direction, 0f);
        }
    }

    void FixedUpdate()
    {
        Vector3 delta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.AddForce(delta * Speed, ForceMode.Force);
    }
}
