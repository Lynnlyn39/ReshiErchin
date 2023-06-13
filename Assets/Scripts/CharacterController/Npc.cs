using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string interactionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Hello im an NPC");
        return true;
    }
}
