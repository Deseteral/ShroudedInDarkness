using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;
    private int totalWoodCount = 12;

    // UI
    private GameObject woodCountText;
    private GameObject allWoodText;
    private GameObject deathText;
    private GameObject blackScreen;

    private GameObject campfireLight;

    private TimeProgress deathScreenTimer = new TimeProgress();

    private GameObject player;

    void Start()
    {
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;

        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText");
        allWoodText = GameObject.Find("GameManager/Canvas/AllWoodText");
        deathText = GameObject.Find("GameManager/Canvas/DeathText");
        blackScreen = GameObject.Find("GameManager/Canvas/BlackScreen");

        campfireLight = GameObject.Find("Campfire/Point Light");

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
            Color targetColor = new Color((65f / 255f), (20f / 255f), (121f / 255f), 1f);
            campfireLight.GetComponent<LightColorChange>().ChangeColor(targetColor, 7f);
        }
    }

    public void PlayerDies()
    {
        blackScreen.GetComponent<UIImageFader>().FadeIn(3f);
        deathText.GetComponent<UITextFader>().FadeIn(3f);
        deathScreenTimer.Start(3f + 3f);
    }
}
