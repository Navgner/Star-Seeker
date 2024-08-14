using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool wasJumping;
    private bool isFalling;

    // Stocker l'�chelle initiale pour le retour arri�re
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Sauvegarder l'�chelle initiale
        initialScale = transform.localScale;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Mise � jour des animations
        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // G�rer le saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            isGrounded = false;
            wasJumping = true; // Indique que le joueur est en train de sauter
        }

        // D�tecter la chute
        if (!isGrounded && rb.velocity.y < 0)
        {
            isFalling = true;
            animator.SetBool("isFalling", true);
        }
        else
        {
            isFalling = false;
            animator.SetBool("isFalling", false);
        }

        // G�rer le retour en Idle
        if (wasJumping && isGrounded)
        {
            animator.SetBool("isWalking", false);
            wasJumping = false; // R�initialiser le statut de saut
        }

        // Mettre � jour l'�tat de `isGrounded` dans l'Animator
        animator.SetBool("isGrounded", isGrounded);

        // Flip le joueur sans changer l'�chelle verticale
        if (moveInput < 0)
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); // Normal
        else if (moveInput > 0)
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z); // Invers�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // D�tection de sol pour permettre le saut
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isFalling", false); // Assurez-vous d'arr�ter l'animation de chute lorsque le joueur atterrit
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Lorsque le personnage quitte le sol, le marquer comme non au sol
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }
}
