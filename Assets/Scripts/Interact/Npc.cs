using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private TextAsset inkJSON;

    public string interactionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        DialogueManager.instance.EnterDialogueMode(inkJSON);
        return true;
    }
}
