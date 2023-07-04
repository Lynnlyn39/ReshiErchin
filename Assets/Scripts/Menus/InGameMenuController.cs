using UnityEngine;

/// <summary>
/// Controls the in game menu. Should be attached to the ingame menu parent game object.
/// Pauses / Resumes the game.
/// </summary>
public class InGameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _inGameMenu;
    //[SerializeField] private MouseCursorSettings _mouseCursor;

    private void Start()
    {
        _inGameMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_inGameMenu.activeSelf)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }           
        }
    }

    public void ResumeGame()
    {
        _inGameMenu.SetActive(false);        
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        _inGameMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
