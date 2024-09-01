using UnityEngine;

public class PlayerCollision2D : MonoBehaviour
{
    public QuizManager quizManager; // Référence au QuizManager

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision détectée avec : " + other.gameObject.name);

        // Itérer sur les sphères du quizManager
        for (int i = 0; i < quizManager.spheres.Length; i++)
        {
            // Comparer le collider avec les sphères
            if (other.gameObject == quizManager.spheres[i])
            {
                Debug.Log("Sphère touchée avec l'index : " + i);
                quizManager.OnPlayerAnswer(i);
                return; // On a trouvé la sphère, pas besoin de continuer à vérifier
            }
        }
    }
}
