/* AsteroidSpawner.cs */
using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The Asteroid prefab to be spawned.")]
    public GameObject[] asteroidPrefabs; 
    
    [Tooltip("Minimum time between spawns (in seconds).")]
    public float minSpawnTime = 1.0f; 
    
    [Tooltip("Maximum time between spawns (in seconds).")]
    public float maxSpawnTime = 3.0f; 

    [Header("Spawn Points")]
    [Tooltip("A list of specific points (at the top) where asteroids can spawn.")]
    public Transform[] spawnPoints; 

    [Header("Following")]
    [Tooltip("The Player object to follow on the X-axis.")]
    public Transform playerToFollow; 

    [Tooltip("The horizontal (X) distance to maintain from the player. (e.g., 5 = 5 units in front).")]
    public float xOffset = 5f;

    // This variable will store the spawner's fixed Y position (at the top)
    private float fixedYPosition;

    void Start()
    {
        // Store the initial Y position (which should be at the top of the screen)
        fixedYPosition = transform.position.y;

        // Check if spawn points are assigned
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the AsteroidSpawner!");
            return;
        }

        // Check if a player to follow has been assigned
        if (playerToFollow == null)
        {
            Debug.LogError("AsteroidSpawner has no 'Player To Follow' assigned.");
            return;
        }

        // Start the spawning loop
        StartCoroutine(SpawnAsteroidRoutine());
    }

    // LateUpdate is best for follow logic
    void LateUpdate()
    {
        // Follow the player on the X-axis, but keep the fixed Y position
        Vector3 targetPosition = new Vector3(
            playerToFollow.position.x + xOffset,
            fixedYPosition,
            transform.position.z
        );
        
        transform.position = targetPosition;
    }

    // The spawning coroutine
    // The spawning coroutine
    IEnumerator SpawnAsteroidRoutine()
    {
        // This loop will run indefinitely
        while (true)
        {
            // 1. Wait for a random amount of time
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // --- BU KISIM ÖNEMLİ ---

            // 2. Pick a random spawn point from the array
            int pointIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedPoint = spawnPoints[pointIndex];
            Vector2 spawnPosition = selectedPoint.position; 

            // 3. Pick a random PREFAB from the 'asteroidPrefabs' array
            // Hatanın olduğu yer burası olabilir. Önce bir prefab seçmelisin.
            int prefabIndex = Random.Range(0, asteroidPrefabs.Length);
            GameObject prefabToSpawn = asteroidPrefabs[prefabIndex];

            // 4. Instantiate the CHOSEN prefab at the chosen position
            // Hata mesajın, buraya 'prefabToSpawn' yerine 'asteroidPrefabs' (tüm listeyi) gönderdiğini söylüyor.
            if (prefabToSpawn != null) 
            {
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }
}