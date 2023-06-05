using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonContinueGame : Button
{
    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Loading game...");
        GameManager.PersistenceManager.LoadGame();
    }
}
