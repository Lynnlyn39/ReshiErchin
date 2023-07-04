using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Menu button that shows the game credits panel
/// </summary>
public class ButtonToggleBook : Button
{
    [SerializeField] private BookManager _bookManager;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _bookManager.ToggleBook();
    }
}
