using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField] GameObject _book;

    public void OpenBook()
    {
        _book.SetActive(true);
    }

    public void CloseBook()
    {
        _book.SetActive(false);
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
