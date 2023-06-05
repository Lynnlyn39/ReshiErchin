using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets the global volume with the value passed from the UI slider on change event
/// </summary>
public class MasterVolumeSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = GameManager.CurrentVolume;
    }

    public void SetGlobalVolume(float volume)
    {
        GameManager.SetGlobalVolumeSetting(volume);
        _text.text = $"{Mathf.RoundToInt(volume * 100f)}%";
    }
}
