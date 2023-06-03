using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [Header("Dialogs")]
    public string name;
    public string[] lines;

    [Header("Conditionals")]
    public bool isNPC;

    [Header("Mark Quest")]
    public string questToMarkTitle;
    public bool shouldActivateQuest;
    public bool markComplete;

    [Header("Create Quest")]
    public string title;
    public string description;
    public bool shouldCreateQuest;

    private bool canActivate;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (
            canActivate
            && Input.GetKeyDown(KeyCode.E)
            && !DialogManager.instance.dialogBox.activeInHierarchy
        )
        {
            DialogManager.instance.ShowDialog(name, lines, isNPC);
            DialogManager.instance.ShouldActivateQuestAtEnd(title, markComplete);
            if (shouldCreateQuest)
            {
                DialogManager.instance.CreateQuestAtEnd(title, description);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canActivate = false;
        }
    }
}
