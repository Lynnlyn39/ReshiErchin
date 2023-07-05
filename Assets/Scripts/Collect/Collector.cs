using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private SFXSetSO _sfxCollect;
    [SerializeField] private SFXPlayer _sfxPlayer;


    private void Start()
    {
        if (!_sfxPlayer)
            _sfxPlayer = GetComponent<SFXPlayer>();

        if (!_inventory)
            _inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        InventoryItem item = other.GetComponent<InventoryItem>();
        if (item)
        {
            if (_inventory.AddItem(item.Data))
            {
                _sfxPlayer.PlayRandomPitch(_sfxCollect);
                Destroy(item.gameObject);
            }            
        }
    }

}
