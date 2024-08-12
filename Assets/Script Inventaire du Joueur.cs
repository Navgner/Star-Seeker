using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalCollectibles = 17; // Nombre total d'étoiles à collecter
    private int collectedCount = 0; // Nombre d'étoiles collectées
    public UIManager uiManager;
    public FadeController fadeController; // Référence au FadeController

    void Start()
    {
        UpdateUI();
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount >= totalCollectibles)
        {
            EndGame();
        }
    }

    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateCollectibleCount(collectedCount, totalCollectibles);
        }
    }

    private void EndGame()
    {
        // Activer le Canvas de fondu et démarrer le fondu au noir
        fadeController.gameObject.SetActive(true);
        fadeController.StartFadeOut();
    }
}
