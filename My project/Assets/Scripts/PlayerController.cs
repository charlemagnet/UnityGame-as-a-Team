/* PlayerController.cs */
// GÜNCELLENMİŞ SÜRÜM (Basılı Tutma Sorunu Çözümü)

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    // Private components
    private Animator anim;
    private Rigidbody2D rb;
    private float distanceTraveled = 0f;
    private Vector3 lastPosition;

    // --- YENİ EKLENEN ANAHTAR (FLAG) ---
    private bool isMoveButtonPressed = false; // "Hareket et" tuşuna basılı tutuluyor mu?
    
    // Private state
    private bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null) Debug.LogError("Rigidbody2D eksik!");
        if (groundCheck == null) Debug.LogError("Ground Check objesi atanmamış!");
    }
    void Update()
    {
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        // Her 1 birim ilerleyince +1 puan verelim
        if (distanceTraveled >= 1f)
        {
            GameManager.Instance.AddScore(1);
            distanceTraveled = 0f;
        }
    }

    void FixedUpdate()
    {
        // --- 1. HAREKET KONTROLÜ (HER KARE) ---
        // "Anahtar" (isMoveButtonPressed) AÇIKSA (true ise):
        if (isMoveButtonPressed)
        {
            // Hızı HER FİZİK KARESI 'moveSpeed' olarak zorla.
            // Bu, 'Linear Drag'in hızı yavaşlatmasını engeller.
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

            if (anim != null)
                anim.SetBool("isWalking", true);
        }
        // "Anahtar" KAPALIYSA (false ise):
        else
        {
            // Sadece yatay hızı durdur. Dikey hızı (zıplama/düşme) koru.
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (anim != null)
                anim.SetBool("isWalking", false);
        }

        // --- 2. ZEMİN KONTROLÜ (HER KARE) ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Dikey hızı sıfırla
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            SoundManager.PlaySound(SoundType.jump_sound);
        }
    }

    /// <summary>
    /// Hareket butonunun 'PointerDown' eventi buna bağlanacak.
    /// </summary>
    public void OnMovePointerDown()
    {
        // Anahtarı AÇ
        isMoveButtonPressed = true;
    }

    /// <summary>
    /// Hareket butonunun 'PointerUp' eventi buna bağlanacak.
    /// </summary>
    public void OnMovePointerUp()
    {
        // Anahtarı KAPAT
        isMoveButtonPressed = false;
    }

    // --- DİĞER FONKSİYONLAR ---

    public void Die()
    {
        // ... (Die fonksiyonun içeriği aynı kalabilir) ...
        GameManager.Instance.GameOver();
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}