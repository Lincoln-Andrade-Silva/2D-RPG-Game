using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<Quest> quests = new List<Quest>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update() { }

    public void CreateQuest(string title, string description)
    {
        quests.Add(new Quest(title, description, false));
    }

    public Quest FindQuest(string title)
    {
        Quest quest = quests.Find(x => x.title == title);
        return quest;
    }

    public bool CheckIfComplete(string title)
    {
        if (title != null)
        {
            Debug.Log(title);
            Quest quest = FindQuest(title);
            return quest.completed;
        }

        return false;
    }

    public void MarkQuestComplete(string title)
    {
        if (title != null)
        {
            Quest quest = FindQuest(title);
            quest.completed = true;
            UpdateLocalQuestObjects();
        }
    }

    public void MarkQuestIncomplete(string title)
    {
        Quest quest = FindQuest(title);
        if (quest != null)
        {
            quest.completed = false;
            UpdateLocalQuestObjects();
        }
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if (questObjects.Length > 0)
        {
            for (int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }
}
