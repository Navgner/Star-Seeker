using UnityEngine;

public class PlayerCollision2D : MonoBehaviour
{
    public QuizManager quizManager; // Référence au QuizManager

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision détectée avec : " + other.gameObject.name);

        for (int i = 0; i < quizManager.spheres.Length; i++)
        {
            // Compare avec les sphères en utilisant des Collider2D
            if (other.gameObject == quizManager.spheres[i])
            {
                Debug.Log("Sphère touchée avec l'index : " + i);
                quizManager.OnPlayerAnswer(i);
                return; // On a trouvé la sphère, pas besoin de continuer à vérifier
            }
        }
    }
}
