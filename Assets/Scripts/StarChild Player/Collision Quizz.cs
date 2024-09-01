using UnityEngine;

public class PlayerCollision2D : MonoBehaviour
{
    public QuizManager quizManager; // R�f�rence au QuizManager

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision d�tect�e avec : " + other.gameObject.name);

        // It�rer sur les sph�res du quizManager
        for (int i = 0; i < quizManager.spheres.Length; i++)
        {
            // Comparer le collider avec les sph�res
            if (other.gameObject == quizManager.spheres[i])
            {
                Debug.Log("Sph�re touch�e avec l'index : " + i);
                quizManager.OnPlayerAnswer(i);
                return; // On a trouv� la sph�re, pas besoin de continuer � v�rifier
            }
        }
    }
}
