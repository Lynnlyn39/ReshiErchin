using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSaveGame : Button
{
    //private Player _player;
    UnityEngine.UI.Button _button; 

    protected override void Start()
    {
        base.Start();
      //  _player = FindObjectOfType<Player>();
        _button = GetComponent<UnityEngine.UI.Button>();
    }

    private void OnEnable()
    {
        //_button.enabled = !_player.IsUnderAttack && !_player.Target;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Saving game...");
        GameManager.PersistenceManager.SaveGame();
    }
}
