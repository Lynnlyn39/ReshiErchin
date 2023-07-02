using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //private IA_ThirdPersonController _playerActionAsset;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _inventoryCamera;
    [SerializeField] private GameObject _inventoryCanvas;

    public Camera PlayerCamera { get => _playerCamera; set => _playerCamera = value; }
    public Camera InventoryCamera { get => _inventoryCamera; set => _inventoryCamera = value; }

    private void Awake()
    {
        ActivatePlayerCamera();
        //_playerActionAsset = new IA_ThirdPersonController();
/*        _playerActionAsset.Player.Enable();
        _playerActionAsset.Book.Disable();
        _playerActionAsset.Inventory.Disable();
*/
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


}
