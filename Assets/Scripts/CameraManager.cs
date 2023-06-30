using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _inventoryCamera;
    [SerializeField] private GameObject _inventoryCanvas;

    private void Awake()
    {
        ActivatePlayerCamera();
    }
    public void ActivateInventoryCamera()
    {
        _playerCamera.enabled = false;
        _inventoryCamera.enabled = true;
        _inventoryCanvas.SetActive(true);
    }

    public void ActivatePlayerCamera()
    {
        _inventoryCamera.enabled = false;
        _inventoryCanvas.SetActive(false);
        _playerCamera.enabled = true;        
    }
}
