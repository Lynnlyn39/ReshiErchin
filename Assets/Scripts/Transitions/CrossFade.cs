using System.Collections;
using UnityEngine;

/// <summary>
/// Transition between scenes
/// It is done by means of changing CanvasGroup alpha channel
/// </summary>
public class CrossFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeTime;    
    [SerializeField] private bool _fadeInMusic;
    
    private bool _isFading;

    public bool IsFading => _isFading;


    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _isFading = false;
    }

    private void Start()
    {
        CrossFadeIn();
    }

    public void CrossFadeIn()
    {
        if (!IsFading)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void CrossFadeOut()
    {
        if (!IsFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        if (!_isFading)
        {
            Debug.Log($"FadeIn Start");
            //GameManager.ResetVolume();
            _isFading = true;
            _canvasGroup.alpha = 1.0f;
            if (_fadeInMusic)
            {
                _audioSource.volume = 0;
                _audioSource.Play();
            }            
            float elapsedTime = 0f;
            while (elapsedTime < _fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float value = Mathf.Lerp(0f, 1.0f, elapsedTime / _fadeTime);
                _canvasGroup.alpha = 1 - value;
                if (_fadeInMusic)
                {
                    _audioSource.volume = value * GameManager.CurrentVolume;
                }
                yield return null;
            }
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            if (_fadeInMusic)
            {
                _audioSource.volume = GameManager.CurrentVolume;
            }            
            _isFading = false;
            Debug.Log($"FadeIn End");
        }
    }

    private IEnumerator FadeOut()
    {
        if (!_isFading)
        {
            Debug.Log($"FadeOut Start");
            _isFading = true;
            float elapsedTime = 0f;
            float value;
            while (elapsedTime < _fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                value = Mathf.Lerp(1.0f, 0f, elapsedTime / _fadeTime);
                _canvasGroup.alpha = 1 - value;                
                _audioSource.volume = value * GameManager.CurrentVolume;
                yield return null;
            }
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _audioSource.volume = 0.0f;
            _isFading = false;
            Debug.Log($"FadeOut End");
        }
    }
}
