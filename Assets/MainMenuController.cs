using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // M�thode pour charger la sc�ne
    public void StartGame()
    {
        // Nom de la sc�ne � charger
        SceneManager.LoadScene("testing 1");
    }
}
