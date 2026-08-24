using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource bossAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip energyAudioClip;
    [SerializeField] private AudioClip shootingAudioClip;
    [SerializeField] private AudioClip reloadingAudioClip;

    // Singleton    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #region Sound Effects

    public void PlayShootingAudio()
    {
        effectAudioSource.PlayOneShot(shootingAudioClip);
    }

    public void PlayReloadingAudio()
    {
        effectAudioSource.PlayOneShot(reloadingAudioClip);
    }

    public void PlayEnergyAudio()
    {
        effectAudioSource.PlayOneShot(energyAudioClip);
    }

    #endregion
    

    #region Boss Audio

    public void PlayBossAudio()
    {
        bossAudioSource.Play();
    }

    #endregion
}