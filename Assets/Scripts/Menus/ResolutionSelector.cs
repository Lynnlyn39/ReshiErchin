using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Allows changing the screen resolution with the one selected in the dropdown
/// </summary>
public class ResolutionSelector : MonoBehaviour
{ 
    private TMP_Dropdown _dropdown;
    List<Resolution> _resolutions;

    // Initialize dropdown with the available resolutions and the current resolution
    private void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _resolutions = new List<Resolution>();
        List<string> data = new List<string>();

        foreach (Resolution r in Screen.resolutions)
        {
            _resolutions.Add(r);
            data.Add(r.ToString());
        }
        _dropdown.AddOptions(data);
        //Debug.Log($"Current resolution: {Screen.currentResolution}");
        _dropdown.captionText.text = Screen.currentResolution.ToString();
    }

    /// <summary>
    /// Sets the selected screen resolution
    /// </summary>
    /// <param name="value"></param>
    public void OnDropdownValueChanged(int value)
    {
        if (_dropdown && _dropdown.captionText)
        {
            _dropdown.captionText.text = _dropdown.options[value].text;
            //Debug.Log($"{name} Value changed to {_dropdown.captionText.text}");
            Resolution r = _resolutions[value];
            Screen.SetResolution(r.width, r.height, false);
        }
    }
}
