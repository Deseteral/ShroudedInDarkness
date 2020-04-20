using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;
    private int totalWoodCount = 12;

    private int ghostCount;
    private GameObject[] rageGhosts;

    // UI
    private GameObject blackScreen;
    private GameObject woodCountText;
    private GameObject allWoodText;
    private GameObject postIntroText;
    private GameObject thankYouText;
    private GameObject tutorialTitle;
    private GameObject tutorialText;
    private GameObject deathText;

    private GameObject campfireLight;
    private GameObject playerPointLight;
    private GameObject playerTorchlight;

    private TimeProgress deathScreenTimer = new TimeProgress();

    private GameObject player;
    private DialogSystem dialogSystem;

    private TimeProgress postIntroTimer = new TimeProgress();
    private TimeProgress beforeEndingTimer = new TimeProgress();

    private bool transitionToStageTwoComplete = false;
    private bool wasTutorialHidden = false;

    void Start()
    {
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;
        rageGhosts = GameObject.FindGameObjectsWithTag("GhostRage");
        foreach (GameObject g in rageGhosts)
        {
            g.SetActive(false);
        }
        ghostCount = rageGhosts.Length;
        Debug.Log("ghostCount " + ghostCount);

        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText");
        allWoodText = GameObject.Find("GameManager/Canvas/AllWoodText");
        deathText = GameObject.Find("GameManager/Canvas/DeathText");
        postIntroText = GameObject.Find("GameManager/Canvas/PostIntroText");
        thankYouText = GameObject.Find("GameManager/Canvas/ThankYouText");
        tutorialTitle = GameObject.Find("GameManager/Canvas/TutorialTitle");
        tutorialText = GameObject.Find("GameManager/Canvas/TutorialText");
        blackScreen = GameObject.Find("GameManager/Canvas/BlackScreen");

        campfireLight = GameObject.Find("Campfire/Point Light");
        playerPointLight = GameObject.Find("Player/TorchlightPointLight");
        playerTorchlight = GameObject.Find("Player/Torchlight");

        player = GameObject.Find("Player");
        dialogSystem = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();

    }

    void Update()
    {
        if (!wasTutorialHidden)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wasTutorialHidden = true;
                blackScreen.GetComponent<UIImageFader>().FadeOut(10f);
                tutorialTitle.GetComponent<UITextFader>().FadeOut(10f);
                tutorialText.GetComponent<UITextFader>().FadeOut(10f);

                // Kick off the gameplay
                dialogSystem.ChangeActive(true, "Intro");
            }
        }

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

        if (dialogSystem.HasFinished("BlueFire"))
        {
            // Activate new ghosts
            foreach (GameObject g in rageGhosts)
            {
                g.SetActive(true);
            }
            Debug.Log("Ghosts to kill " + ghostCount);
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
            blackScreen.GetComponent<UIImageFader>().FadeIn(3f);
            thankYouText.GetComponent<UIImageFader>().FadeIn(3f);
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
        if (CollectedWood == totalWoodCount && !transitionToStageTwoComplete) // Stage 1 complete
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

            // Kill old ghosts
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Ghost"))
            {
                Destroy(g.gameObject);
            }

            transitionToStageTwoComplete = true;
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
