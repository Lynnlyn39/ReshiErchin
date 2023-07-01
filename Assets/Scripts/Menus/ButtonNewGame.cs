using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Menu button that stars a new game by loading the Prologue
/// </summary>
public class ButtonNewGame : Button
{
    [SerializeField] private LevelLoader _loader;

    protected override void Start()
    {
        base.Start();
        if (!_loader)
            _loader = FindObjectOfType<LevelLoader>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {        
        _loader.LoadNextLevel();
    }    
}
