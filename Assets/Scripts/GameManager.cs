using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;
    private int totalWoodCount = 12;

    private Text woodCountText;
    private float targetWoodCountTextOpacity = 0f;
    private float hideWoodCountTextAfter = float.MinValue;

    void Start()
    {
        woodCountText = GameObject.Find("GameManager/Canvas/WoodCountText").GetComponent<Text>();
        totalWoodCount = GameObject.FindGameObjectsWithTag("BlueLogs").Length;
    }

    void Update()
    {
        if (targetWoodCountTextOpacity != 0f && Time.timeSinceLevelLoad >= hideWoodCountTextAfter)
        {
            targetWoodCountTextOpacity = 0f;
        }

        EaseTextOpacity(woodCountText, targetWoodCountTextOpacity);
    }

    public void PickUpWood()
    {
        CollectedWood += 1;

        // Show text
        woodCountText.text = "Collected wood: " + CollectedWood + "/" + totalWoodCount;
        targetWoodCountTextOpacity = 1f;
        hideWoodCountTextAfter = (Time.timeSinceLevelLoad + 4f);
    }

    private void EaseTextOpacity(Text text, float targetOpacity)
    {
        float alpha = text.color.a;
        alpha += (targetOpacity - alpha) * 0.05f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
