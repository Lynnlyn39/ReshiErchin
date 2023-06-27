using UnityEngine;
using UnityEngine.InputSystem;

public class BookManager : MonoBehaviour
{
    [SerializeField] GameObject _book;
    
    private IA_ThirdPersonController _bookActionAsset;

    private void Awake()
    {
        _bookActionAsset = new IA_ThirdPersonController();
        CloseBook();
    }

    private void OnEnable()
    {
        _bookActionAsset.Player.OpenBook.performed += OnOpenBook;
        _bookActionAsset.Book.Enable();
        _bookActionAsset.Player.Disable();
    }

    private void OnDisable()
    {
        _bookActionAsset.Book.Disable();
    }

    public void OnOpenBook(InputAction.CallbackContext context)
    {
        OpenBook();
    }


    public void OpenBook()
    {
        _book.SetActive(true);
        _bookActionAsset.Player.Disable();
        _bookActionAsset.Book.Enable();
    }

    public void CloseBook()
    {        
        _book.SetActive(false);
        _bookActionAsset.Book.Disable();
        _bookActionAsset.Player.Enable();
    }

}
