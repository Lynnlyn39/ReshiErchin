using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _inventorySet;
    [SerializeField] Inventory _inventory;
    [SerializeField] CameraManager _cameraManager;

    public void OpenInventory()
    {
        _inventorySet.SetActive(true);
        _cameraManager.ActivateInventoryCamera();
    }

    public void CloseInventory()
    {
        _inventorySet.SetActive(false);
        _cameraManager.ActivatePlayerCamera();
    }

    public void ToggleInventory()
    {
        if (_inventorySet.activeSelf)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void AddToMix()
    {
        if (_inventorySet.activeSelf)
            _inventory.AddToMix();
    }

    public void ResetMix()
    {
        if (_inventorySet.activeSelf)
            _inventory.ResetMix();
    }
}
