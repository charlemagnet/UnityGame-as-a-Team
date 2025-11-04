using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // "SCORE: " yazan objemiz için
    public TextMeshProUGUI highScoreText; // "HIGH SCORE: " yazan objemiz için
    // 1. "RETRY" (YENİDEN BAŞLAT) FONKSİYONU
    public GameObject newHighScoreLabel;
    public void YenidenBaslat()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    // 2. "ANA MENÜYE DÖN" FONKSİYONU
    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Anamenu");
        
    }
    // Bu fonksiyon, oyun sahnesinden çağrılacak.
// Oyuncu ölünce, oyun sahnesi bu fonksiyona puanı gönderecek.
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
            if (newHighScoreLabel != null)
            {
                newHighScoreLabel.SetActive(true);
            }
        }
        else
        {
            // Eğer rekor kırılmadıysa, sadece eski rekoru yazdır.
            highScoreText.text = "HIGH SCORE: " + oldHighScore.ToString();
        }
    }
}