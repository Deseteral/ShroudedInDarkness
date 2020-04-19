using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject TextInstance;
    public bool Active = false;

    private Text dialogText;

    private DialogLine[] activeDialog;
    private int index = 0;

    private Dictionary<string, DialogLine[]> dialogs;

    private Color pColor = Color.red;
    private Color xColor = Color.blue;
    private Color noColor = new Color(0f, 0f, 0f, 0f);

    private Player player;

    void Start()
    {
        dialogText = TextInstance.GetComponent<Text>();

        dialogs = new Dictionary<string, DialogLine[]>();
        dialogs["TestDialog"] = Dialogs.TestDialog;

        player = GameObject.Find("Player").GetComponent<Player>();

        ChangeActive(true, "TestDialog");
    }

    void Update()
    {
        if (!Active) return;

        if (Input.GetKeyDown(KeyCode.Space)) index += 1;
        if (index >= activeDialog.Length)
        {
            ChangeActive(false, "");
            return;
        }

        dialogText.text = activeDialog[index].Text;
        dialogText.color = activeDialog[index].Speaker == "p" ? pColor : xColor;
    }

    public void ChangeActive(bool nextValue, string nextDialog)
    {
        Active = nextValue;
        if (Active)
        {
            index = 0;
            activeDialog = dialogs[nextDialog];
            player.MovementEnabled = false;
        }
        else
        {
            dialogText.color = noColor;
            activeDialog = null;
            player.MovementEnabled = true;
        }
    }
}

public struct DialogLine
{
    public string Speaker;
    public string Text;

    public DialogLine(string s, string t)
    {
        Speaker = s;
        Text = t;
    }
}

public static class Dialogs
{
    public static DialogLine[] TestDialog = {
        new DialogLine("p", "My line"),
        new DialogLine("x", "Oh no! That's terrible"),
        new DialogLine("p", "Hope this works!")
    };
}
