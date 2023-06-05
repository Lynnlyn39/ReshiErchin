using UnityEngine;

/// <summary>
/// Fullscreen checkbox logic. Attached to the checkbox on value change event.
/// </summary>
public class FullScreenSelector : MonoBehaviour
{
    public void OnFullScreenToggleValueChanged(bool value)
    {
        if (value)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
}
