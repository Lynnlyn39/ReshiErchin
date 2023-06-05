using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Base class for menu buttons
///     Adds sfx on hover and on click
///     Adds virtual OnPointerClick method to override with logic
/// 
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip _sfxOnHover;
    [SerializeField] private AudioClip _sfxOnClick;

    private AudioSource _audioSource;

    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Override this method to implement the button logic
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{name} OnClick event NOT IMPLEMENTED.");
    }

    /// <summary>
    /// Play click sound
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_sfxOnClick)
        {
            _audioSource.clip = _sfxOnClick;
            _audioSource.Play();
        }
    }

    /// <summary>
    /// Play on hover sound
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Button::OnHover");
        if (_sfxOnHover)
        {
            _audioSource.clip = _sfxOnHover;
            _audioSource.Play();
        }
    }
}
