using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusicManager : MonoBehaviour
{
    private static PersistentMusicManager instance = null;
    public AudioClip musicClip;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Music started: " + musicClip.name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("MainMenu scene loaded. Destroying PersistentMusicManager.");
            Destroy(gameObject);
        }
    }
}
