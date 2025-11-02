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
    // This array will hold all your empty GameObject spawn points
    public Transform[] spawnPoints; 

    void Start()
    {
        // Check if spawn points are assigned in the Inspector
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the LaserSpawner! Please add Transform references.");
            return; // Stop the script if no points are set
        }

        // Start the laser spawning loop
        StartCoroutine(SpawnLaserRoutine());
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
            // Random.Range(int min, int max) 'max' is exclusive, so .Length is correct
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedPoint = spawnPoints[randomIndex];

            // 3. Get the position of the selected spawn point
            Vector2 spawnPosition = selectedPoint.position;

            // 4. Instantiate the laser at the chosen position
            if (laserPrefab != null)
            {
                // We use selectedPoint.position and Quaternion.identity (no rotation)
                Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    
}