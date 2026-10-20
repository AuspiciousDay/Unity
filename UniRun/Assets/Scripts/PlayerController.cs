using System;
using Unity.VisualScripting;
using UnityEngine;

// 1. 점프 뛰기 : 위로 속도를 가지게 한다. => Rigidbody의 속도를 설정
// 2. 애니메이션 제어 : 애니메이터에게 정확한 정보를 주면 => 그에 따라 플레이할 클립이 정해진다.
// 3. 죽음 처리

public class PlayerController : MonoBehaviour
{
    [Header("점프 기능")]
    [SerializeField] Rigidbody2D rigid; // 에디터 관리 방법
    [SerializeField] [Range(0f, 20f)] float jumpSpeed;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] int jumpCount;

    [Header("애니메이션")]
    [SerializeField] Animator animator;

    [Header("게임 오버")]
    [SerializeField] AudioSource dieSound;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // 코드 관리 방법
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount <= 2)
        {
            Jump();
        }
    }
    private void Jump()
    {
        rigid.linearVelocityY = jumpSpeed;
        jumpSound.Play();
        jumpCount++;
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        rigid.linearVelocity = Vector2.zero;
        rigid.simulated = false;
        dieSound.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Paltform")
        {
            jumpCount = 0;
            animator.SetBool("Grounded", true); 
        }        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Paltform")
        {
            animator.SetBool("Grounded", false);
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dead")
        {
            Die();   
        }        
    }
}
