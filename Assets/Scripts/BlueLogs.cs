using UnityEngine;
using UnityEngine.UI;

public class BlueLogs : MonoBehaviour
{
    private GameObject player;
    public Text PickUpText;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Set text opacity
        float targetOpacity = (distanceToPlayer < 10f) ? 1f : 0f;
        float alpha = PickUpText.color.a;
        alpha += (targetOpacity - alpha) * 0.05f;
        PickUpText.color = new Color(PickUpText.color.r, PickUpText.color.g, PickUpText.color.b, alpha);
    }
}
