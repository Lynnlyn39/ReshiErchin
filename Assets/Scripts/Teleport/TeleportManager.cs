using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] GameObject _player;

    public void TeleportPlayer(Teleporter teleporter)
    {
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = teleporter.Destination.position + Vector3.up;
        _player.transform.forward = teleporter.Destination.forward;
        _player.GetComponent<CharacterController>().enabled = true;
        Debug.Log("Player teleported");
    }
}
