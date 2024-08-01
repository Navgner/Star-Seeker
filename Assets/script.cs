using UnityEngine;

namespace Gamekit2D
{
    public class CollectibleStar : MonoBehaviour
    {
        // This method is called when another collider enters the trigger collider attached to the object this script is attached to
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the object that entered the trigger is tagged as "Player"
            if (other.CompareTag("Player"))
            {
                // Destroy this game object, making it disappear
                Destroy(gameObject);
            }
        }
    }
}
