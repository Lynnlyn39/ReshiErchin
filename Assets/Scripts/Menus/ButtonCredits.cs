using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Menu button that shows the game credits panel
/// </summary>
public class ButtonCredits : Button
{
    [SerializeField] private GameObject _creditsPanelPrefab;
    [SerializeField] private GameObject _canvas;
    private GameObject _creditsPanel;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!_creditsPanel)
        {
            _creditsPanel = Instantiate(_creditsPanelPrefab, _canvas.transform);
        }

        if (_creditsPanel && !_creditsPanel.activeSelf)
        {
            _creditsPanel.SetActive(true);
        }
    }
}
