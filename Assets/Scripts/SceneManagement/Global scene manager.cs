using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    public List<string> scenes; // Liste des scènes pour les transitions
    public List<string> excludedScenes; // Liste des scènes où la touche Espace ne doit pas être utilisée

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

        // Débogage pour vérifier la scène actuelle
        Debug.Log("Current Scene: " + currentScene);

        // Vérification si la scène actuelle est dans la liste des scènes exclues
        if (excludedScenes.Contains(currentScene))
        {
            Debug.Log("Scene " + currentScene + " is excluded from space-triggered transitions.");
            return; // Ne rien faire si la scène est exclue
        }

        // Transition si la touche Espace est pressée et la scène n'est pas exclue
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
