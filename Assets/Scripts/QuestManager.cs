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
        Quest quest = new Quest(title, description, false);

        if (!quests.Contains(quest))
        {
            quests.Add(quest);
        }
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

    public void SaveQuestData()
    {
        foreach (Quest quest in quests)
        {
            if (quest.completed)
            {
                PlayerPrefs.SetInt("Quest_" + quest.title, 1);
            }
            else
            {
                PlayerPrefs.SetInt("Quest_" + quest.title, 0);
            }
        }
    }

    public void LoadQuestData()
    {
        foreach (Quest quest in quests)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey("Quest_" + quest.title))
            {
                valueToSet = PlayerPrefs.GetInt("Quest_" + quest.title);
            }
            if (valueToSet == 0)
            {
                quest.completed = false;
            }
            else
            {
                quest.completed = true;
            }
        }
    }
}
