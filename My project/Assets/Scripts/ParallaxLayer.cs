using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Bu script için SpriteRenderer zorunlu
public class ParallaxLayer : MonoBehaviour
{
    [Tooltip("Kameranın ne kadar yavaş hareket edeceğini belirler.\n0 = Hiç hareket etmez.\n1 = Kamera ile aynı hızda.\nEn arkadaki katman (gökyüzü) için 0.1, en öndeki için 0.8 gibi değerler verin.")]
    [Range(0f, 1f)]
    public float parallaxFactorX;
    
    [Tooltip("Dikey (Y ekseni) paralaks. Gerekmiyorsa 0 bırakın.")]
    [Range(0f, 1f)]
    public float parallaxFactorY;

    [Tooltip("Bu katman sonsuz döngüye (tiling) girecek mi?")]
    public bool enableTiling;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float spriteWidth;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        if (enableTiling)
        {
            // Sprite'ın dünyadaki gerçek genişliğini alıyoruz
            spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    void LateUpdate()
    {
        // 1. PARALAKS HAREKETİ
        // Kameranın bu frame'de ne kadar hareket ettiğini bul
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        
        // Bu katmanın ne kadar hareket etmesi gerektiğini hesapla
        // (Paralaks faktörü 0 ise (en arka plan), moveAmount = 0 olur ve hiç hareket etmez)
        Vector3 moveAmount = new Vector3(deltaMovement.x * parallaxFactorX, deltaMovement.y * parallaxFactorY, 0);

        // Katmanı hareket ettir
        transform.position += moveAmount;

        // Kameranın son pozisyonunu kaydet
        lastCameraPosition = cameraTransform.position;

        // 2. DÖNGÜ (TILING) KONTROLÜ
        if (enableTiling)
        {
            // Bu objenin kameraya göre pozisyonunu kontrol et
            float distanceToCameraX = cameraTransform.position.x - transform.position.x;

            // Kamera bizden çok uzaklaştıysa ışınla
            
            // Kamera sağa gitti, biz solda kaldık (Kamera bizden > 1 sprite boyu ileride)
            if (distanceToCameraX > spriteWidth)
            {
                // Bizi 2 sprite boyu sağa ışınla (Diğer kopyanın sağına)
                transform.position = new Vector3(transform.position.x + (spriteWidth * 2f), transform.position.y, transform.position.z);
            }
            // Kamera sola gitti, biz sağda kaldık (Kamera bizden > 1 sprite boyu geride)
            else if (distanceToCameraX < -spriteWidth)
            {
                // Bizi 2 sprite boyu sola ışınla (Diğer kopyanın soluna)
                transform.position = new Vector3(transform.position.x - (spriteWidth * 2f), transform.position.y, transform.position.z);
            }
        }
    }
}