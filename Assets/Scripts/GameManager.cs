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
    private GameObject postIntroText;
    private GameObject deathText;

    private GameObject campfireLight;
    private GameObject playerPointLight;
    private GameObject playerTorchlight;

    private TimeProgress deathScreenTimer = new TimeProgress();

    private GameObject player;
    private DialogSystem dialogSystem;

    private TimeProgress postIntroTimer = new TimeProgress();
    private TimeProgress beforeEndingTimer = new TimeProgress();

    private bool transitionToStageOneComplete = false;

    void Start()
    {
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;
        ghostCount = GameObject.FindGameObjectsWithTag("GhostRage").Length;
        Debug.Log("Ghosts to kill " + ghostCount);

        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText");
        allWoodText = GameObject.Find("GameManager/Canvas/AllWoodText");
        deathText = GameObject.Find("GameManager/Canvas/DeathText");
        postIntroText = GameObject.Find("GameManager/Canvas/PostIntroText");
        blackScreen = GameObject.Find("GameManager/Canvas/BlackScreen");

        campfireLight = GameObject.Find("Campfire/Point Light");
        playerPointLight = GameObject.Find("Player/TorchlightPointLight");
        playerTorchlight = GameObject.Find("Player/Torchlight");

        player = GameObject.Find("Player");
        dialogSystem = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();

        // Kick off the gameplay
        dialogSystem.ChangeActive(true, "Intro");
        blackScreen.GetComponent<UIImageFader>().FadeOut(5f);
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

        if (dialogSystem.HasFinished("Intro"))
        {
            postIntroText.GetComponent<UITextFader>().FadeIn(2f);
            postIntroTimer.Start(7f);
        }
        if (postIntroTimer.IsDone())
        {
            postIntroText.GetComponent<UITextFader>().FadeOut(2f);
            postIntroTimer.Reset();
        }

        // Ending sequence
        if (beforeEndingTimer.IsDone())
        {
            // At this point the screen is black
            GameObject.Find("Sun").GetComponent<Light>().intensity = 1f;
            GameObject.Find("Campfire/Point Light").SetActive(false);
            GameObject.Find("Campfire/Particle System").SetActive(false);
            player.GetComponent<Player>().Respawn();
            player.GetComponent<Player>().MovementEnabled = false;

            dialogSystem.ChangeActive(true, "Ending");

            beforeEndingTimer.Reset();
            blackScreen.GetComponent<UIImageFader>().FadeOut(3f);
        }

        if (dialogSystem.HasFinished("Ending"))
        {
            player.GetComponent<Player>().MovementEnabled = false;
            blackScreen.GetComponent<UIImageFader>().FadeIn(1f);
            // TODO: Thank you screen
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
        if (CollectedWood == totalWoodCount && !transitionToStageOneComplete) // Stage 1 complete
        {
            Debug.Log("stage 1 complete");
            dialogSystem.ChangeActive(true, "BlueFire");

            // Change fire color
            Color targetColor = new Color((65f / 255f), (20f / 255f), (121f / 255f), 1f);
            campfireLight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);
            playerPointLight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);
            playerTorchlight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);

            // Set player wand mode
            player.GetComponent<Player>().SetWandMode();

            transitionToStageOneComplete = true;
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
            blackScreen.GetComponent<UIImageFader>().FadeIn(3f);
            beforeEndingTimer.Start(3f + 1f);
        }
    }
}
