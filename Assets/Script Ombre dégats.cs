using UnityEngine;
using Gamekit2D; // Inclure le namespace Gamekit2D

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1; // Nombre de dégâts infligés au joueur

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Vérifiez si le trigger touche le joueur
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Enemy triggered with player."); // Debug message

            Damageable damageable = collider.GetComponent<Damageable>();
            if (damageable != null)
            {
                Debug.Log("Damageable component found."); // Debug message

                // Crée une instance de Damager depuis le namespace Gamekit2D
                Damager damager = new Damager { damage = damageAmount };

                // Inflige des dégâts au joueur
                damageable.TakeDamage(damager);
            }
            else
            {
                Debug.LogWarning("Damageable component not found on player."); // Debug message
            }
        }
        else
        {
            Debug.Log("Trigger not with player."); // Debug message
        }
    }
}
