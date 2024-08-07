using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalCollectibles = 17; // Nombre total d'étoiles à collecter
    private int collectedCount = 0; // Nombre d'étoiles collectées
    public UIManager uiManager;

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
        // Logic to end the game, for example:
        Debug.Log("All collectibles gathered! Game Over!");
        // Here you can trigger the end game scene or any other action
    }
}
