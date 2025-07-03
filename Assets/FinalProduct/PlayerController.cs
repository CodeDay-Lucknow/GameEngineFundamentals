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
    private bool wasGroundedLastFrame;

    private void Start()
    {
    }

    private void Update()
    {
        Move();
        CheckGrounded();
        Jump();
        
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
            Debug.Log("Flipping right!");
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
            Debug.Log("Flipping left!");
        }
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount > 0)
        {
            jumpCount--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("Jumps remaining: " + jumpCount);
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGroundedLastFrame)
        {
            // Only reset jump when you newly land, not when you're already grounded.
            jumpCount = maxJumps;
        }

        wasGroundedLastFrame = isGrounded;

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
