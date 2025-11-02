/* PlayerController.cs */
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Character horizontal speed.")]
    public float moveSpeed = 5f;

    [Tooltip("The vertical force applied for the jump.")]
    public float jumpForce = 10f; // This is your public 'y' distance setting

    [Header("Ground Check")]
    [Tooltip("A transform at the player's feet to check for ground.")]
    public Transform groundCheck; // Assign an empty GameObject at player's feet
    
    [Tooltip("Radius of the circle used for ground check.")]
    [Range(0.01f, 1.0f)]
    public float groundCheckRadius = 0.2f;
    
    [Tooltip("Which layers are considered 'Ground' for jumping.")]
    public LayerMask whatIsGround; // Set this to your 'Ground' layer

    // Private component references
    private Animator anim; 
    private Rigidbody2D rb; 

    // Private state variables
    private bool isGrounded; // Tracks if the player is on the ground
    
    void Start()
    {
        // Get references to components on this GameObject
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // This is now required for physics

        if (rb == null)
        {
            Debug.LogError("PlayerController needs a Rigidbody2D component to function!");
        }
        if (groundCheck == null)
        {
            Debug.LogError("Assign a 'Ground Check' Transform in the Inspector!");
        }
    }

    void Update()
    {
        bool isMovingRight = false;
        
        // --- MOVEMENT INPUT (HOLD) ---
        // Check if the screen is being held down
        if (Input.GetMouseButton(0))
        {
            // Check if the touch position is on the right half of the screen
            if (Input.mousePosition.x > Screen.width / 2)
            {
                isMovingRight = true;
            }
        }
        
        // Apply movement based on the check
        if (isMovingRight)
        {
            MoveCharacter(Vector2.right);
        }
        else
        {
            StopCharacter();
        }

        // --- JUMP INPUT (TAP) ---
        // Check for a *new press* this frame
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the tap was on the left half AND the player is grounded
            if (Input.mousePosition.x <= Screen.width / 2 && isGrounded)
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        // --- GROUND CHECK ---
        // Physics checks should be done in FixedUpdate
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        // (Optional) Update the animator's 'isGrounded' parameter
        // if (anim != null)
        // {
        //     anim.SetBool("isGrounded", isGrounded);
        // }

        if (isGrounded)
        {
            Debug.Log("ZEMİNDEYİM!");
        }
        else
        {
            // Bu mesaj sürekli geliyorsa, zemin algılaması çalışmıyor demektir
            Debug.Log("HAVADAYIM!"); 
        }
    }
    void Jump()
    {
        // Apply an instant upward force (Impulse)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity first
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; // We are no longer grounded

        // (Optional) Trigger a jump animation
        // if (anim != null)
        // {
        //    anim.SetTrigger("isJumping");
        // }
    }

    /// <summary>
    /// Moves the character horizontally using Rigidbody velocity.
    /// </summary>
    void MoveCharacter(Vector2 direction)
    {
        // Set horizontal velocity, but maintain the current vertical (gravity) velocity
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // Update animation
        if (anim != null)
        {
            anim.SetBool("isWalking", true);
        }
    }

    /// <summary>
    /// Stops the character's horizontal movement.
    /// </summary>
    void StopCharacter()
    {
        // Stop horizontal movement, let gravity control vertical
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        // Update animation
        if (anim != null)
        {
            anim.SetBool("isWalking", false);
        }
    }
    
    /// <summary>
    /// Called by the Laser to kill the player.
    /// </summary>
    public void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("isHit"); 
        }

        // Disable this script and the collider
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true; // Stop responding to physics
        }

        // Destroy the player object after 1 second (to let 'isHit' animation play)
        // Destroy(gameObject, 1.0f);
    }

    /// <summary>
    /// (Helper) Draws the ground check gizmo in the Scene view.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}