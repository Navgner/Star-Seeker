using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        // Vérifiez s'il y a déjà une instance de MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ne pas détruire cet objet lors du chargement d'une nouvelle scène
        }
        else
        {
            Destroy(gameObject); // Détruire l'objet s'il y a déjà une instance
        }
    }
}

