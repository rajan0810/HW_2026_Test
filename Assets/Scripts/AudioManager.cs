using UnityEngine;

public class AudioManager: MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;
    public AudioClip fallSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayBGM()
    {
        if (backgroundMusic != null && !bgmSource.isPlaying)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayButtonClick()
    {
        if (buttonClickSound != null) sfxSource.PlayOneShot(buttonClickSound);
    }

    public void PlayFall()
    {
        if (fallSound != null) sfxSource.PlayOneShot(fallSound);
    }
}