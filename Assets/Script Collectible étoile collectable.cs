using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Ajoutez cette ligne pour le son de collecte

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddCollectible();

                 // Jouer le son de collecte
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                
                Destroy(gameObject); // Détruit l'objet une fois collecté
            }
        }
    }
}
