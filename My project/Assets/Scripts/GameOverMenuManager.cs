using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // BU SATIRI EKLE (Text Mesh Pro kütüphanesi)
using UnityEngine.SceneManagement; // Sahne yüklemek ve değiştirmek için bu kütüphane ŞART!

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // "SCORE: " yazan objemiz için
    public TextMeshProUGUI highScoreText; // "HIGH SCORE: " yazan objemiz için
    // 1. "RETRY" (YENİDEN BAŞLAT) FONKSİYONU
    public void YenidenBaslat()
    {
        // Bu kod, o an hangi sahne açıksa onu yeniden yükler.
        // Yani "Level_1"deysek "Level_1"i, "Level_2"deysek "Level_2"yi yeniden başlatır.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 2. "ANA MENÜYE DÖN" FONKSİYONU
    public void AnaMenuyeDon()
    {
        // Adını verdiğimiz "AnaMenu" sahnesini yükler.
        // Bu sahnenin Build Profiles'da (Build Settings) olduğundan emin olmalıyız.
        SceneManager.LoadScene("Anamenu");
        // Not: Senin sahnenin adı "Anamenu" ise ("a" küçükse) yukarıyı "Anamenu" yapmalısın.
        // Build Profiles'da nasıl yazdıysan o şekilde olmalı.
    }
    // Bu fonksiyon, oyun sahnesinden çağrılacak.
// Oyuncu ölünce, oyun sahnesi bu fonksiyona puanı gönderecek.
    public void SetScores(int newScore)
    {
        // 1. Gelen yeni puanı "Score" yazısına yazdır.
        scoreText.text = "SCORE: " + newScore.ToString();

        // 2. "High Score" (En Yüksek Puan) kontrolü yap.
        // PlayerPrefs, Unity'nin telefona/bilgisayara küçük verileri kaydetme yöntemidir.

        // Önce kayıtlı olan eski yüksek puanı "HighScore" adıyla çağır (yoksa 0 getir).
        int oldHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // 3. Eğer yeni puan, eski yüksek puandan BÜYÜKSE...
        if (newScore > oldHighScore)
        {
            // Yeni puanı "HighScore" olarak kaydet.
            PlayerPrefs.SetInt("HighScore", newScore);

            // Ve "High Score" yazısına da bu yeni rekoru yazdır.
            highScoreText.text = "HIGH SCORE: " + newScore.ToString();
        }
        else
        {
            // Eğer rekor kırılmadıysa, sadece eski rekoru yazdır.
            highScoreText.text = "HIGH SCORE: " + oldHighScore.ToString();
        }
    }
}