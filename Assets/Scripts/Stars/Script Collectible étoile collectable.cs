using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Déclarez un AudioClip pour le son de collecte

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

                Destroy(gameObject); // Détruit l'objet une fois collecté
            }
        }
    }
}
