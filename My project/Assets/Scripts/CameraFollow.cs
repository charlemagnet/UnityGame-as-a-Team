/* CameraFollow.cs */
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("The Player object for the camera to follow.")]
    public Transform playerToFollow;

    [Tooltip("The horizontal (X) distance to maintain from the player. (0 = centered)")]
    public float xOffset = 0f;

    // We will store the camera's fixed Y and Z positions from the start.
    private float fixedYPosition;
    private float fixedZPosition; // Cameras are usually at -10 Z

    void Start()
    {
        // Check if a player has been assigned in the Inspector
        if (playerToFollow == null)
        {
            Debug.LogError("CameraFollow script needs a 'Player To Follow' assigned in the Inspector!");
            this.enabled = false; // Disable the script if no player is set
            return;
        }

        // Store the camera's starting Y and Z positions.
        // The camera will be locked to these axes.
        fixedYPosition = transform.position.y;
        fixedZPosition = transform.position.z;
    }

    // LateUpdate runs after all 'Update' functions have finished.
    // This is the best place to move a camera that follows a physics object.
    void LateUpdate()
    {
        // If the player reference is somehow lost, do nothing.
        if (playerToFollow == null)
        {
            return;
        }

        // Create the new target position for the camera
        Vector3 targetPosition = new Vector3(
            playerToFollow.position.x + xOffset, // Follow the player's X position + offset
            fixedYPosition,                      // Use the FIXED Y position we stored in Start()
            fixedZPosition                       // Use the FIXED Z position we stored in Start()
        );

        transform.position = targetPosition;
    }
}