using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Méthode pour charger la scène
    public void StartGame()
    {
        // Nom de la scène à charger
        SceneManager.LoadScene("testing 1");
    }
}
