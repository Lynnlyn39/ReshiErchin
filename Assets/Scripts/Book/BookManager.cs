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

/*    public void OnOpenBook(InputAction.CallbackContext context)
    {
        _bookActionAsset.Player.Disable();
        _bookActionAsset.Book.Enable();        
        OpenBook();        
    }
*/
    public void OnCloseBook(InputAction.CallbackContext context)
    {
        CloseBook();
    }


    public void OpenBook()
    {
        //_bookActionAsset.Player.Disable();
        //_bookActionAsset.Book.Enable();
        _book.SetActive(true);
    }

    public void CloseBook()
    {
        _bookActionAsset.Book.Disable();
        _bookActionAsset.Player.Enable();
        _book.SetActive(false);
    }

}
