using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatObstacle : MonoBehaviour
{
    [Header("���� �̵� ���� ����")]
    [SerializeField] private float movingSpeed = 1f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private float collisionForce = 0.2f;
    [SerializeField] private float jumpForce = 2f; // ���� �� ���� �߰�

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
        // ���� ���⿡ ���� ���� �̵�
        if (movingRight)
        {
            transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);

            // �̵� ������ ������ ���� ��ȯ
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
        // ���� ���⿡ ���� ��������Ʈ�� ����
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
        // �浹�� ��ü�� �÷��̾��� ���
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
            Debug.Log("�浹");
            playerRigidbody.velocity = Vector2.zero;

            // �浹 ���⿡ ���� ���� ����
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
