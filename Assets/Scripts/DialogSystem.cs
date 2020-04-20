using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject TextInstance;
    public GameObject BackgroundInstance;
    public GameObject PressSpaceInstance;

    public bool Active = false;

    private Text dialogText;

    private DialogLine[] activeDialog;
    private int index = 0;

    private Dictionary<string, DialogLine[]> dialogs;

    private Color pColor = new Color(255f / 255f, 67f / 255f, 59f / 255f);
    private Color xColor = new Color(59f / 255f, 154f / 255f, 255f / 255f);
    private Color noColor = new Color(0f, 0f, 0f, 0f);
    private Color bgColor = new Color(24f / 255f, 24f / 255f, 24f / 255f);
    private Color psColor = Color.white;

    private Player player;

    private bool finishedThisFrame = false;
    private string lastDialogId = "";

    void Start()
    {
        dialogText = TextInstance.GetComponent<Text>();

        dialogs = new Dictionary<string, DialogLine[]>();
        dialogs["Intro"] = Dialogs.Intro;
        dialogs["BlueFire"] = Dialogs.BlueFire;
        dialogs["Ending"] = Dialogs.Ending;

        player = GameObject.Find("Player").GetComponent<Player>();

        ChangeActive(false, "");
    }

    void Update()
    {
        if (finishedThisFrame) finishedThisFrame = false;
        if (!Active) return;

        if (Input.GetKeyDown(KeyCode.Space)) index += 1;
        if (index >= activeDialog.Length)
        {
            ChangeActive(false, "");
            finishedThisFrame = true;
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
            lastDialogId = nextDialog;
            player.MovementEnabled = false;
            BackgroundInstance.GetComponent<Image>().color = bgColor;
            PressSpaceInstance.GetComponent<Text>().color = psColor;
        }
        else
        {
            dialogText.color = noColor;
            activeDialog = null;
            player.MovementEnabled = true;
            BackgroundInstance.GetComponent<Image>().color = noColor;
            PressSpaceInstance.GetComponent<Text>().color = noColor;
        }
    }

    public bool HasFinished(string dialogId)
    {
        return (finishedThisFrame && lastDialogId == dialogId);
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
    public static DialogLine[] Intro = {
        new DialogLine("x", "I'm scared!"),
        new DialogLine("p", "Don't worry Little Brother, we are safe by the fire."),
        new DialogLine("x", "Are you sure? What's going to happen when the fire dies out?"),
        new DialogLine("p", "Don't worry. I won't allow this to happen."),
        new DialogLine("x", "But... but how?"),
        new DialogLine("x", "We have no wood here..."),
        new DialogLine("x", "Nowhere to hide..."),
        new DialogLine("p", "Listen, I'm going into the forest..."),
        new DialogLine("x", "No! Don't leave me here!"),
        new DialogLine("x", "I don't want to be alone! What if something happens to you!?"),
        new DialogLine("x", "What if one of those things gets to you!?"),
        new DialogLine("p", "I will be fine, I can outrun them."),
        new DialogLine("p", "On the way here, in the forest, I saw some of the old magic wood."),
        new DialogLine("p", "I'm going to find it."),
        new DialogLine("x", "Will they protect us?"),
        new DialogLine("p", "Yes, with a little bit of luck the fire from those magic branches might exile those monsters."),
        new DialogLine("x", "Exile them? Is it possible? We could go back to our old lifes?"),
        new DialogLine("p", "We shall see..."),
        new DialogLine("p", "I'm have to go now."),
        new DialogLine("x", "Promise me you will come back..."),
        new DialogLine("p", "I promise."),
    };

    public static DialogLine[] BlueFire = {
        new DialogLine("p", "I've found the magic wood!"),
        new DialogLine("x", "Look! The fire!"),
        new DialogLine("x", "Your torchlight!"),
        new DialogLine("p", "Impossible!"),
        new DialogLine("x", "What?"),
        new DialogLine("p", "The Old Fire Magic... I think I can now cast it with my torchlight..."),
        new DialogLine("p", "Maybe it's really possible to defend them!"),
        new DialogLine("x", "That would be amazing! But..."),
        new DialogLine("p", "What Little Brother?"),
        new DialogLine("x", "It means that you'll have to walk into the forest again..."),
        new DialogLine("p", "I will be back soon."),
        new DialogLine("p", "And this time I'll be back for good."),
    };

    public static DialogLine[] Ending = {
        new DialogLine("x", "The darkness! It's gone!"),
        new DialogLine("p", "And so are the monsters."),
        new DialogLine("x", "You... you saved us!"),
        new DialogLine("x", "We are safe now!"),
        new DialogLine("p", "Looks like it! We can live peacefully now!"),
        new DialogLine("x", "Thank you sister."),
    };
}
