/* Laser.cs */
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The speed at which the laser moves.")]
    public float moveSpeed = 8f;
    
    [Header("Explosion")]
    [Tooltip("(Optional) The explosion effect prefab to spawn *in addition* to the animation.")]
    public GameObject explosionEffect; // You can still use this for extra particles

    [Tooltip("How long (in seconds) the explosion animation plays before the laser is destroyed.")]
    public float explosionDuration = 0.5f; // IMPORTANT: Match this to your animation's length

    // Private component references
    private Animator anim;
    private Collider2D coll;
    private Rigidbody2D rb;

    void Start()
    {
        // Get the laser's own components
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // This 'if' check ensures movement only happens if moveSpeed is not 0
        if (moveSpeed > 0)
        {
            // Move the laser to the left (from right to left)
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // Destroy the laser if it goes too far off-screen
        // (We also check moveSpeed > 0 so it doesn't self-destroy if it's exploding off-screen)
        if (transform.position.x < -15f && moveSpeed > 0) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we hit is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // 1. Tell the player to run its Die() function
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die(); // The player will handle its own 'isDead' or 'isHit' state
            }
            
            // 2. Stop the laser's movement immediately
            moveSpeed = 0f; 
            
            // If using a Rigidbody, stop its velocity too
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.isKinematic = true; // Stop all physics interactions
            }
            
            // 3. Disable the collider so it can't hit anything else
            if (coll != null)
            {
                coll.enabled = false;
            }

            // 4. Trigger the laser's own "isHit" (explosion) animation
            if (anim != null)
            {
                anim.SetTrigger("isHit");
            }
            
            // 5. (Optional) Spawn any *additional* particle effects
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // 6. Destroy this laser object *after* the animation has time to play
            // This 'explosionDuration' must match your animation's length!
            Destroy(gameObject, explosionDuration);
        }
    }
}