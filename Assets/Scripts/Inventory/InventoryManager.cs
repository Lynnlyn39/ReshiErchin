using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _inventorySet;
    [SerializeField] Inventory _inventory;
    [SerializeField] CameraManager _cameraManager;
    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        _inventorySet.SetActive(false);
    }

    public void OpenInventory()
    {
        if (_audioSource)
            _audioSource.Play();

        _inventorySet.SetActive(true);
        _cameraManager.ActivateInventoryCamera();
    }

    public void CloseInventory()
    {
        if (_audioSource)
            _audioSource.Play();

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
