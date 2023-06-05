using System.Collections;
using UnityEngine;

/// <summary>
/// Utility class to play random clips from a list defined in a SFXSetSO ScriptableObject.
/// It can be at random pitch also to add more variety.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    public bool IsPlaying => _audioSource.isPlaying;

    public float Volume {
        get => _audioSource.volume;
        set { _audioSource.volume = value; }
    }

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(SFXSetSO sfx)
    {
        if (sfx)
        {
            _audioSource.PlayOneShot(GetRandomAudioClip(sfx));
        }
    }

    public void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void Play(AudioClip clip, float volume)
    {
        float vol = _audioSource.volume;
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(clip);
        StartCoroutine(ResetVolume(vol));
    }


    public void Play(SFXSetSO sfx, float volume)
    {
        float vol = _audioSource.volume;
        _audioSource.volume = volume;
        Play(sfx);
        StartCoroutine(ResetVolume(vol));
    }

    // Resets volume back to default once finished playing current clip
    IEnumerator ResetVolume(float volume)
    {
        yield return new WaitUntil(() => !IsPlaying);
        _audioSource.volume = volume;
    }
    

    public void PlayRandomPitch(SFXSetSO sfx)
    {
        if (sfx && sfx.AudioClips.Length > 0)
        {
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(GetRandomAudioClip(sfx));
            _audioSource.pitch = 1f;
        } else
        {
            Debug.Log($"ERROR: SFXSet {sfx.name} is empty");
        }
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    private AudioClip GetRandomAudioClip(SFXSetSO sfx)
    {
        if (sfx)
        {
            return sfx.AudioClips[Random.Range(0, sfx.AudioClips.Length)];
        } else
        {
            return null;
        }
    }
}
