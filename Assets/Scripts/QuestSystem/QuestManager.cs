using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> _questMap;

    private void Awake()
    {
        _questMap = CreateQuestMap();
        Debug.Log(_questMap.Count);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinsihQuest += FinishQuest;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinsihQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach (Quest quest in _questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }
    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        //check for requirements

        foreach (QuestInfoSO prerequisitesInfo in quest.info.questPrerequisites)
        {
            if(GetQuestById(prerequisitesInfo.id).state != QuestState.FINISHED){
                meetsRequirements = false;
            }
        }
        Debug.Log(meetsRequirements);
        return meetsRequirements;
    }
    private void Update()
    {
        foreach(Quest quest in _questMap.Values)
        {
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }
    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStepPrefab(transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }
    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStepPrefab(transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }
    private void ClaimRewards(Quest quest)
    {
        //give rewards
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuest = Resources.LoadAll("Quests", typeof(QuestInfoSO)).Cast<QuestInfoSO>().ToArray();

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfoSO in allQuest)
        {
            if(idToQuestMap.ContainsKey(questInfoSO.id))
            {
                //duplicated quest
            }
            idToQuestMap.Add(questInfoSO.id, new Quest(questInfoSO));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = _questMap[id];
        Debug.Log(quest +" " + _questMap[id]);
        if(quest == null)
        {
            Debug.LogError("ID not found in the Quest map: " + id);
        }
        return quest;
    }
}
