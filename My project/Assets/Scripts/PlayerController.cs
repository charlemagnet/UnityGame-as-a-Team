using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Charracter speed per sec")]
    public float moveSpeed = 5f;

    private Animator anim; 
    private Rigidbody2D rb; 

    
    void Start()
    {
        
        anim = GetComponent<Animator>();

        // Eğer fizik tabanlı (Rigidbody) bir hareket yapacaksanız bu satırı da ekleyin
        // rb = GetComponent<Rigidbody2D>(); 
    }

    
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            
            if (Input.mousePosition.x > Screen.width / 2)
            {
                
                MoveCharacter(Vector2.right);
            }
            else
            {
               
                StopCharacter();
            }
        }
        else
        {
            
            StopCharacter();
        }
    }

   
    void MoveCharacter(Vector2 direction)
    {
        
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        
        if (anim != null)
        {
            anim.SetBool("isWalking", true);
        }
    }

    void StopCharacter()
    {

        if (anim != null)
        {
            anim.SetBool("isWalking", false);
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
        anim.SetTrigger("isHit");
    }
}