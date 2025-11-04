using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [Tooltip("For score.")]
    public TextMeshProUGUI scoreText; // Oyun içindeki skor yazısı

    // --- YENİ EKLENEN KISIM (BAŞLANGIÇ) ---
    [Header("Player Reference")]
    [Tooltip("Skoru hesaplamak için oyuncunun Transform'unu buraya sürükleyin.")]
    public Transform playerTransform; 
    // --- YENİ EKLENEN KISIM (BİTİŞ) ---

    [Header("Scoring")]
    public float scoreMultiplier = 10f; // Skoru pozisyonla çarpmak için
    
    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameOverManager gameOverManager;

    // private float scoreCounter = 0f; // Bu değişkene artık gerek yok.
    private int displayScore = 0; 
    private bool isGameActive = true; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Oyuncu atanmamışsa hata ver
        if (playerTransform == null)
        {
            Debug.LogError("GameManager: 'Player Transform' atanmamış! Lütfen Inspector'dan sürükleyin.");
        }

        displayScore = 0;
        UpdateScoreText();
        isGameActive = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // --- BU FONKSİYON TAMAMEN GÜNCELLENDİ ---
    void Update()
    {
        // Sadece oyun aktifse ve player ataması yapıldıysa
        if (isGameActive && playerTransform != null)
        {
            // 1. Skoru, oyuncunun X pozisyonuna göre hesapla
            // (Eğer oyuncu x=0'dan başlıyorsa bu doğrudur)
            int newDisplayScore = (int)(playerTransform.position.x * scoreMultiplier);

            // 2. Sadece skor artmışsa UI'ı güncelle
            // (Bu, oyuncu bir şekilde geriye gitse bile skorun düşmesini engeller)
            if (newDisplayScore > displayScore)
            {
                displayScore = newDisplayScore;
                UpdateScoreText();
            }
        }
    }
    // --- GÜNCELLEME BİTİŞ ---

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + displayScore.ToString();
        }
    }

    public void GameOver()
    {
        isGameActive = false; // Skor sayacını durdur
        Debug.Log("Oyun Bitti! Final Skor: " + displayScore);

        // --- YENİ EKLENEN SATIR ---
        Time.timeScale = 0f; // ZAMANI DURDUR!
        // --- BİTİŞ ---

        // 1. GameOver Panelini Aktif Et
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 2. GameOverManager'a son skoru gönder
        if (gameOverManager != null)
        {
            gameOverManager.SetScores(displayScore);
        }
    }
}