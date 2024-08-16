using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalCollectibles = 17;
    private int collectedCount = 0;
    public UIManager uiManager;
    public FadeController fadeController;
    public DialogueManager dialogueManager;

    void Start()
    {
        UpdateUI();
        dialogueManager.ShowDialogue("Je sens les ombres partout... mais les étoiles brillent toujours, elle m'appellent...");
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount == 1)
        {
            dialogueManager.ShowDialogue("Oh, je reconnais cette étoile...");
        }
        else if (collectedCount == 6)
        {
            dialogueManager.ShowDialogue("Elles sont vraiment belles...");
        }
        else if (collectedCount == 12)
        {
            dialogueManager.ShowDialogue("Oh c’est Eltanin...");
        }
        else if (collectedCount == totalCollectibles)
        {
            dialogueManager.ShowDialogue("Et maintenant...");
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
        if (fadeController != null)
        {
            fadeController.gameObject.SetActive(true);
            fadeController.StartFadeOut("EndGameScene", null);  // No music change needed here
        }
        else
        {
            Debug.LogError("fadeController is not assigned.");
        }
    }
}
