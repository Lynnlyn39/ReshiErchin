using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SFXPlayer))]
public class PlayerSounds : MonoBehaviour
{
    [SerializeField] SFXSetSO _footSteps;
    [SerializeField] SFXPlayer _sfxPlayer;


    // Start is called before the first frame update
    void Start()
    {
        if (!_sfxPlayer)
        {
            _sfxPlayer = GetComponent<SFXPlayer>();
        }
    }

    public void PlayStepSFX()
    {
        _sfxPlayer.Play(_footSteps);
    }

}
