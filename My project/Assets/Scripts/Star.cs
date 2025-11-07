// Star.cs (Güncellendi)
using UnityEngine;

public class Star : MonoBehaviour
{
    public int scoreValue = 100;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bir şey çarptı: " + other.name);
            Debug.Log("PLAYER çarptı!");
            SoundManager.PlaySound(SoundType.star);
            // GameManager'a puan eklemesi için sinyal gönder
            GameManager.Instance.AddScore(scoreValue);
            
            // YILDIZ SPINNER'IN YENİ YILDIZ YARATMASI GEREKTİĞİNİ BİLDİR
            // Bu, StarSpawner'ın oyuncuyu takip ederken ne zaman yeni yıldız yaratacağını belirlemesini sağlar.
            StarSpawner.Instance.NotifyStarCollected(); 
            
            // Bu yıldız objesini yok et (toplanmış olur)
            Destroy(gameObject);
        }
    }
}