using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("MusicManager");
                    instance = obj.AddComponent<MusicManager>();
                }
            }
            return instance;
        }
    }

    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip endMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip, float fadeDuration)
    {
        StartCoroutine(PlayMusicCoroutine(clip, fadeDuration));
    }

    private IEnumerator PlayMusicCoroutine(AudioClip clip, float fadeDuration)
    {
        // Fade out the current music
        if (audioSource.isPlaying)
        {
            yield return StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 0f));
        }

        // Change the clip and fade in the new music
        audioSource.clip = clip;
        audioSource.Play();
        yield return StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 1f));
    }

    public void StopMusic(float fadeDuration)
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 0f));
    }
}
