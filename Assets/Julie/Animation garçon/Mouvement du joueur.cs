using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck; // R�f�rence pour le point de v�rification du sol
    public float groundCheckRadius = 0.2f; // Rayon du raycast pour v�rifier le sol
    public LayerMask groundLayer; // Masque de couche pour d�finir ce qui est consid�r� comme sol

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool wasJumping;

    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAnimation();
        FlipCharacter();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void HandleJump()
    {
        // Raycast pour v�rifier si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            wasJumping = true;
        }
    }

    private void HandleAnimation()
    {
        float verticalVelocity = rb.velocity.y;
        bool isFalling = verticalVelocity < -0.1f; // Seuil pour �viter les petites erreurs

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            if (wasJumping)
            {
                animator.SetBool("isWalking", false);
                wasJumping = false;
            }
            animator.SetBool("isFalling", false); // Assurez-vous que l'animation de chute est d�sactiv�e
        }
        else
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isFalling", isFalling);
        }

        // Mise � jour de l'animation de marche
        animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0);
    }

    private void FlipCharacter()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            // Inverser l'�chelle x du personnage en fonction de la direction du mouvement
            transform.localScale = new Vector3(-initialScale.x * Mathf.Sign(moveInput), initialScale.y, initialScale.z);
        }
    }
}
