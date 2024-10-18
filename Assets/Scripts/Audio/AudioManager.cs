using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SFX
{
    PlayerWalk,
    PlayerJump,
    PlayerAttack,
    PlayerHurt,
    PlayerDeath,
    ButtonClick,
}

[Serializable]
struct SFXConfig
{
    public SFX Type;
    public AudioClip AudioClip;
    public float VolumeScale;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private AudioSource EnvironmentAudioSource;
    [SerializeField] private SFXConfig[] SFXConfigs;

    private Dictionary<SFX, SFXConfig> SFXs;

    private void Awake()
    {
        SFXs = SFXConfigs.ToDictionary(sfxConfig => sfxConfig.Type, sfxConfig => sfxConfig);
    }

    public void PlaySFX(SFX type)
    {
        if (SFXs.ContainsKey(type))
        {
            SFXConfig config = SFXs[type];
            SFXAudioSource.PlayOneShot(config.AudioClip, config.VolumeScale);
        }
    }
}
