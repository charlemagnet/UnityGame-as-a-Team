using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yüklemek (Oyna tuşu) için bu kütüphane ŞART!
using UnityEngine.UI; // Toggle (Ses tuşu) için bu gerekebilir.

public class MainMenuManager : MonoBehaviour
{
    // Oyna tuşuna bastığımızda yüklenecek olan oyun sahnesinin ADINI buraya yazın.
    // Bu sahnenin Build Settings'e eklenmiş olması gerekir.
    public string Game = "Game"; // BURAYI KENDİ SAHNE ADINLA DEĞİŞTİR

    
    // 1. OYNA TUŞU FONKSİYONU
    // Bu fonksiyonu "public" yapıyoruz ki Unity'nin Buton bileşeni onu görebilsin.
    public void OyunuBaslat()
    {
        // Belirtilen oyun sahnesini yükler.
        SceneManager.LoadScene(Game);
    }

    // 2. ÇIKIŞ TUŞU FONKSİYONU
    public void OyundanCik()
    {
        // Not: Bu komut Unity Editör'de ÇALIŞMAZ.
        // Sadece derlenmiş (build) oyunda (PC veya mobil) çalışır.
        // Editörde test etmek için konsola bir mesaj yazdırabiliriz:
        Debug.Log("Çıkış düğmesine basıldı!");
        Application.Quit();
    }

    // 3. SES TOGGLE FONKSİYONU
    // Toggle (aç/kapat), bir "bool" (doğru/yanlış) değeri gönderir.
    // Fonksiyonun bu "bool durum" parametresini alması gerekir.
    public void SesiAyarla(bool durum)
    {
        if (durum == true)
        {
            // Toggle İŞARETLİ (AÇIK) ise:
            // Oyundaki tüm sesleri aç (veya sesi duraklatmayı kaldır).
            AudioListener.pause = false; 
        }
        else
        {
            // Toggle İŞARETLİ DEĞİL (KAPALI) ise:
            // Oyundaki tüm sesleri kapat (duraklat).
            AudioListener.pause = true; 
        }
    }

    // Başlangıçta sesin açık olduğundan ve Toggle'ın da "açık" göründüğünden emin olalım.
    void Start()
    {
        AudioListener.pause = false; // Sesler açık başlasın
        // (Toggle'ın "Is On" ayarını Unity içinden işaretlemeyi unutma)
    }
}