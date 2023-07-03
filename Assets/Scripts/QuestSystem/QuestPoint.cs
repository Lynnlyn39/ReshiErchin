using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoPoint;

    [Header("Config")]
    [SerializeField] private bool _startPoint = true;
    [SerializeField] private bool _finishPoint = true;

    private bool _playerIsNear = false;
    private string _questId;
    private QuestState _currentQuestState;

    private QuestIcon _questIcon;

    private IA_ThirdPersonController _playerActionAsset;

    private void Awake()
    {
        _questId = questInfoPoint.id;
        _questIcon = GetComponentInChildren<QuestIcon>();
        _playerActionAsset = new IA_ThirdPersonController();
    }

    private void OnEnable()
    {
        _playerActionAsset.Player.Interact.performed += SubmitPressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        _playerActionAsset.Player.Interact.performed -= SubmitPressed;
    }

    private void SubmitPressed(InputAction.CallbackContext context)
    {
        Debug.Log("tried to accept quest");
        if(!_playerIsNear)
        {
            return;
        }
        if(_currentQuestState.Equals(QuestState.CAN_START) && _startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(_questId);
            Debug.Log("quest started" + _questId);
        }
        else if(_currentQuestState.Equals(QuestState.CAN_FINISH) && _finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(_questId);
            Debug.Log("quest finished" + _questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.Equals(_questId))
        {
            _currentQuestState = quest.state;
            _questIcon.SetState(_currentQuestState, _startPoint, _finishPoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = false;
        }
    }
}
