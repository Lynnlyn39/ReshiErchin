using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utility class to manage transversal elements like scene loading and global settings
/// </summary>
public static class GameManager
{
    private static float _globalVolumeSetting = 1f;
    private static float _currentVolume = _globalVolumeSetting;
    private static AudioSource[] _sources;
    private static PersistenceManager _pm;
    private const string SAVE_FILENAME = "ReshiErchin_savegame.json";

    /// <summary>
    /// The global setting for audio source volume
    /// </summary>
    public static float GlobalVolume
    {
        get => _globalVolumeSetting;
        set { _globalVolumeSetting = value; }
    }

    /// <summary>
    /// Returns the current volume. It can be different to the defaul global setting if modified by code.
    /// Use ResetVolume to match volume with the default global setting again.
    /// </summary>
    public static float CurrentVolume => _currentVolume;

    /// <summary>
    /// Returns an array with all available resolutions based on the current graphics adapter
    /// </summary>
    /// <returns></returns>
    public static Resolution[] GetScreenResolutions()
    {
        return Screen.resolutions;
    }

    /// <summary>
    /// Changes volume for all AudioSources but does not change the global default volume setting so it can be reset later.
    /// </summary>
    /// <param name="value"></param>
    public static void ChangeVolume(float value)
    {
        foreach (AudioSource audioSource in GetAudioSources())
        {
            if (audioSource)
            {
                audioSource.volume = Mathf.Clamp(value, 0f, 1f);
            }
        }
        _currentVolume = value;
    }

    /// <summary>
    /// Resets volume for all audio sources to the global setting
    /// </summary>
    public static void ResetVolume()
    {
        ChangeVolume(GlobalVolume);     
    }

    /// <summary>
    /// Changes the global volume setting to the specified value and resets current volume to the same value
    /// </summary>
    /// <param name="value">new volume setting</param>
    public static void SetGlobalVolumeSetting(float value)
    {
        GlobalVolume = value;
        ChangeVolume(GlobalVolume);
    }

    /// <summary>
    /// Loads the scene specified by index
    /// </summary>
    /// <param name="scene"></param>
    public static void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Debug.Log($"Scene={scene} loaded. Volume={GlobalVolume}.");
    }

    /// <summary>
    /// Quits the game and returns to desktop
    /// </summary>
    public static void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Returns an array with all audio sources in the scene
    /// </summary>
    /// <returns></returns>
    private static AudioSource[] GetAudioSources()
    {
        _sources = Object.FindObjectsOfType<AudioSource>();
        Debug.Log($"Sources found:{_sources.Length}");
        return _sources;
    }

    public static PersistenceManager PersistenceManager 
    {
        get
        {
            if (_pm == null)
            {
                _pm = Object.FindObjectOfType<PersistenceManager>().instance;
                _pm.Filename = SAVE_FILENAME;
            }
            return _pm;
        }
    }
}
