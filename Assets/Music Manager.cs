using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip defaultMusicClip;
    public AudioClip endMusic;
    public AudioClip gameMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource.clip == null && defaultMusicClip != null)
        {
            PlayMusic(defaultMusicClip);
        }
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f)
    {
        if (audioSource.isPlaying)
        {
            StartCoroutine(FadeOutAndPlayNewClip(clip, fadeDuration));
        }
        else
        {
            audioSource.clip = clip;
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(FadeIn(fadeDuration));
        }
    }

    private IEnumerator FadeOutAndPlayNewClip(AudioClip newClip, float fadeDuration)
    {
        yield return StartCoroutine(FadeOut(fadeDuration));
        audioSource.clip = newClip;
        audioSource.Play();
        StartCoroutine(FadeIn(fadeDuration));
    }

    private IEnumerator FadeIn(float fadeDuration)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 1f;
    }

    private IEnumerator FadeOut(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void StopMusic(float fadeDuration = 1f)
    {
        StartCoroutine(FadeOut(fadeDuration));
    }

    public void StopMusicImmediately()
    {
        audioSource.Stop();
        audioSource.volume = 1f;
    }
}
