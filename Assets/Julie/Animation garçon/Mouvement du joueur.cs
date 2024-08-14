using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool wasJumping;

    // Stocker l'échelle initiale pour le retour arrière
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Sauvegarder l'échelle initiale
        initialScale = transform.localScale;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Mise à jour des animations
        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Gérer le saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            isGrounded = false;
            wasJumping = true; // Indique que le joueur est en train de sauter
        }

        // Gérer le retour en Idle
        if (wasJumping && isGrounded)
        {
            animator.SetBool("isWalking", false);
            wasJumping = false; // Réinitialiser le statut de saut
        }

        // Mettre à jour l'état de `isGrounded` dans l'Animator
        animator.SetBool("isGrounded", isGrounded);

        // Flip le joueur sans changer l'échelle verticale
        if (moveInput < 0)
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); // Normal
        else if (moveInput > 0)
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z); // Inversé
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Détection de sol pour permettre le saut
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Lorsque le personnage quitte le sol, le marquer comme non au sol
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = false;
        }
    }
}
