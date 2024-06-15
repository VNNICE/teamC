using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallOnRight; // 壁が右にあるかどうか

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
        if (Input.GetButtonDown("Jump") && (isGrounded || isTouchingWall))
        {
            // 壁キックロジック
            if (isTouchingWall && !isGrounded)
            {
                // 壁キックの方向を設定
                Vector2 wallKickDirection;
                if (isWallOnRight)
                {
                    wallKickDirection = new Vector2(-1, 1).normalized; // 左斜め上方向
                }
                else
                {
                    wallKickDirection = new Vector2(1, 1).normalized; // 右斜め上方向
                }
                rb.AddForce(wallKickDirection * jumpForce, ForceMode2D.Impulse);

                // 壁キック後のクールダウンを設定
                StartCoroutine(WallJumpCooldown());
            }
            else
            {
                // 通常のジャンプ
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private IEnumerator WallJumpCooldown()
    {
        isTouchingWall = false; // 壁キック後、すぐに再び壁に触れた状態にしない
        yield return new WaitForSeconds(0.2f); // クールダウン時間
        isTouchingWall = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;

            // 壁の左右を判別
            if (collision.contacts[0].point.x > transform.position.x)
            {
                isWallOnRight = true; // 壁が右にある
            }
            else
            {
                isWallOnRight = false; // 壁が左にある
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
