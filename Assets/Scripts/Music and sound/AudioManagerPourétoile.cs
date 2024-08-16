using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource audioSource; // Déclare une variable pour l'AudioSource

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Garder l'AudioManager entre les scènes
        }
        else
        {
            Destroy(gameObject); // Assurer qu'il n'y ait qu'une seule instance de l'AudioManager
        }
    }
}
