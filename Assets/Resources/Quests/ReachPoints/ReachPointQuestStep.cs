using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPointQuestStep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
}
