using System.Collections;
using UnityEngine;


/// <summary>
/// Allows loading scenes/levels in several ways, performing a crossfade transition between the scenes
/// </summary>
public class LevelLoader : MonoBehaviour
{    
    [SerializeField] private Scenes _nextScene;
    [SerializeField] private CrossFade _crossFade;

    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading || _crossFade.IsFading; 
    }

    private void Awake()
    {
        if (_crossFade && !_crossFade.gameObject.activeSelf)
        {
            _crossFade.gameObject.SetActive(true);
        }
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1.0f;
        }
        _isLoading = true;
    }

    public void LoadNextLevel()
    {
        LoadLevel(_nextScene);
    }

    public void LoadLevel(Scenes scene)
    {
        LoadLevelByIndex((int)scene);
    }

    private void LoadLevelByIndex(int index) 
    {
        _crossFade.CrossFadeOut();
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int index)
    {
        _isLoading = true;
        yield return new WaitWhile(() => _crossFade.IsFading);
        GameManager.ChangeScene(index);
        _isLoading = false;
    }
}
