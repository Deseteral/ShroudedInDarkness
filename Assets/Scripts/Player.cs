using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;
    public float DamageAmount = 0.1f;
    public GameObject Bullet;

    private GameObject torchlightCollider;
    private MeshCollider torchlightColliderActualColliderThatCollides;
    private Light torchlightLight;

    public bool IsTorchlightActive = false;
    public float TorchlightLengthActive = 10f;
    public float TorchlightLength = 4f;

    public float Health = 1.0f;

    private new Rigidbody rigidbody;
    private Volume globalVolume;

    public float AttackTime = 1f;
    public float AttackRechargeTime = 3f;
    private float canAttackAfter = float.MinValue;
    private float noLongerAttackingAfter = float.MinValue;

    private Image attackTimerProgress;

    private GameObject torchlightPivot;
    private float torchlightPivotTargetRotation = 0f;

    private GameManager gameManager;

    private Vector3 spawnPoint;
    private bool deathFlag = false;

    private bool wandMode = false;

    public bool MovementEnabled = true;

    private AudioSource magicSoundSource;

    void Start()
    {
        spawnPoint = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        torchlightLight = GameObject.Find("Player/Torchlight").GetComponent<Light>();
        attackTimerProgress = GameObject.Find("Player/Canvas/AttackTimerProgress").GetComponent<Image>();
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        torchlightCollider = GameObject.Find("Player/TorchlightCollider");
        torchlightColliderActualColliderThatCollides = torchlightCollider.GetComponent<MeshCollider>();
        torchlightPivot = GameObject.Find("Player/TorchlightPivot");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        magicSoundSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotation
        // TODO: Controller support
        Vector3 diff = transform.position - GetMouseOnTerrain();
        float direction = Mathf.Atan2(diff.z, -diff.x) * Mathf.Rad2Deg;
        if (MovementEnabled) transform.rotation = Quaternion.Euler(0f, direction, 0f);

        // Atack logic
        if (Time.timeSinceLevelLoad >= canAttackAfter) // Can attack
        {
            attackTimerProgress.fillAmount = 0f;

            if (MovementEnabled && Input.GetMouseButton(0))
            {
                IsTorchlightActive = true;
                canAttackAfter = (Time.timeSinceLevelLoad + AttackTime + AttackRechargeTime);
                noLongerAttackingAfter = (Time.timeSinceLevelLoad + AttackTime);
                torchlightPivotTargetRotation = -112f;

                if (wandMode)
                {
                    Vector3 bpos = transform.position;
                    bpos.y += 0.5f;
                    bpos += (transform.right * 2f);
                    GameObject b = Instantiate(Bullet, bpos, transform.rotation);
                    magicSoundSource.Play();
                }
            }
        }
        else
        {
            // Display attack timer progress
            float v = (canAttackAfter - Time.timeSinceLevelLoad) / (AttackTime + AttackRechargeTime);
            attackTimerProgress.fillAmount = Mathf.Clamp(v, 0f, 1f);
        }

        if (IsTorchlightActive && (Time.timeSinceLevelLoad >= noLongerAttackingAfter)) // Is no longer attacking
        {
            IsTorchlightActive = false;
            torchlightPivotTargetRotation = 0f;
        }

        // Animate torchlight
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, torchlightPivotTargetRotation, 0f));
        torchlightPivot.transform.localRotation = Quaternion.RotateTowards(torchlightPivot.transform.localRotation, targetRotation, 60f * Time.deltaTime);

        // Activate collider
        torchlightColliderActualColliderThatCollides.enabled = IsTorchlightActive;

        // Torchlight
        Vector3 torchColliderScale = torchlightCollider.transform.localScale;
        Vector3 torchColliderPosition = torchlightCollider.transform.localPosition;
        {
            float actualLightLength = IsTorchlightActive ? TorchlightLengthActive : TorchlightLength;

            torchColliderScale.y = actualLightLength;
            torchColliderPosition.x = (actualLightLength / 2) + 2;
            torchlightLight.range = (actualLightLength * 4);

            torchlightLight.intensity += ((IsTorchlightActive ? 80f : 15f) - torchlightLight.intensity) * 0.1f;
        }
        torchlightCollider.transform.localScale = torchColliderScale;
        torchlightCollider.transform.localPosition = torchColliderPosition;

        // Set vignette to health value
        SetVignetteIntensity(1f - Health);

        // TODO: Check health and die
        if (!deathFlag && Health <= 0f)
        {
            deathFlag = true;
            gameManager.PlayerDies();
        }

        // Restore health
        ChangeHealth(DamageAmount * 0.5f);
    }

    void FixedUpdate()
    {
        if (MovementEnabled)
        {
            Vector3 delta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 forceVec = (Quaternion.Euler(0, 45, 0) * (delta * Speed));
            rigidbody.AddForce(forceVec, ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "InnerCampfireTrigger")
        {
            gameManager.PlayerEnteredCampfire();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.name == "DamageCollider")
        {
            ChangeHealth(-DamageAmount);
        }
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

    private void ChangeHealth(float diff)
    {
        Health += diff;
        if (Health > 1f) Health = 1f;
    }

    public void Respawn()
    {
        Health = 1f;
        transform.position = spawnPoint;
        deathFlag = false;
    }

    public void SetWandMode()
    {
        wandMode = true;

        torchlightCollider.SetActive(false);
        torchlightColliderActualColliderThatCollides.enabled = false;

        AttackTime = 0.25f;
        AttackRechargeTime = 0.25f;
    }
}
