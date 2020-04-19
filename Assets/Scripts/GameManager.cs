using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;
    private int totalWoodCount = 12;

    private GameObject woodCountText;

    void Start()
    {
        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText");
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;
    }

    void Update()
    {
    }

    public void PickUpWood()
    {
        CollectedWood += 1;

        // Show text
        woodCountText.GetComponent<Text>().text = "Collected wood: " + CollectedWood + "/" + totalWoodCount;
        woodCountText.GetComponent<TextFade>().Show(4f);
    }
}
