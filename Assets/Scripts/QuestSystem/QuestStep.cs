using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool _isFinsihed = false;
    private string _questId;

    public void InitializeQuestStep(string questId)
    {
        _questId = questId;
    }
    protected void FinishQuestStep()
    {
        if (!_isFinsihed)
        {
            _isFinsihed=true;
            GameEventsManager.instance.questEvents.AdvanceQuest(_questId);
            Destroy(gameObject);
        }
    }
}
