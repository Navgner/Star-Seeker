using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    public List<string> scenes; // Liste des sc�nes pour les transitions
    public List<string> excludedScenes; // Liste des sc�nes o� la touche Espace ne doit pas �tre utilis�e

    private void Awake()
    {
        // Assurez-vous qu'il y a une seule instance de ce gestionnaire
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

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // D�bogage pour v�rifier la sc�ne actuelle
        Debug.Log("Current Scene: " + currentScene);

        // V�rification si la sc�ne actuelle est dans la liste des sc�nes exclues
        if (excludedScenes.Contains(currentScene))
        {
            Debug.Log("Scene " + currentScene + " is excluded from space-triggered transitions.");
            return; // Ne rien faire si la sc�ne est exclue
        }

        // Transition si la touche Espace est press�e et la sc�ne n'est pas exclue
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed, attempting to load next scene.");
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int currentIndex = scenes.IndexOf(currentScene);

        if (currentIndex >= 0 && currentIndex < scenes.Count - 1)
        {
            string nextScene = scenes[currentIndex + 1];
            SceneManager.LoadScene(nextScene);
            Debug.Log("Loading scene: " + nextScene);
        }
        else
        {
            Debug.LogWarning("No next scene found or current scene is not in the list.");
        }
    }
}
