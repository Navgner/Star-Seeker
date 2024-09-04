using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalCollectibles = 17;
    private int collectedCount = 0;
    public UIManager uiManager;

    private void Start()
    {
        UpdateUI();
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount == totalCollectibles)
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
        // Assurez-vous que SceneTransitionManager est bien configuré
        if (SceneTransitionManager.Instance != null)
        {
            Debug.Log("Ending game. Loading EndGameScene.");
            SceneTransitionManager.Instance.LoadNextScene(); // Utilisez la méthode LoadNextScene
        }
        else
        {
            Debug.LogError("SceneTransitionManager is not assigned.");
        }
    }
}
