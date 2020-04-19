using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;
    private int totalWoodCount = 12;

    private int ghostCount;

    // UI
    private GameObject blackScreen;
    private GameObject woodCountText;
    private GameObject allWoodText;
    private GameObject deathText;

    private GameObject campfireLight;
    private GameObject playerPointLight;
    private GameObject playerTorchlight;

    private TimeProgress deathScreenTimer = new TimeProgress();

    private GameObject player;

    void Start()
    {
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;
        ghostCount = GameObject.FindGameObjectsWithTag("Ghost").Length;

        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText");
        allWoodText = GameObject.Find("GameManager/Canvas/AllWoodText");
        deathText = GameObject.Find("GameManager/Canvas/DeathText");
        blackScreen = GameObject.Find("GameManager/Canvas/BlackScreen");

        campfireLight = GameObject.Find("Campfire/Point Light");
        playerPointLight = GameObject.Find("Player/TorchlightPointLight");
        playerTorchlight = GameObject.Find("Player/Torchlight");

        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (deathScreenTimer.IsDone())
        {
            player.GetComponent<Player>().Respawn();
            deathText.GetComponent<UITextFader>().FadeOut(2f);
            blackScreen.GetComponent<UIImageFader>().FadeOut(2f);
            deathScreenTimer.Reset();
        }
    }

    public void PickUpWood()
    {
        CollectedWood += 1;
        woodCountText.GetComponent<Text>().text = "Collected wood: " + CollectedWood + "/" + totalWoodCount;

        if (CollectedWood == totalWoodCount)
        {
            allWoodText.GetComponent<TextFade>().Show(7f);
        }
        else
        {
            woodCountText.GetComponent<TextFade>().Show(4f);
        }
    }

    public void PlayerEnteredCampfire()
    {
        if (CollectedWood == totalWoodCount) // Stage 1 complete
        {
            Debug.Log("stage 1 complete");

            // Change fire color
            Color targetColor = new Color((65f / 255f), (20f / 255f), (121f / 255f), 1f);
            campfireLight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);
            playerPointLight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);
            playerTorchlight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);

            // Set player wand mode
            player.GetComponent<Player>().SetWandMode();
        }
    }

    public void PlayerDies()
    {
        blackScreen.GetComponent<UIImageFader>().FadeIn(3f);
        deathText.GetComponent<UITextFader>().FadeIn(3f);
        deathScreenTimer.Start(3f + 3f);
    }

    public void GhostDied()
    {
        ghostCount -= 1;

        if (ghostCount == 0) // Stage 2 complete
        {
            Debug.Log("stage 2 complete");
        }
    }
}
