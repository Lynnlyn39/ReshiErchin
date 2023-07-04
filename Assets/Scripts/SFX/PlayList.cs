using System.Collections;
using UnityEngine;

/// <summary>
/// Manages main music themes and battle music
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayList : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    [SerializeField] SFXSetSO _sfxBattleMusic;
    private SFXPlayer _sfxPlayer;        
    private int _index = 0;
    private float _initialVolume = 1f;
    private float _fadeTime = 1f;
    //private Player _player;
    //private bool _isBattle;

    // Start is called before the first frame update
    void Start()
    {
        if (!_sfxPlayer)
        {
            _sfxPlayer = GetComponent<SFXPlayer>();
        }
        
        if (_sfxPlayer)
        {
            _initialVolume = _sfxPlayer.Volume;
            _sfxPlayer.Volume = 0f;
        }
       // _player = FindObjectOfType<Player>();
        //_isBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!_isBattle && _player.IsUnderAttack)
        {
            _isBattle = true;
            StartCoroutine(PlayBattleMusic());
        }*/

        if (!_sfxPlayer.IsPlaying)
        {
            //_isBattle = false;            
            _sfxPlayer.Volume = 0f;
            _sfxPlayer.Play(GetNextClip());
            StartCoroutine(FadeIn());
        }
    }

    /// <summary>
    /// When in combat, plays a random battle music
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayBattleMusic()
    {
        _sfxPlayer.Stop();
        _sfxPlayer.Play(_sfxBattleMusic);
        yield return new WaitWhile(() => (/*_player.IsUnderAttack ||*/ _sfxPlayer.IsPlaying));
        if (_sfxPlayer.IsPlaying)
            StartCoroutine(FadeOut());
    }

    /// <summary>
    /// Fade in sound
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float value = Mathf.Lerp(0f, _initialVolume, elapsedTime / _fadeTime);
            _sfxPlayer.Volume = value;

            yield return null;
        }
    }

    /// <summary>
    /// Fade out sound
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float value = Mathf.Lerp(_initialVolume, 0, elapsedTime / _fadeTime);
            _sfxPlayer.Volume = value;

            yield return null;
        }
    }

    private AudioClip GetNextClip()
    {
        return _clips[(_index++) % _clips.Length];
    }
}
