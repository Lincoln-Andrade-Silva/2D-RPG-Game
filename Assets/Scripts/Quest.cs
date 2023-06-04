using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string title;
    public string description;
    public bool completed;
    public bool hide;

    public Quest(string t, string d, bool c)
    {
        title = t;
        description = d;
        completed = c;
    }

    public void MarkQuest(Quest quest, bool completed)
    {
        quest.completed = completed;
    }
}
