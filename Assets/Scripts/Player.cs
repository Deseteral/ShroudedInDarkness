using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;

    public GameObject TorchlightCollider;
    public GameObject Torchlight;
    private Light torchlightLight;

    public bool IsTorchlightActive = false;
    public float TorchlightLengthActive = 10f;
    public float TorchlightLength = 4f;

    public float Health = 1.0f;

    private new Rigidbody rigidbody;
    private Volume globalVolume;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        torchlightLight = Torchlight.GetComponent<Light>();
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }

    void Update()
    {
        IsTorchlightActive = Input.GetMouseButton(0);

        // Rotation
        // TODO: Controller support
        Vector3 diff = transform.position - GetMouseOnTerrain();
        float direction = Mathf.Atan2(diff.z, -diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, direction, 0f);

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

        // Set vignette to health value
        SetVignetteIntensity(1f - Health);
    }

    void FixedUpdate()
    {
        Vector3 delta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.AddForce(delta * Speed, ForceMode.Force);
    }

    private Vector3 GetMouseOnTerrain()
    {
        const int terrainLayerMask = (1 << 8);
        RaycastHit hit;
        return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, terrainLayerMask)
            ? hit.point
            : Vector3.zero;
    }

    private void SetVignetteIntensity(float value)
    {
        Vignette vignette;
        if (globalVolume.profile.TryGet(out vignette)) vignette.intensity.value = value;
    }
}
