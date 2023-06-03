using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    public string questToMarker;
    public bool markComplete;

    private bool canMark;

    public bool markOnEnter;
    public bool deactivateOnMarking;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (canMark)
        {
            canMark = false;
            MarkQuest();
        }
    }

    public void MarkQuest()
    {
        if (markComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMarker);
        }
        else
        {
            QuestManager.instance.MarkQuestIncomplete(questToMarker);
        }

        gameObject.SetActive(!deactivateOnMarking);
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            if (markOnEnter)
            {
                MarkQuest();
            }
            else
            {
                canMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canMark = false;
        }
    }
}
