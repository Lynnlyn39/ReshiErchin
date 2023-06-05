using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// In game only menu button that leads back to the main menu
/// </summary>
public class ButtonMainMenu : Button
{
    private LevelLoader _loader;

    protected override void Start()
    {
        base.Start();
        _loader = FindObjectOfType<LevelLoader>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {        
        _loader.LoadLevel(Scenes.MAINMENU);
    }

}
