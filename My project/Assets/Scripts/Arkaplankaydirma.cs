using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Bu satır ÇOK ÖNEMLİ! Arayüz (UI) elemanlarını kontrol etmek için gerekli.

public class ArkaPlanKaydirma : MonoBehaviour
{
    // Inspector'dan hızı ayarlayabilmek için public (herkese açık) değişkenler
    public float kaymaHiziX = 0.05f; // X ekseninde (yatay) kayma hızı
    public float kaymaHiziY = 0.05f; // Y ekseninde (dikey) kayma hızı

    private RawImage arkaplanResmi; // Arkaplan resmimizin bileşeni

    void Start()
    {
        // Bu kodun eklendiği objeden RawImage bileşenini bul ve değişkene ata
        arkaplanResmi = GetComponent<RawImage>();
    }

    void Update()
    {
        // Update fonksiyonu her karede bir kez çalışır

        // Resmimizin o anki "UV" (doku) dikdörtgenini alıyoruz. 
        // Bu, resmin neresinin gösterileceğini belirler.
        Rect mevcutRect = arkaplanResmi.uvRect;

        // X ve Y pozisyonlarını hızımıza ve zamana göre artırıyoruz.
        // Time.deltaTime, hareketin bilgisayar hızından bağımsız, pürüzsüz olmasını sağlar.
        mevcutRect.x += kaymaHiziX * Time.deltaTime;
        mevcutRect.y += kaymaHiziY * Time.deltaTime;

        // Hesapladığımız bu yeni dikdörtgeni, resmimize geri atıyoruz.
        // Wrap Mode = Repeat yaptığımız için, resim kaydıkça kendini tekrar edecek.
        arkaplanResmi.uvRect = mevcutRect;
    }
}