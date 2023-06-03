using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject target;

    public string questTitle;

    public bool activeIfComplete;

    private bool initialCheck;

    // Start is called before the first frame update
    void Start()
    {
        target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheck && QuestManager.instance.quests.Count > 0)
        {
            initialCheck = true;

            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        // Show target if quest is completed
        if (QuestManager.instance.CheckIfComplete(questTitle))
        {
            target.SetActive(activeIfComplete);
        }
    }
}
