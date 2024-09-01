using UnityEngine;

public class PlayerCollision2D : MonoBehaviour
{
    public QuizManager quizManager; // R�f�rence au QuizManager

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision d�tect�e avec : " + other.gameObject.name);

        for (int i = 0; i < quizManager.spheres.Length; i++)
        {
            // Compare avec les sph�res en utilisant des Collider2D
            if (other.gameObject == quizManager.spheres[i])
            {
                Debug.Log("Sph�re touch�e avec l'index : " + i);
                quizManager.OnPlayerAnswer(i);
                return; // On a trouv� la sph�re, pas besoin de continuer � v�rifier
            }
        }
    }
}
