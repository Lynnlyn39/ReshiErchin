using UnityEngine;
using UnityEngine.InputSystem;

public class BookManager : MonoBehaviour
{
    [SerializeField] GameObject _book;
    
    private IA_ThirdPersonController _bookActionAsset;

    private void Awake()
    {
        _bookActionAsset = new IA_ThirdPersonController();        
    }

    private void OnEnable()
    {
        _bookActionAsset.Book.CloseBook.performed += OnCloseBook;                        
    }

    private void OnDisable()
    {
        _bookActionAsset.Book.Disable();
    }

    public void OnCloseBook(InputAction.CallbackContext context)
    {
        CloseBook();
    }

    public void OpenBook()
    {
        _book.SetActive(true);
    }

    public void CloseBook()
    {
        Debug.Log("CloseBook called");
        _book.SetActive(false);
        //_bookActionAsset.Book.Disable();
        //_bookActionAsset.Player.Enable();
    }

    public void ToggleBook()
    {
        if (_book.activeSelf)
        {
            CloseBook();
        } else
        {
            OpenBook();
        }
    }

}
