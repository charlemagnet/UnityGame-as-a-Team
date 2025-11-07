/* Asteroid.cs */
// This script should be added to ALL of your different asteroid prefabs.
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The maximum random horizontal speed. (e.g., 2 = can move between -2 and 2 on X-axis)")]
    public float maxHorizontalSpeed = 2f; 

    private Rigidbody2D rb;
    private int groundLayerId; // NEW: We will store the Ground Layer's ID

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // NEW: Get the integer ID for the "Ground" layer.
        // This is much faster than checking the tag every frame.
        groundLayerId = LayerMask.NameToLayer("Ground");

        if (rb == null)
        {
            Debug.LogError("Asteroid is missing a Rigidbody2D component!");
            return;
        }

        // Calculate and apply random horizontal speed
        float horizontalSpeed = Random.Range(-maxHorizontalSpeed, maxHorizontalSpeed);
        rb.linearVelocity = new Vector2(horizontalSpeed, 0f);
    }

    void Update()
    {
        // Destroy if it falls too far off-screen
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // --- THIS FUNCTION IS NOW UPDATED ---
    // Called because the collider is set to "Is Trigger"
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check 1: Did we hit the Player?
        if (other.CompareTag("Player"))
        {
            SoundManager.PlaySound(SoundType.crash);
            // Get the PlayerController and call Die()
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die(); 
            }

            // Destroy the asteroid
            Destroy(gameObject);
        }
        // NEW Check 2: Did we hit the Ground?
        // We check if the layer of the object we hit matches the 'groundLayerId' we stored.
        else if (other.gameObject.layer == groundLayerId)
        {
            // (Optional: You could play a small dust/explosion effect here)

            // Destroy the asteroid
            Destroy(gameObject);
        }
    }
}