using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatObstacle : MonoBehaviour
{
    [Header("박쥐 이동 관련 변수")]
    [SerializeField] private float movingSpeed = 1f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private float collisionForce = 0.2f;
    [SerializeField] private float jumpForce = 2f; // 점프 힘 변수 추가

    private bool movingRight = true;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        MovingBat();
    }

    public void MovingBat()
    {
        // 현재 방향에 따라 박쥐 이동
        if (movingRight)
        {
            transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);

            // 이동 범위를 넘으면 방향 전환
            if (transform.position.x >= originalPosition.x + moveRange)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * movingSpeed * Time.deltaTime);

            if (transform.position.x <= originalPosition.x - moveRange)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        // 현재 방향에 따라 스프라이트를 반전
        if (movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // 충돌한 객체가 플레이어일 경우
        if (other.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision(other);
        }
    }

    private void HandlePlayerCollision(Collider2D playerCollider)
    {
        Rigidbody2D playerRigidbody = playerCollider.GetComponent<Rigidbody2D>();

        if (playerRigidbody != null)
        {
            Debug.Log("충돌");
            playerRigidbody.velocity = Vector2.zero;

            // 충돌 방향에 따라 힘을 가함
            if (movingRight)
            {
                playerRigidbody.AddForce(new Vector2(-collisionForce, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                playerRigidbody.AddForce(new Vector2(collisionForce, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
