using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Vitesse du mouvement de l'ennemi
    public float moveRange = 5f; // Plage de mouvement horizontal

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float movement = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector3.right * movement);

            if (transform.position.x > startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * movement);

            if (transform.position.x < startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }
}
