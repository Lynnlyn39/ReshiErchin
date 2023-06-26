using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents
{

    public event Action<string> onStartQuest;
    public void StartQuest(string id)
    {
        if(onStartQuest != null)
            onStartQuest(id);
    }

    public event Action<string> onAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
            onAdvanceQuest(id);
    }

    public event Action<string> onFinsihQuest;
    public void FinishQuest(string id)
    {
        if (onFinsihQuest != null)
            onFinsihQuest(id);
    }

    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
            onQuestStateChange(quest);
    }

    
}
