using UnityEngine;

namespace Gamekit2D
{
    public class CollectibleStar : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Inventory playerInventory = other.GetComponent<Inventory>();
                if (playerInventory != null)
                {
                    playerInventory.AddItem(gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Player does not have an Inventory component.");
                }
            }
        }
    }
}
