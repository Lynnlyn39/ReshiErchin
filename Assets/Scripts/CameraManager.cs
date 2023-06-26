using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _inventoryCamera;

    private void Awake()
    {
        ActivatePlayerCamera();
    }
    public void ActivateInventoryCamera()
    {
        _playerCamera.enabled = false;
        _inventoryCamera.enabled = true;
    }

    public void ActivatePlayerCamera()
    {
        _inventoryCamera.enabled = false;
        _playerCamera.enabled = true;        
    }
}
