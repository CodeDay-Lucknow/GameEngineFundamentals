using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    public Rigidbody2D rb;
    private bool isGrounded;
    bool isMoving;
    bool isFacingRight = true;
    public Animator animator;

    private int jumpCount;
    public int maxJumps = 2; // Allows double jump

    private void Start()
    {
    }

    private void Update()
    {
        Move();
        Jump();
        CheckGrounded();
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if(moveInput != 0)
        {
            isMoving = true;
        }
        else
            isMoving = false;
        animator.SetBool("Moving", isMoving);

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
            Debug.Log("Flipping left!");
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
            Debug.Log("Flipping left!");
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            jumpCount = maxJumps;
        }
        animator.SetBool("Jumping", !isGrounded);
    }

    // For visualizing the ground check in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

   void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
