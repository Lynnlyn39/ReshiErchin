using UnityEngine.EventSystems;


namespace Project3D {
/// <summary>
/// Menu button that quits the game.
/// </summary>
public class ButtonQuit : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.QuitGame();
    }
}
}