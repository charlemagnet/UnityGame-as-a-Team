// StarSpawner.cs (Güncellendi - GameManager durum kontrolü eklendi)
using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour
{
    public static StarSpawner Instance { get; private set; }

    [Header("Yaratma Ayarları")]
    public GameObject starPrefab;
    public Transform[] spawnPoints; 

    [Header("Zamanlama Ayarları")]
    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 3f;

    [Header("Takip Ayarları")]
    public Transform playerTransform; 
    public Vector3 offset = new Vector3(0, 2f, 0); 

    private Coroutine spawnCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (starPrefab != null && spawnPoints.Length > 0)
        {
            // Oyun başlamadıysa yıldız yaratma
            if (!GameManager.Instance.IsGameOver)
            {
                spawnCoroutine = StartCoroutine(SpawnStarRoutine());
            }
        }
        else
        {
            Debug.LogError("Star Prefab veya Spawn Points atanmamış! Lütfen Inspector'dan atama yapın.");
        }
    }

    void LateUpdate()
    {
        // Sadece oyun devam ederken oyuncuyu takip et
        if (!GameManager.Instance.IsGameOver && playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
    }

    public void NotifyStarCollected()
    {
        // Sadece oyun devam ederken yeni yıldız yaratma rutini başlat
        if (GameManager.Instance.IsGameOver) return; 

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); 
        }
        spawnCoroutine = StartCoroutine(SpawnStarRoutine());
    }

    IEnumerator SpawnStarRoutine()
    {
        // Sadece oyun devam ederken bekle ve yarat
        if (GameManager.Instance.IsGameOver) yield break; // Oyun bittiyse dur

        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);

        // Bekledikten sonra da oyunun bitmediğinden emin ol
        if (GameManager.Instance.IsGameOver) yield break; // Oyun bittiyse dur

        if (starPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Star Prefab veya Spawn Points atanmamış! Yeni yıldız yaratılamıyor.");
            yield break;
        }

        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        
        Instantiate(starPrefab, selectedSpawnPoint.position + randomOffset, Quaternion.identity);
    }
}