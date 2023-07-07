using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public enum DestinationType { INTERIOR, EXTERIOR };
    
    [SerializeField] private Transform _destination;
    [SerializeField] private DestinationType _type;
    private TeleportManager _telManager;
    public Transform Destination => _destination;
    public DestinationType Type => _type;

    private void Start()
    {
        _telManager = FindObjectOfType<TeleportManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Teleporter.OnTriggerEnter {other.name}");
        //if (other.CompareTag("Player"))
            Teleport();
    }

    public void Teleport()
    {
        _telManager.TeleportPlayer(this);
    }
}
