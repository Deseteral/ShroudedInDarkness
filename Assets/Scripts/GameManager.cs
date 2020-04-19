using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;
    private int totalWoodCount = 12;
    private int deadWood = 0;

    // UI
    private GameObject woodCountText;
    private GameObject allWoodText;
    private GameObject blackScreen;

    private GameObject campfireLight;

    void Start()
    {
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;

        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText");
        allWoodText = GameObject.Find("GameManager/Canvas/AllWoodText");
        blackScreen = GameObject.Find("GameManager/Canvas/BlackScreen");

        campfireLight = GameObject.Find("Campfire/Point Light");
    }

    void Update()
    {
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
        deadWood = CollectedWood;
        CollectedWood = 0;
        blackScreen.GetComponent<UIImageFader>().FadeOut(2f);
    }
}
