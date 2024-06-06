using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 8f;
    [SerializeField] private float moveRange = 5f;

    private bool movingRight = true;
    private Vector3 originalPosition;
    private Vector3 previousPosition;
    private Transform playerTransform;
    private bool playerOnPlatform = false;

    void Start()
    {
        originalPosition = transform.position;
        previousPosition = transform.position;
    }

    void Update()
    {
        Moving();
    }

    public void Moving()
    {
        Vector3 movement;

        if (movingRight)
        {
            movement = Vector3.right * movingSpeed * Time.deltaTime;
            transform.Translate(movement);

            if (transform.position.x >= originalPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            movement = Vector3.left * movingSpeed * Time.deltaTime;
            transform.Translate(movement);

            if (transform.position.x <= originalPosition.x - moveRange)
            {
                movingRight = true;
            }
        }

        if (playerOnPlatform && playerTransform != null)
        {
            Vector3 platformMovement = transform.position - previousPosition;
            playerTransform.Translate(platformMovement, Space.World);
        }

        previousPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            playerTransform = null;
        }
    }
}
