using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        // V�rifiez s'il y a d�j� une instance de MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ne pas d�truire cet objet lors du chargement d'une nouvelle sc�ne
        }
        else
        {
            Destroy(gameObject); // D�truire l'objet s'il y a d�j� une instance
        }
    }
}

