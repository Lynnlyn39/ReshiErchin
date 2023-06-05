using UnityEngine.EventSystems;

/// <summary>
/// Generic "close" button used in all panels to close them
/// </summary>
public class ButtonClosePanel : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.gameObject.SetActive(false);
    }
}
