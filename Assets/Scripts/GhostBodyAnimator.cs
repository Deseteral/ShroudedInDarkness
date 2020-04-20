using UnityEngine;

public class GhostBodyAnimator : MonoBehaviour
{
    public float Speed = 1f;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float a = (Time.deltaTime * Speed) % 1f;
        material.mainTextureOffset += new Vector2(a, a);
    }
}
