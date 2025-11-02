using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [Tooltip("For score.")]
    // 3. UI Text Referansı
    public TextMeshProUGUI scoreText; 

    [Header("Scoring")]
    public float scoreMultiplier = 10f;
    
    private float scoreCounter = 0f; // Skoru hassas tutmak için float
    private int displayScore = 0; // Ekranda gösterilecek tam sayı skor
    private bool isGameActive = true; // Oyuncu ölünce skoru durdurmak için

    void Awake()
    {
        // Singleton kurulumu
        if (Instance == null)
        {
            Instance = this;
            // (İsteğe bağlı) Sahne değişse bile GameManager'ı koru
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Eğer zaten bir GameManager varsa, bu yenisini yok et
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Oyuna başlarken skoru sıfırla
        scoreCounter = 0f;
        displayScore = 0;
        UpdateScoreText();
        isGameActive = true;
    }

    void Update()
    {
        // 4. Skor Artırma
        // Sadece oyun aktifse (oyuncu ölmemişse) skoru artır
        if (isGameActive)
        {
            // Geçen zamana (Time.deltaTime) göre skoru artır
            scoreCounter += Time.deltaTime * scoreMultiplier;
            
            // Ekranda göstereceğimiz tam sayı skoru al
            int newDisplayScore = (int)scoreCounter;

            // Sadece skor gerçekten değiştiyse UI Text'i güncelle (Performans için)
            if (newDisplayScore > displayScore)
            {
                displayScore = newDisplayScore;
                UpdateScoreText();
            }
        }
    }

    /// <summary>
    /// UI'daki skor yazısını günceller.
    /// </summary>
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            // Text objesinin yazısını güncelle
            scoreText.text = "Score: " + displayScore.ToString();
        }
    }

    /// <summary>
    /// Bu fonksiyon, oyuncu öldüğünde PlayerController tarafından çağrılacak.
    /// </summary>
    public void GameOver()
    {
        isGameActive = false; // Skor sayacını durdur
        Debug.Log("Oyun Bitti! Final Skor: " + displayScore);
    }
}