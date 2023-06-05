using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Menu button that opens the settings panel.
/// </summary>
public class ButtonSettings : Button
{
    [SerializeField] private GameObject _settingsPanelPrefab;    
    [SerializeField] private Canvas _canvas;
    private GameObject _settingsPanel;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!_settingsPanel)
        {
            _settingsPanel = Instantiate(_settingsPanelPrefab, _canvas.transform);                        
        } 
        
        if (_settingsPanel && !_settingsPanel.activeSelf) {
            _settingsPanel.SetActive(true);
        }
    }
}
