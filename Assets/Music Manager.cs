using UnityEngine;

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

    public AudioSource audioSource; // Assure-toi d'assigner une AudioSource dans l'inspecteur
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
        if (audioSource.clip == clip)
        {
            return; // La musique est déjà jouée
        }

        StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 0f)); // Fade-out musique actuelle
        audioSource.clip = clip;
        StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 1f)); // Fade-in nouvelle musique
        audioSource.Play();
    }

    public void StopMusic(float fadeDuration)
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 0f)); // Fade-out musique
    }
}
