using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalCollectibles = 17; // Nombre total d'étoiles à collecter
    private int collectedCount = 0; // Nombre d'étoiles collectées
    public UIManager uiManager;
    public FadeController fadeController; // Référence au FadeController
    public DialogueManager dialogueManager; // Référence au DialogueManager

    void Start()
    {
        UpdateUI();
        dialogueManager.ShowDialogue("Je sens les ombres partout... mais les étoiles brillent toujours, elle m'appellent..."); // Afficher le premier message
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount == 1)
        {
            dialogueManager.ShowDialogue("Oh, je reconnais cette étoile, elle appartient à Ladon ! Le dragon céleste de Héra, ancien gardien des jardins des Hespérides ! Mmmmmh… Les autres ne doivent pas être loin… ");
        }
        if (collectedCount == 6)
        {
            dialogueManager.ShowDialogue("Elles sont vraiment belles ces étoiles, si brillantes qu'elles me rappellent les pommes dorées des jardins, celles qu'Hercule parvint à voler sous le nez du dragon");
        }
        else if (collectedCount == 12)
        {
            dialogueManager.ShowDialogue("Oh c’est Eltanin, la plus lumineuse sans aucun doute ! Ladon peut se réveiller et faire dissiper ces ténèbres dès que j'ai récolté le reste des étoiles. ");
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
        // Activer le Canvas de fondu et démarrer le fondu au noir
        fadeController.gameObject.SetActive(true);
        fadeController.StartFadeOut();
    }
}
