using UnityEngine;
using TMPro; // Importez le namespace pour TextMeshPro

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Déclarez un AudioClip pour le son de collecte
    public string dialogueText; // Ajoutez un champ pour le texte de dialogue
    public DialogueManager dialogueManager; // Ajoutez une référence au DialogueManager

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddCollectible();

                // Jouer le son de collecte en utilisant l'AudioManager
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.audioSource.PlayOneShot(collectSound);
                }

                // Afficher le texte de dialogue si disponible
                if (!string.IsNullOrEmpty(dialogueText) && dialogueManager != null)
                {
                    dialogueManager.ShowDialogue(dialogueText);
                }

                Destroy(gameObject); // Détruit l'objet une fois collecté
            }
        }
    }
}
