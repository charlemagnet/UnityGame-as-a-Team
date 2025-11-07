using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yüklemek (Oyna tuşu) için bu kütüphane ŞART!
using UnityEngine.UI; // Toggle (Ses tuşu) için bu gerekebilir.

public class MainMenuManager : MonoBehaviour
{

    public string Game = "Game"; // BURAYI KENDİ SAHNE ADINLA DEĞİŞTİR

    
    public void OyunuBaslat()
    {
        // Belirtilen oyun sahnesini yükler.
        SceneManager.LoadScene(Game);
        SoundManager.PlaySound(SoundType.click);
    }

    // 2. ÇIKIŞ TUŞU FONKSİYONU
    public void OyundanCik()
    {

        Application.Quit();
        SoundManager.PlaySound(SoundType.click);
    }
    public void SesiAyarla(bool durum)
    {
        if (durum == true)
        {
     
            AudioListener.pause = false; 
        }
        else
        {
          
            AudioListener.pause = true; 
        }
    }

    void Start()
    {
        AudioListener.pause = false; // Sesler açık başlasın
        // (Toggle'ın "Is On" ayarını Unity içinden işaretlemeyi unutma)
    }
}