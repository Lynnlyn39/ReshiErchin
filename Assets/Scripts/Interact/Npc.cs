using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private int _dialogueIndex = 0;
    [SerializeField] private List<TextAsset> inkJSON;

    public string interactionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        DialogueManager.instance.EnterDialogueMode(inkJSON[_dialogueIndex]);
        _dialogueIndex++;
        if(_dialogueIndex >= inkJSON.Count)
            _dialogueIndex=inkJSON.Count-1;
        return true;
    }
    
}
