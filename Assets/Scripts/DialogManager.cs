using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public GameObject nameBox;
    public GameObject dialogBox;

    private string speakerText;
    private string[] dialogLines;
    private int currentLine;
    private bool knowName;

    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogBox.SetActive(false);
        nameBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1") || Input.GetKeyDown(KeyCode.E))
            {
                if (currentLine >= dialogLines.Length)
                {
                    nameBox.SetActive(false);
                    dialogBox.SetActive(false);
                    GameManager.instance.dialogActive = false;
                }
                else if ((currentLine <= dialogLines.Length))
                {
                    SetDialogAndNameText(dialogLines[currentLine]);
                }
                currentLine++;
            }
        }
    }

    public void ShowDialog(string name, string[] lines, bool isNPC)
    {
        speakerText = name;
        dialogLines = lines;

        currentLine = 0;
        dialogText.text = dialogLines[0];

        GameManager.instance.dialogActive = true;
        dialogBox.SetActive(true);
        nameBox.SetActive(isNPC);
    }

    private void SetDialogAndNameText(string dialog)
    {
        if (dialog.StartsWith("-p"))
        {
            nameText.text = "Player";
            dialogText.text = dialog.Replace("-p", "");
        }
        else
        {
            nameText.text = speakerText;
            dialogText.text = dialog;
        }
    }
}
