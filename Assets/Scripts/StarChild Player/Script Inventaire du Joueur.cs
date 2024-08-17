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
        dialogueManager.ShowDialogue("Je sens les ombres partout... mais les étoiles brillent toujours, elles m'appellent...");
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount == 1)
        {
            dialogueManager.ShowDialogue("Oh, je reconnais cette étoile, elle appartient à Ladon ! Le dragon céleste de Héra, ancien gardien des jardins des Hespérides ! Mmmmmh… Les autres ne doivent pas être loin…");
        }
        else if (collectedCount == 6)
        {
            dialogueManager.ShowDialogue("Elles sont vraiment belles ces étoiles, si brillantes qu'elles me rappellent les pommes dorées des jardins, celles qu'Hercule parvint à voler sous le nez du dragon.");
        }
        else if (collectedCount == 12)
        {
            dialogueManager.ShowDialogue("Oh c’est Eltanin, la plus lumineuse sans aucun doute ! Ladon peut se réveiller et faire dissiper ces ténèbres dès que j'ai récolté le reste des étoiles.");
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
            Debug.Log("Ending game. Loading EndGameScene.");
            fadeController.StartFadeOut("EndGameScene");
        }
        else
        {
            Debug.LogError("fadeController is not assigned.");
        }
    }
}
