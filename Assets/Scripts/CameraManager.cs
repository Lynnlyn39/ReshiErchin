using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //private IA_ThirdPersonController _playerActionAsset;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _inventoryCamera;
    [SerializeField] private GameObject _inventoryCanvas;
    [SerializeField] private CinemachineVirtualCamera _vcamInterior;
    [SerializeField] private CinemachineVirtualCamera _vcamExterior;

    public Camera PlayerCamera { get => _playerCamera; set => _playerCamera = value; }
    public Camera InventoryCamera { get => _inventoryCamera; set => _inventoryCamera = value; }

    private void Start()
    {
        ActivatePlayerCamera();
    }

    public void ActivateInventoryCamera()
    {
        PlayerCamera.enabled = false;
        InventoryCamera.enabled = true;
        _inventoryCanvas.SetActive(true);
    }

    public void ActivatePlayerCamera()
    {
        InventoryCamera.enabled = false;
        _inventoryCanvas.SetActive(false);
        PlayerCamera.enabled = true;        
    }

    public void ActivateInteriorCamera()
    {
        if (PlayerCamera.enabled)
        {
            _vcamExterior.enabled = false;
            _vcamInterior.enabled = true;
        }
    }

    public void ActivateExteriorCamera()
    {
        if (PlayerCamera.enabled)
        {
            _vcamInterior.enabled = false;
            _vcamExterior.enabled = true;
        }
    }

}
