using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Skor Ayarları")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("Game Over Menüsü")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;     // Panelde gösterilen "SCORE: ..."
    public TextMeshProUGUI highScoreText;      // Panelde gösterilen "HIGH SCORE: ..."
    public GameObject newHighScoreLabel;       // "NEW HIGH SCORE!" yazısı

    public bool IsGameOver { get; private set; } = false; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Eğer sahneler arasında taşınsın istiyorsan aktif et
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (newHighScoreLabel != null)
            newHighScoreLabel.SetActive(false);

        IsGameOver = false;
        Time.timeScale = 1f;
        UpdateScoreUI();
    }

    /// <summary>
    /// Skora puan ekler ve UI'ı günceller.
    /// </summary>
    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        score += amount;
        UpdateScoreUI();
    }

    /// <summary>
    /// Oyun sırasında görünen skor yazısını günceller.
    /// </summary>
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    /// <summary>
    /// Oyuncu öldüğünde çağrılır ve Game Over panelini gösterir.
    /// </summary>
    public void GameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        Debug.Log("Game Over! Score: " + score);

        // Paneli aktif et
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Game Over Panel referansı eksik! Inspector'dan atamayı unutma.");
        }

        // Skorları yaz
        if (finalScoreText != null)
            finalScoreText.text = "SCORE: " + score;

        int oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            if (highScoreText != null)
                highScoreText.text = "HIGH SCORE: " + score;

            if (newHighScoreLabel != null)
                newHighScoreLabel.SetActive(true);
        }
        else
        {
            if (highScoreText != null)
                highScoreText.text = "HIGH SCORE: " + oldHighScore;

            if (newHighScoreLabel != null)
                newHighScoreLabel.SetActive(false);
        }

        Time.timeScale = 0f; // Oyunu durdur
    }

    public void YenidenBaslat()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.PlaySound(SoundType.click);
    }

    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Anamenu");
        SoundManager.PlaySound(SoundType.click);
    }
}
