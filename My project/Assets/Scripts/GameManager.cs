using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [Tooltip("For score.")]
    public TextMeshProUGUI scoreText; // Oyun içindeki skor yazısı

    [Header("Scoring")]
    public float scoreMultiplier = 10f;
    
    // --- YENİ EKLENEN KISIM (BAŞLANGIÇ) ---
    [Header("Game Over")]
    public GameObject gameOverPanel; // Unity Editor'dan GameOver panelini buraya sürükle
    public GameOverManager gameOverManager; // GameOverManager scriptini buraya sürükle
    // --- YENİ EKLENEN KISIM (BİTİŞ) ---

    private float scoreCounter = 0f; 
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
        scoreCounter = 0f;
        displayScore = 0;
        UpdateScoreText();
        isGameActive = true;

        // --- GÜVENLİK ÖNLEMİ ---
        // Oyun başladığında GameOver panelinin kapalı olduğundan emin ol
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            scoreCounter += Time.deltaTime * scoreMultiplier;
            int newDisplayScore = (int)scoreCounter;

            if (newDisplayScore > displayScore)
            {
                displayScore = newDisplayScore;
                UpdateScoreText();
            }
        }
    }

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

        // --- GÜNCELLENEN KISIM (BAŞLANGIÇ) ---
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
        // --- GÜNCELLENEN KISIM (BİTİŞ) ---
    }
    
}