using UnityEngine;

/// <summary>
/// Scriptable object that allows defining sets of audio clips to be used by the monobehaviours.
/// This way the audio clips instances are shared, saving memory.
/// </summary>
[CreateAssetMenu(fileName = "SFXSet", menuName = "ReshiErchin/SFX Set")]
public class SFXSetSO : ScriptableObject
{
    [SerializeField] private AudioClip[] _audioClips;

    public AudioClip[] AudioClips => _audioClips;
}
