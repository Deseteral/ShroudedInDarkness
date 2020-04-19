using UnityEngine;
using UnityEngine.UI;

public class BlueLogs : MonoBehaviour
{
    private GameObject player;
    public Text PickUpText;

    private GameManager gameManager;

    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool isPlayerInRange = (distanceToPlayer < 10f);

        // Set text opacity
        float targetOpacity = isPlayerInRange ? 1f : 0f;
        EaseTextOpacity(PickUpText, targetOpacity);

        // Collect wood
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.PickUpWood();
            Destroy(this.gameObject);
        }
    }

    private void EaseTextOpacity(Text text, float targetOpacity)
    {
        float alpha = text.color.a;
        alpha += (targetOpacity - alpha) * 0.05f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
