using UnityEngine;

public class maphareket : MonoBehaviour
{
    // [Inspector'dan Atanacaklar]
    public GameObject[] mapChunkPrefabs;
    public GameObject[] activeChunks; // Lütfen bunları soldan sağa sıralı atayın!
    public Transform player; 
    public float chunkLength; 

    // [Dahili Değişkenler]
    private float nextSpawnXPosition; 

    void Start()
    {
        if (activeChunks.Length > 0)
        {
            // Sahnede bulunan en sağdaki parçayı bularak bir sonraki ekleme pozisyonunu ayarla
            GameObject lastChunk = activeChunks[activeChunks.Length - 1];
            nextSpawnXPosition = lastChunk.transform.position.x + chunkLength;
        }
        else
        {
             Debug.LogError("Active Chunks dizisine parça atanmadı!");
        }
    }

    void Update()
    {

        float deleteThreshold = activeChunks[0].transform.position.x + chunkLength;

        if (player.position.x > deleteThreshold)
        {
            // 1. EN ARKADAKİ (SOLDAKİ) PARÇAYI SAHNEDEN SİL
            
            GameObject chunkToDelete = activeChunks[0];
            Destroy(chunkToDelete);

            // 2. DİZİ REFERANSLARINI KAYDIRMA (SİLİNEN PARÇAYI YOK SAYMA)
            
            // Tüm elemanları bir sola kaydır: [1] -> [0], [2] -> [1], vb.
            for (int i = 0; i < activeChunks.Length - 1; i++)
            {
                activeChunks[i] = activeChunks[i + 1];
            }
            
            // KAYDIRMA SONRASI EN SON ELEMANI TEMİZLE (Eski referansları tutmayı önle)
            // Bu, Array ile çalışırken güvenlik için eklenmiştir.
            activeChunks[activeChunks.Length - 1] = null; 

            // 3. RASTGELE YENİ PARÇA SEÇ VE EN ÖNE (SAĞA) EKLE

            // Prefab havuzundan rastgele bir parça seç
            int randomIndex = Random.Range(0, mapChunkPrefabs.Length);
            GameObject randomPrefab = mapChunkPrefabs[randomIndex];
            
            // Yeni parçanın pozisyonunu belirle
            Vector3 spawnPosition = new Vector3(nextSpawnXPosition, 0, 0);
            
            // Yeni parçayı sahnede yarat
            GameObject newChunk = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

            // 4. DİZİYE EKLE ve POZİSYONU GÜNCELLE
            
            // Boş kalan en son sıraya yeni parçayı yerleştir
            activeChunks[activeChunks.Length - 1] = newChunk;
            
            // Bir sonraki ekleme konumunu güncelle
            nextSpawnXPosition += chunkLength;
        
            
        }
    }
}
