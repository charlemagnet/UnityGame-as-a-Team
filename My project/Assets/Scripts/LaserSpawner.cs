/* LaserSpawner.cs */
using System.Collections;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The Laser prefab to be spawned.")]
    public GameObject laserPrefab; 
    
    [Tooltip("Minimum time between spawns (in seconds).")]
    public float minSpawnTime = 0.5f; 
    
    [Tooltip("Maximum time between spawns (in seconds).")]
    public float maxSpawnTime = 2.0f; 

    [Header("Spawn Points")]
    [Tooltip("A list of specific points where lasers can spawn. The spawner will pick one at random.")]
    public Transform[] spawnPoints; 

    [Header("Following")]
    [Tooltip("The Player object to follow on the X-axis.")]
    public Transform playerToFollow; // Assign your Player object here

    [Tooltip("The horizontal (X) distance to maintain from the player. (e.g., 10 = 10 units in front).")]
    public float xOffset = 10f;

    // This variable will store the spawner's starting Y position
    private float initialYPosition;

    void Start()
    {
        // Store the initial Y position so the spawner doesn't move up or down
        initialYPosition = transform.position.y;

        // Check if spawn points are assigned in the Inspector
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the LaserSpawner!");
            return;
        }

        // Check if a player to follow has been assigned
        if (playerToFollow == null)
        {
            Debug.LogWarning("LaserSpawner has no 'Player To Follow' assigned. It will remain stationary.");
        }

        // Start the laser spawning loop
        StartCoroutine(SpawnLaserRoutine());
    }

    // LateUpdate runs after all Update functions. Best for camera/follow logic.
    void LateUpdate()
    {
        // Check if a player has been assigned
        if (playerToFollow != null)
        {
            // Create a new target position for the spawner
            // X = Player's X position + the offset
            // Y = The spawner's original starting Y position (so it never moves vertically)
            // Z = The spawner's original Z position
            Vector3 targetPosition = new Vector3(
                playerToFollow.position.x + xOffset,
                initialYPosition,
                transform.position.z
            );
            
            // Move the spawner to the target position
            transform.position = targetPosition;
        }
    }

    // A Coroutine that can pause its execution
    IEnumerator SpawnLaserRoutine()
    {
        // This loop will run indefinitely
        while (true)
        {
            // 1. Determine a random wait time
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // 2. Pick a random spawn point from the array
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedPoint = spawnPoints[randomIndex];

            // 3. Get the *global* position of the selected spawn point
            Vector2 spawnPosition = selectedPoint.position; 

            // 4. Instantiate the laser at the chosen position
            if (laserPrefab != null)
            {
                Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}