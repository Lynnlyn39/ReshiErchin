using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestInfoSO info;

    public QuestState state;
    public int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists() 
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStepPrefab(Transform parent)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parent).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id);
        }
    }
    public GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        return questStepPrefab;
    }
}
