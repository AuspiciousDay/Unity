using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public int jumpCount;
    public Rigidbody2D rigid;
    public Collider2D collidersStand;
    public Collider2D collidersSlide;
    private  Animator animatior;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();
    }
    private void Start()
    {
        collidersStand.enabled = true;
        collidersSlide.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && jumpCount <= 1)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.X))
        {
            Sliding();
        }
        else if(!collidersStand.enabled)
        {
            collidersStand.enabled = true;
            collidersSlide.enabled = false;
        }
    }
    private void Sliding()
    {
        collidersStand.enabled = false;
        collidersSlide.enabled = true;
    }
    private void Jump()
    {
        rigid.linearVelocityY = jumpSpeed;
        jumpCount++;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            animatior.SetBool("Slide",Input.GetKey(KeyCode.X));
            animatior.SetBool("Grounded",true);
            jumpCount = 0;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            animatior.SetBool("Grounded",false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dead")
        {
            Die();
        }
    }
    private void Die()
    {
        animatior.SetTrigger("Die");
        rigid.linearVelocity = Vector2.zero;
        rigid.simulated = false;
    }
}
