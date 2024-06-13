using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }


    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveX, 0f, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
           isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    

    
}

