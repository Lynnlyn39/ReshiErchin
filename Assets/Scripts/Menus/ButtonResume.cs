using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// In game menu button that closes the menu and resumes the game.
/// </summary>
public class ButtonResume : Button
{
    [SerializeField] private InGameMenuController _inGameMenuController;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        _inGameMenuController.ResumeGame();
    }

}
