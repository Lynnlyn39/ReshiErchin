using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject requierementsNotMetToSartIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject requirementsNotMetToFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requierementsNotMetToSartIcon.SetActive(false); 
        canStartIcon.SetActive(false);
        requierementsNotMetToSartIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch(newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) requierementsNotMetToSartIcon.SetActive(true);
                break;
            case QuestState.CAN_START: 
                if(startPoint) canStartIcon.SetActive(true);
                break;
            case QuestState.IN_PROGRESS: 
                if(finishPoint) requirementsNotMetToFinishIcon.SetActive(true);
                break;
            case QuestState.CAN_FINISH: 
                if(finishPoint) canFinishIcon.SetActive(true);
                break;
            case QuestState.FINISHED: 
                break;
            default:
                Debug.LogWarning("Quest state not found: " + newState);
                break;
        }
    }
}
