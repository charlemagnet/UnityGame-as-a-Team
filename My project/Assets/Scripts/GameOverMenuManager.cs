// GameOverManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; // Panel veya Button gibi UI elemanları için

public class GameOverMenuManager : MonoBehaviour
{
    // Singleton Deseni - Diğer scriptlerden kolayca erişmek için
    public static GameOverMenuManager Instance { get; private set; }

    [Header("UI Elementleri")]
    // Inspector'dan Game Over panelini buraya sürükle
    public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;      // "SCORE: " yazan objemiz için
    public TextMeshProUGUI highScoreText;  // "HIGH SCORE: " yazan objemiz için
    public GameObject newHighScoreLabel;   // Yeni Rekor yazısı (başlangıçta kapalı olmalı)

    // Eğer düğmeleri script'ten yöneteceksen, bu referansları da ekleyebilirsin.
    // public Button retryButton;
    // public Button mainMenuButton;

    private void Awake()
    {
        // Singleton kurulumu
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // GameOver menüsü genellikle sahneye özeldir, bu yüzden yorum satırında bırakıyorum
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Oyun başladığında Game Over panelini gizle
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Eğer newHighScoreLabel varsa, başlangıçta kapalı olduğundan emin ol
        if (newHighScoreLabel != null)
        {
            newHighScoreLabel.SetActive(false);
        }

    }

    public void ShowGameOverMenu(int finalScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Skorları ayarla ve yüksek skoru kontrol et
        SetScores(finalScore);

        // Zamanı durdur (GameManager da bunu yapabilir, ancak burada da yapılabilir)
        Time.timeScale = 0f;
    }

    // 1. "RETRY" (YENİDEN BAŞLAT) FONKSİYONU - Düğmeden çağrılacak
    public void YenidenBaslat()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.PlaySound(SoundType.click);
    }

    // 2. "ANA MENÜYE DÖN" FONKSİYONU - Düğmeden çağrılacak
    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Anamenu"); 
        SoundManager.PlaySound(SoundType.click);
    }

    public void SetScores(int newScore)
    {
        scoreText.text = "SCORE: " + newScore.ToString();

        int oldHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // 3. Eğer yeni puan, eski yüksek puandan BÜYÜKSE...
        if (newScore > oldHighScore)
        {
            // Yeni puanı "HighScore" olarak kaydet.
            PlayerPrefs.SetInt("HighScore", newScore);

            // Ve "High Score" yazısına da bu yeni rekoru yazdır.
            highScoreText.text = "HIGH SCORE: " + newScore.ToString();
            
            // Yeni Rekor Etiketini Göster
            if (newHighScoreLabel != null)
            {
                newHighScoreLabel.SetActive(true);
            }
        }
        else
        {
            // Eğer rekor kırılmadıysa, sadece eski rekoru yazdır.
            highScoreText.text = "HIGH SCORE: " + oldHighScore.ToString();
            // Yeni Rekor Etiketini Gizle
            if (newHighScoreLabel != null)
            {
                newHighScoreLabel.SetActive(false);
            }
        }
    }

    // Eğer düğme listener'ları burada atanıyorsa, OnDestroy'da temizlenmeli
    // void OnDestroy()
    // {
    //     if (retryButton != null) retryButton.onClick.RemoveAllListeners();
    //     if (mainMenuButton != null) mainMenuButton.onClick.RemoveAllListeners();
    // }
}