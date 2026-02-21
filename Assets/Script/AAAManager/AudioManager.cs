using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // ============ Singleton pattern ===========
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    // ============ Audio Source && Clip ===========
    [SerializeField] private AudioSource effectAudioSource, bossAudioSource;
    [SerializeField] private AudioClip energyAudioClip, shootingAudioClip, reloadingAudioClip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ============ Play Audio Clip ===========
    // PlayOneShot laf phương thức của AudioSource để phát một clip âm thanh mà không làm gián đoạn clip đang phát hiện tại
    // Play là phương thức của AudioSource để phát một clip âm thanh và sẽ dừng clip đang phát hiện tại nếu có
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

    public void PlayBossAudio()
    {
        bossAudioSource.Play();
    }

}
